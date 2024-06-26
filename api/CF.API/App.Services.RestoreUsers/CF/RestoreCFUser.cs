using App.Core.Enums;
using App.Data.Entities.Companies;
using App.Data.Entities.Payments.TaskRequests;
using App.Data.Entities.UserProfiles;
using App.Data.Entities.Wallets;
using App.Interfaces.Data.Sql;
using App.Repositories.CustomerCompanies;
using App.Repositories.Payments.TaskRequests;
using App.Repositories.ReferralPairs;
using App.Repositories.Tasks;
using App.Repositories.TaskTakeRequests;
using App.Repositories.UserLogs;
using App.Repositories.UserProfiles;
using App.Repositories.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace App.Services.RestoreUsers.CF;

public class RestoreCFUser {
    private readonly IAppDbContext _appDbContext;
    private readonly List<string> _emails;
    private readonly TaskTakeRequestRepository _importTaskRequestRepository;
    private readonly UserLogRepository _importUserLogRepository;
    private readonly UserProfileRepository _importUserProfileRepository;
    private readonly IAppDbContext _oldAppDbContext;
    private readonly TaskTakeRequestRepository _taskRequestRepository;
    private readonly UserLogRepository _userLogRepository;
    private readonly UserProfileRepository _userProfileRepository;
    private int _countRestoreUsers;
    private Guid _firsUser;
    private readonly ReferralPairRepository _importReferralPairsRepository;
    private ILogger _localLogger;
    private readonly ReferralPairRepository _referralPairsRepository;

    private List<UserProfile> _userProfiles;
    private int _usersByEmail;

    public RestoreCFUser(IAppDbContext appDbContext, IAppDbContext oldAppDbContext) {
        _appDbContext = appDbContext;
        _oldAppDbContext = oldAppDbContext;

        _importUserProfileRepository = new UserProfileRepository(_oldAppDbContext);
        _userProfileRepository = new UserProfileRepository(_appDbContext);

        _importTaskRequestRepository = new TaskTakeRequestRepository(_oldAppDbContext);
        _taskRequestRepository = new TaskTakeRequestRepository(_appDbContext);

        _importUserLogRepository = new UserLogRepository(_oldAppDbContext);
        _userLogRepository = new UserLogRepository(_appDbContext);

        _importReferralPairsRepository ??= new ReferralPairRepository(_oldAppDbContext);
        _referralPairsRepository ??= new ReferralPairRepository(_appDbContext);
        _usersByEmail = 0;
        _emails = new List<string>();
    }

    public UserProfile[] RestoreUser(string email) {
        _localLogger = new RestoreCFUserLoggerProvider().CreateLogger($"{++_usersByEmail}_queries");
        Log(email, "Restoring");

        var importUser = _importUserProfileRepository.GetAllWithDeleted()
                                                     .SingleOrDefault(a => a.Email != null && a.Email.ToLower() == email.ToLower());

        if (importUser == null) {
            Error($"User with email [{email}] not found");
            return Array.Empty<UserProfile>();
        }

        _firsUser = importUser.Id;

        _userProfiles = new List<UserProfile>();
        _countRestoreUsers = 0;
        var users = RestoreUser(importUser)?.ToList() ?? new List<UserProfile>();
        users.Add(importUser);

        _userProfiles.AddRange(users);

        return users.ToArray();
    }

