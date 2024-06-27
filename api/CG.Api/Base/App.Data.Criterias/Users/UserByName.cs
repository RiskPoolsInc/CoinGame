using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Users {
    public class UserByName : ACriteria<User> {
        public UserByName(string name) {
            _name = name?.ToLower() ?? throw new ArgumentNullException(nameof(name));
        }
        private readonly string _name;

        public override Expression<Func<User, bool>> Build() {
            return a => (a.FirstName + " " + a.LastName).ToLower().StartsWith(_name);
        }
    }
}
