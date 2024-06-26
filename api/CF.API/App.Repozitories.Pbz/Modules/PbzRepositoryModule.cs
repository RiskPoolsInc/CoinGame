using App.Core.Logging.Repository;
using App.Interfaces.Repositories.Core;
using Autofac;

namespace App.Repozitories.Pbz.Modules
{

    public class PbzRepositoryModule : BaseRepositoryModule<PbzRepositoryModule>
    {

//     protected override void Load(ContainerBuilder builder)
//     {
//         var typesRegistration =
//             ThisAssembly.GetTypes().Where(t => t.IsClosedTypeOf(typeof(IBaseRepository<,>)) && !t.IsAbstract).ToArray();
//         builder.RegisterType<RepositoryMethodLogger>();
//         builder.RegisterTypes(typesRegistration).AsSelf().AsImplementedInterfaces();
//         base.Load(builder);
//     }
// }
    }
}