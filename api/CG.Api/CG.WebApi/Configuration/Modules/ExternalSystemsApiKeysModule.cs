using Autofac;

namespace CF.WebApi.Configuration.Modules;

public class ExternalSystemsApiKeysModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var apiKeys = config.GetSection("ExternalSystemsApiKeys").Get<ExternalSystemsApiKeys>();
                return apiKeys ?? new ExternalSystemsApiKeys()
                {
                    ApiKeys = Array.Empty<ExternalSystemsApiKey>()
                };
            })
            .SingleInstance();
    }
}