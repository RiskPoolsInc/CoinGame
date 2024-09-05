using System;
using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Users {
	public class UserByIsActive : ACriteria<User> {
		private readonly bool _isActive;

		public UserByIsActive(bool isActive) {
			_isActive = isActive;
		}

		public override Expression<Func<User, bool>> Build() {
			return a => a.IsActive == _isActive;
		}
	}
}