using System;
using App.Data.Criterias.Helpers;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Users {
    public class UsersByIds : EntitiesByIds<User> {
        public UsersByIds(Guid[] ids, bool include = true) : base(ids, include) {
        }
    }
}