using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Users {
    public class UserByFirstNameLike : AContainsCriteria<User> {
        public UserByFirstNameLike(string name) : base(name) {
            _name = name?.ToLower() ?? throw new ArgumentNullException(nameof(name));
        }
        private readonly string _name;

        protected override Expression<Func<User, string>> Property => a => a.FirstName;
    }
}
