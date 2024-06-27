using System.Linq.Expressions;
using App.Core.Enums;
using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities.UserProfiles;
using App.Interfaces.RequestsParams;

namespace App.Data.Criterias.UserProfiles;

public class UserProfilesFilter : PagedCriteria<UserProfile>
{
    public int? TypeId { get; set; }
    public string? Email { get; set; }

    public string[]? Emails { get; set; }
    public string[]? TwitterIds { get; set; }
    public string[]? TelegramIds { get; set; }


    public string? TwitterId { get; set; }
    public string? TelegramId { get; set; }

    public string? LinkedWallet { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? ReferralId { get; set; }
    public int? ReferralTypeId { get; set; }
    public Guid? TaskId { get; set; }

    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }

    public bool? ExistEmail { get; set; }

    public bool? IsActive { get; set; }
    public bool? IsBlocked { get; set; }
    public Guid? UbikiriUserId { get; set; }
    public Guid? Id { get; set; }
    public Guid[]? TaskIds { get; set; }
    public string? Text { get; set; }

    public string[]? ExclusiveProperties { get; set; }
    public KeyWordFilter[]? KeyWords { get; set; }


    public override Expression<Func<UserProfile, bool>> Build()
    {
        var criteria = True;

        if (TypeId.HasValue)
            criteria = criteria.And(new UserProfileByTypeId(TypeId.Value));

        if (CreatedFrom.HasValue && CreatedFrom.Value > DateTime.MinValue)
            criteria = criteria.And(new UserProfileCreatedFrom(CreatedFrom.Value));

        if (CreatedTo.HasValue && CreatedTo.Value > DateTime.MinValue)
            criteria = criteria.And(new UserProfileCreatedTo(CreatedTo.Value));

        if (Id.HasValue && Id.Value != default)
            criteria = criteria.And(new UserProfileByIds(new[] {Id.Value}));

        if (UbikiriUserId.HasValue && UbikiriUserId.Value != default)
            criteria = criteria.And(new UserProfileByUbikiriId(UbikiriUserId.Value));


        if (string.IsNullOrEmpty(Sort))
            SetSortBy(a => a.CreatedOn);

        return criteria.Build();
    }

    public override IQueryable<UserProfile> OrderBy(IQueryable<UserProfile> source)
    {
        if (string.IsNullOrEmpty(Sort))
            SetSortBy(a => a.CreatedOn);

        return Sort.ToLower() switch
        {
            "createdon" => OrderByDirection(source, s => s.CreatedOn),
            "type" => OrderByDirection(source, s => s.TypeId),
            _ => base.OrderBy(source),
        };
    }
}