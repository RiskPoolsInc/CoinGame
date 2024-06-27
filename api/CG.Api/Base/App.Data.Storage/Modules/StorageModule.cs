using App.Core.Configurations;
using App.Data.Storage.Blobs;
using App.Data.Storage.Docker;
using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

using Autofac;

namespace App.Data.Storage.Modules; 

public class StorageModule : Module {
    private const string STORAGE_CSTRING_KEY = "AppStorage";
    private readonly string _key;

    public StorageModule() : this(STORAGE_CSTRING_KEY) {
    }

    public StorageModule(string key) {
        _key = key;
    }

    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);

        builder.Register(ctx => {
                    var config = ctx.Resolve<AttachmentsFolderConfig>();
                    return new DockerStorageClient(config.Directory, config.HostUrl);
                })
               .As<IStorageClient>();
        builder.RegisterType<AttachmentStorage>().As<IAttachmentStorage>();
        builder.RegisterType<EmailStorage>().As<IEmailStorage>();
        builder.RegisterType<ImageStorage>().As<IImageStorage>();
        builder.RegisterType<StorageFactory>().As<IStorageFactory>();
        builder.RegisterType<NamedStorageResolver>().As<INamedStorageResolver>();
    }
}