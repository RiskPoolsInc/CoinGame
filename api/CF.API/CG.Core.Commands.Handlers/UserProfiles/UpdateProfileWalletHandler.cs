using App.Common.Helpers;
using App.Core.Exceptions;
using App.Interfaces.Core;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Settings;
using App.Interfaces.Security;

using CF.App.ViewModels.Users;
using CF.Core.Commands.TaskTakeRequests;
using CF.Core.Commands.UserProfiles;
using CF.Core.Enums;
using CF.Data.Criterias.Profiles;
using CF.Data.Criterias.UserProfiles;
using CF.Data.Entities.UserProfiles;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CF.Core.Commands.Handlers.UserProfiles;

public class UpdateProfileWalletHandler : IRequestHandler<UpdateUserProfileWalletCommand, UserProfileView> {
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IAppDispatcher _dispatcher;
    private readonly ISettingPropertyRepository _settingPropertyRepository;
    private readonly Guid _currentUserId;

    public UpdateProfileWalletHandler(IUserProfileRepository     userProfileRepository,
                                      IContextProvider           contextProvider, IAppDispatcher dispatcher,
                                      ISettingPropertyRepository settingPropertyRepository) {
        _userProfileRepository = userProfileRepository;
        _dispatcher = dispatcher;
        _settingPropertyRepository = settingPropertyRepository;
        _currentUserId = contextProvider.Context.UserId;
    }

    public async Task<UserProfileView> Handle(UpdateUserProfileWalletCommand request, CancellationToken cancellationToken) {
        if (request.UbikiriUserId != _currentUserId)
            throw new AccessDeniedException("Can't change another user profile");

        var walletIsNotUnique = await _userProfileRepository.AnyAsync(new UserProfileByLinkedWallet(request.LinkedWallet),
                                                                      cancellationToken);

        if (walletIsNotUnique)
            throw new ArgumentException("Wallet address could be unique");

        var criteria = new UserProfileByUbikiriUserId(request.UbikiriUserId);
        var user = await _userProfileRepository.Where(criteria).SingleAsync(cancellationToken);
        _userProfileRepository.Update(user.Id, profile => profile.LinkedWallet = request.LinkedWallet);
        await _userProfileRepository.SaveAsync(cancellationToken);

        var telegramSetting = await _settingPropertyRepository
                                   .Where(a => a.TypeId == (int)SettingPropertyTypes.RequiredTelegramConfirmation)
                                   .SingleAsync();

        if (telegramSetting is not { ValueBoolean: true } and not { ValueNumber: 1 })
            await _dispatcher.Send(new TryAssignRegistrationTaskCommand());

        return await _userProfileRepository.Get(user.Id).SingleAsync<UserProfile, UserProfileView>(CancellationToken.None);
    }
}