    public UserProfile[] RestoreUser(UserProfile importUser) {
        if (importUser == null)
            throw new Exception("User not found");

        if (importUser.TypeId == (int)UserTypes.Executor && _emails.All(a => a.ToLower() != importUser.Email.ToLower())) {
            _emails.Add(importUser.Email.ToLower());
            EmailLogger.LogInformation(importUser.Email + ",");
        }


        _countRestoreUsers++;

        Log(importUser, $"[#{_countRestoreUsers}] Restoring User Found with data: " +
            JsonConvert.SerializeObject(importUser, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        var userId = importUser.Id;
        var email = importUser.Email;

        try {
            var anyUser = _userProfileRepository.GetAllWithDeleted()
                                                .Any(a =>
                                                         (a.Email != null &&
                                                         email != null &&
                                                         a.Email.ToLower() == email.ToLower()) ||
                                                         a.Email == email);
            QueryLog(_userProfileRepository.SqlAddQuery(importUser));

            if (!anyUser) {
                 importUser.ReferralPairs = null;
                importUser.Company = null;
                importUser.Type = null;
                importUser.ReferralType = null;
                importUser.TaskTakeRequest = null;
                importUser.InvitedReferralPairs = null;
                _userProfileRepository.Add(importUser);
                _userProfileRepository.Save();
                Log(importUser, "Insert Complete");
            }


            RestoreTaskRequests(userId);
            RestoreUserLogs(userId);
            RestoreTaskRequestPayments(userId);

            Log($"Restore base tables for {importUser.Id} {importUser.Email} complete");

            if (importUser.Id != _firsUser)
                return Array.Empty<UserProfile>();

            var referrals = GetReferralsForRestore(userId)?.ToList();

            if (!(referrals?.Count > 0))
                return referrals?.ToArray();

            foreach (var userProfile in referrals.ToArray())
                referrals.AddRange(RestoreUser(userProfile) ?? Array.Empty<UserProfile>());

            RestoreReferrals(importUser);

            return referrals?.ToArray();
        }
        catch (Exception e) {
            Log(e);
            throw;
        }
    }

    private UserProfile[] GetReferralsForRestore(Guid userId) {
        var referrals = _importReferralPairsRepository
                       .Where(a => a.InvitedByUserId == userId)
                       .Include(a => a.User)
                       .Include(a => a.AwardBy)
                       .Include(a => a.CustomerCompany)
                       .ToArray();

        if (!referrals.Any())
            return null;
        var awardBy = referrals.Select(a => a.AwardBy);
        var users = referrals.Select(a => a.User);
        var list = awardBy.ToList();
        list.AddRange(users);
        return list.Where(a => a != null).DistinctBy(a => a.Id).Where(a => a.Id != userId).ToArray();
    }

    public void RestoreReferrals(UserProfile user) {
        try {
            var referrals = _importReferralPairsRepository.Where(a => a.InvitedByUserId == user.Id)
                                                          .ToArray();

            if (!referrals.Any())
                return;

            foreach (var referralPair in referrals)
                try {
                    QueryLog(_referralPairsRepository.SqlAddQuery(referralPair));

                    if (_referralPairsRepository.Any(a => a.Id == referralPair.Id))
                        continue;

                    var customerCompany = referralPair.CustomerCompany;
                    referralPair.CustomerCompany = null;
                    RestoreCustomer(customerCompany);

                    _referralPairsRepository.Add(referralPair);
                    _referralPairsRepository.Save();
                }
                catch (Exception e) {
                    Log(e);
                }
        }
        finally {
            Log($"RestoreReferrals for user {user.Email} complete");
        }
    }

    private void RestoreUserLogs(Guid userId) {
        var userLogs = _importUserLogRepository.Where(a => a.UserId == userId)
                                               .Include(a => a.UserByReferral)
                                               .ToArray();

        foreach (var userLog in userLogs)
            try {
                var userByReferral = userLog.UserByReferral;

                if (userByReferral != null)
                    if (!_userProfiles.Any(a => a.Id == userByReferral.Id)) {
                        _userProfiles.Add(userByReferral);
                        RestoreUser(userByReferral);
                    }

                QueryLog(_userLogRepository.SqlAddQuery(userLog));

                if (_userLogRepository.Any(userLog.Id))
                    return;

                userLog.UserByReferral = null;
                _userLogRepository.Add(userLog);
                _userLogRepository.Save();
            }
            catch (Exception e) {
                Log("Error RestoreUserLogs:", e);
            }
    }

    private void RestoreTaskRequestPayments(Guid userId) {
        var importRep = new TaskRequestPaymentRepository(_oldAppDbContext);
        var rep = new TaskRequestPaymentRepository(_appDbContext);

        var requestPayments = importRep.Where(a => a.TaskRequest.UserProfileId == userId).ToArray();

        foreach (var taskRequestPayment in requestPayments) {
            RestoreBatchPayment(taskRequestPayment.BatchPaymentId);

            if (rep.Any(taskRequestPayment.Id))
                continue;

            rep.Add(taskRequestPayment);
            rep.Save();
        }
    }

    private void RestoreBatchPayment(Guid? batchPaymentId) {
        var importRep = new BatchRequestsPaymentRepository(_oldAppDbContext);
        var rep = new BatchRequestsPaymentRepository(_appDbContext);

        if (batchPaymentId.HasValue && !rep.Any(a => a.Id == batchPaymentId.Value)) {
            var batch = importRep.Get(batchPaymentId.Value)
                                 .Include(a => a.TransactionTaskRequestPayment)
                                 .Single();

            RestoreTransaction(batch.TransactionTaskRequestPayment);

            batch.TransactionTaskRequestPayment = null;

            if (rep.Any(batch.Id))
                return;

            rep.Add(batch);
            rep.Save();
        }
    }

    private void RestoreTransaction(TransactionTaskRequestPayment transaction) {
        var rep = new TransactionRequestsPaymentRepository(_appDbContext);

        var user = _importUserProfileRepository.GetAllWithDeleted().Single(a => a.Id == transaction.CreatedById);

        if (_userProfiles.All(a => a.Id != user.Id)) {
            _userProfiles.Add(user);
            _userProfiles.AddRange(RestoreUser(user));
            _userProfiles = _userProfiles.DistinctBy(a => a.Id).ToList();
        }

        QueryLog(rep.SqlAddQuery(transaction));

        if (rep.Any(transaction.Id))
            return;
        
        transaction.CreatedBy = null;
        rep.Add(transaction);
        rep.Save();
    }

    private void RestoreTaskRequests(Guid userId) {
        var taskRequests = _importTaskRequestRepository.Where(a => a.UserProfileId == userId)
                                                       .Include(a => a.Task)
                                                       .ToArray();

        foreach (var taskTakeRequest in taskRequests) {
            if (taskTakeRequest.Task != null)
                RestoreTask(taskTakeRequest.TaskId);

            try {
                QueryLog(_taskRequestRepository.SqlAddQuery(taskTakeRequest));

                if (!_taskRequestRepository.GetAllWithDeleted().Any(a => a.Id == taskTakeRequest.Id)) {
                    taskTakeRequest.UserProfile = null;
                    taskTakeRequest.Task = null;

                    _taskRequestRepository.Add(taskTakeRequest);
                    _taskRequestRepository.Save();
                }
            }
            catch (Exception e) {
                Log($"Error RestoreTaskRequests {taskTakeRequest.Id} for userId {userId}: ", e);
            }
        }
    }

    private void RestoreTask(Guid taskId) {
        var importRep = new TaskRepository(_oldAppDbContext);
        var taskRep = new TaskRepository(_appDbContext);

        var task = importRep.GetAllWithDeleted()
                            .Where(a => a.Id == taskId)
                            .Include(a => a.Wallet)
                            .Include(a => a.CreatedBy)
                            .SingleOrDefault();

        try {
            if (task.Wallet != null) {
                RestoreWallet(task.Wallet);
                task.Wallet = null;
            }

            if (task.CreatedBy != null) {
                RestoreCustomer(task.CreatedBy);
                task.CreatedBy = null;
            }

            QueryLog(importRep.SqlAddQuery(task));

            if (!taskRep.GetAllWithDeleted().Any(a => a.Id == task.Id)) {
                taskRep.Add(task);
                taskRep.Save();
            }
        }
        catch (Exception e) {
            Log("Error RestoreTask: ", e);
        }
    }

    private void RestoreCustomer(CustomerCompany customerCompany) {
        if (customerCompany == null)
            return;

        var importRep = new CustomerCompanyRepository(_oldAppDbContext);
        var rep = new CustomerCompanyRepository(_appDbContext);

        try {
            var company = importRep.GetAllWithDeleted()
                                   .Where(a => a.Id == customerCompany.Id)
                                   .Include(a => a.Wallet)
                                   .Single();

            RestoreWallet(company.Wallet);

            QueryLog(importRep.SqlAddQuery(customerCompany));
            company.Wallet = null;

            if (rep.GetAllWithDeleted().Any(a => a.Id == company.Id))
                return;

            rep.Add(company);
            rep.Save();
        }
        catch (Exception e) {
            Log(e);
        }
    }

    private void RestoreWallet(Wallet companyWallet) {
        var importRep = new WalletRepository(_oldAppDbContext);
        var rep = new WalletRepository(_appDbContext);

        try {
            var wallet = importRep.GetAllWithDeleted()
                                  .Where(a => a.Id == companyWallet.Id)
                                  .Include(a => a.UserProfile)
                                  .Single();

            if (wallet.UserProfile != null) {
                _userProfiles.Add(wallet.UserProfile);
                RestoreUser(wallet.UserProfile);
            }

            wallet.UserProfile = null;

            QueryLog(importRep.SqlAddQuery(companyWallet));

            if (rep.GetAllWithDeleted().Any(a => a.Id == wallet.Id))
                return;

            rep.Add(wallet);
            rep.Save();
        }
        catch (Exception e) {
            Log(e);
        }
    }

    #region Logging

    public ILogger? Logger { get; set; }
    public ILogger EmailLogger { get; set; }

    private void Log(string message, Exception exception) {
        Log($"{message} \r\n {exception}");
    }

    private void Log(Exception exception) {
        Log(exception.ToString());
    }

    private void Log(UserProfile user, string message) {
        Log($"[{user.Id:D} || {user.Email}] || {message}");
    }

    private void Log(string email, string message) {
        Log($"[{email}] || {message}");
    }

    private void Log(Guid id, string message) {
        Log($"[Id:{id:D}] || {message}");
    }

    private void QueryLog(string message) {
        _localLogger.LogInformation(message);
    }

    private void Log(string message) {
        if (Logger == null || string.IsNullOrWhiteSpace(message))
            return;
        Logger.LogInformation(message);
    }

    private void Error(string message) {
        if (Logger == null || string.IsNullOrWhiteSpace(message))
            return;
        Logger.LogError(message);
    }

    #endregion
}