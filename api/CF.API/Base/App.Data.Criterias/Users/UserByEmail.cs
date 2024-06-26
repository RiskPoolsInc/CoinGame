using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;
using App.Data.Entities.Users;

namespace App.Data.Criterias.Users {
    public class UserByEmail : ACriteria<User> {
        public UserByEmail(string email, bool strict = false) {
            _email = email?.ToLower() ?? throw new ArgumentNullException(nameof(email));
            _strict = strict;
        }
        private readonly string _email;
        private readonly bool _strict;

        public override Expression<Func<User, bool>> Build() {
            if (_strict)
                return a => a.Email.ToLower() == _email;
            return a => a.Email.ToLower().StartsWith(_email);
        }
    }
}
