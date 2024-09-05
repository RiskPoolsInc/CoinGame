using Autofac;

using AutoMapper;

namespace App.Common.Modules;

public class AutoMapperModule : Module {
    private readonly Action<IMapperConfigurationExpression> _setup;

    public AutoMapperModule(Action<IMapperConfigurationExpression> setup) {
        _setup = setup;
    }

    protected override void Load(ContainerBuilder builder) {
        Mapper.Initialize(_setup);
        builder.RegisterInstance(Mapper.Configuration);
        builder.RegisterType<Mapper>().As<IMapper>();
    }
}