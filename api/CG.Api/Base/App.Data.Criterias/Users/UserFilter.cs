using System.Linq.Expressions;
using App.Core.Enums;
using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;

namespace App.Data.Criterias.Users
{
    public class UserFilter : PagedCriteria<User>
    {
        public string Name { get; set; }
        public string NameLike { get; set; }
        public string Email { get; set; }
        public Guid? OfficeId { get; set; }
        public int? Status { get; set; }
        public bool? ExistEmail { get; set; }

        public override Expression<Func<User, bool>> Build()
        {
            var criteria = True;

            if (!String.IsNullOrWhiteSpace(Name))
                criteria = criteria.And(new UserByName(Name));

            if (!String.IsNullOrWhiteSpace(NameLike))
                criteria = criteria.And(new UserByFirstNameLike(NameLike).Or(new UserByLastNameLike(NameLike)));

            if (!String.IsNullOrWhiteSpace(Email))
                criteria = criteria.And(new UserByEmail(Email));

            if (Status.HasValue && Status.Value != (int) UserStatuses.All)
                criteria = criteria.And(new UserByIsActive(Status.Value == (int) UserStatuses.Active));


            if (ExistEmail.HasValue)
            {
                criteria = criteria.And(new UserByExistEmail(ExistEmail.Value));
            }

            if (String.IsNullOrWhiteSpace(Sort))
                SetSortBy(a => a.CreatedOn);

            return criteria.Build();
        }
    }
}