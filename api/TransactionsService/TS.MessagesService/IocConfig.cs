using System.Reflection;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

using NATS.Client;

using Newtonsoft.Json;

using SBN.Domain.Models;

using SilentNotary.Common;
using SilentNotary.Common.Storage;
using SilentNotary.Cqrs.Domain.Interfaces;
using SilentNotary.Cqrs.Nats;
using SilentNotary.Cqrs.Nats.Abstract;

using TS.Configuration;
using TS.Configuration.Contracts;

namespace TS.MessagesService;

public static class IocConfig {
    private const string DbCtxName = "DefaultConnection";

    private static Assembly[] Assemblies = {
        typeof(MessageHistory).Assembly,
    };

    public static IServiceCollection AddCqrsSlave(this IServiceCollection services) {
        return services.AddScoped<IMessageSender, SimpleMsgBus>()
                       .AddSingleton<INatsSerializer, NatsSerializer>()
                       .AddSingleton<INatsReceiverCommandQueueFactory, AppNatsCommandQueueFactory>()
                       .AddSingleton<INatsConnectionFactory>(cf => {
                            var serializer = (INatsSerializer)cf.GetService(typeof(INatsSerializer));

                            var options =
                                ((IOptions<NatsReceiverOptions>)cf.GetService(
                                    typeof(IOptions<NatsReceiverOptions>))) // ??? NatsSenderOptions
                               .Value;

                            var natsOptions = ConnectionFactory.GetDefaultOptions();
                            natsOptions.Url = options.Url;
                            natsOptions.Timeout = 60000;

                            System.Console.WriteLine($"NatsConnectionFactory nats options: {JsonConvert.SerializeObject(natsOptions)}");

                            return new NatsConnectionFactory(serializer, natsOptions);
                        })
                       .AddSingleton(typeof(IStorage<>), typeof(SimpleStorage<>))
                       .RegisterAssemblyImplementationsScoped(Assemblies, typeof(IStorage<>))
                       .AddTransient<IMessageResult, MessageHistory>();
    }

    public static IServiceCollection AddConfigOptions(this IServiceCollection services,
                                                      IConfiguration          configuration) {
        // for correct processing of uploaded files (multipart/form-data)
        services.Configure<FormOptions>(x => x.BufferBody = true);

        services.Configure<NatsReceiverOptions>(options => {
            options.Queue = configuration.GetValue<string>("NatsReceiverOptions:Queue");
            options.Subject = configuration.GetValue<string>("NatsReceiverOptions:Subject");
            options.Url = configuration.GetValue<string>("NatsReceiverOptions:Url");
            System.Console.WriteLine($"Nats configuration: {JsonConvert.SerializeObject(options)}");
        });
        return services;
    }

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services) {
        return services
              .RegisterAssemblyImplementationsScoped(Assemblies, typeof(IEventHandler<>))
              .RegisterAssemblyImplementationsScoped(Assemblies, typeof(IMsgHandler<>));
    }
}