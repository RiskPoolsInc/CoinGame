using App.Core.Commands.Handlers.Mapping;
using App.Data.Mapping;

using AutoMapper;

using NUnit.Framework;

using CommandToEntityProfile = App.Core.Commands.Handlers.Mapping.CommandToEntityProfile;

namespace CG.Tests.Commands.Handlers;

[SetUpFixture]
public class Setup {
    [OneTimeSetUp]
    public void Startup() {
        Mapper.Initialize(cfg => {
            cfg.AddProfile<CommandToEntityProfile>();
            cfg.AddProfile<CommandToAdminProfile>();
            cfg.AddProfile<ExternalViewProfile>();
            cfg.AddProfile<CommandToCommandProfile>();
        });
    }
}