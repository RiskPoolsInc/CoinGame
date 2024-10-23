using App.Core.Configurations.Modules;
using App.Core.Logging.Repository;
using App.Interfaces.Repositories.Core;

using Autofac;

namespace App.Repositories.Core;

public class BaseRepositoryModule<T> : BaseAutofacModule<T> {
    protected override Func<Type, bool> RegisterTypesFilter => t => t.IsClosedTypeOf(typeof(IBaseRepository<,>)) && !t.IsAbstract;

    protected override void Load(ContainerBuilder builder) {
        builder.RegisterType<RepositoryMethodLogger>();
        base.Load(builder);
    }
}