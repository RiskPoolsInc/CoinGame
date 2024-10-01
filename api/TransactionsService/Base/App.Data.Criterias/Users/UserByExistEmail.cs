using System;
using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Users {
	public class UserByExistEmail : ACriteria<User> {
        private readonly bool _existEmail;

        public UserByExistEmail(bool existEmail) {
            _existEmail = existEmail;
        }

		public override Expression<Func<User, bool>> Build() {
            return a => !string.IsNullOrWhiteSpace(a.Email) == _existEmail;
        }
	}
}