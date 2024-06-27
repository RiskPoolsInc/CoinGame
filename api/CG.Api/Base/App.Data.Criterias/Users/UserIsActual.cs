using System.Linq.Expressions;
using App.Data.Criterias.Core;

namespace App.Data.Criterias.Users {
    public class UserIsActual : ACriteria<User> {

        public override Expression<Func<User, bool>> Build() {
            return a => a.Password != null;
        }
    }
}
