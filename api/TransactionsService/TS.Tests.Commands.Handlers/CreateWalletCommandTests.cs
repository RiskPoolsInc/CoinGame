using App.Core.Commands.Wallets;
using App.Core.Enums;

using CG.Tests.Commands.Handlers.Cases;

using NUnit.Framework;

namespace CG.Tests.Commands.Handlers;

[TestFixture]
public class CreateWalletCommandTests {
    [Test]
    public async Task CreateWalletCommandTest() {
        using (var testable = Testables.Wallet()) {
            var result = await testable.CallCreate(new CreateWalletCommand {
                Address = "",
                PrivateKey = null,
                TypeId = (int)WalletTypes.Generated
            });
            testable.VerifyAdd();
            testable.VerifyReturnView();
        }
    }

    [Test]
    public async Task GenerateWalletCommandTest() {
        using (var testable = Testables.Wallet()) {
            var result = await testable.CallGenerate(new GenerateWalletCommand());
            testable.VerifyGenerate();
        }
    }

    [Test]
    public async Task ImportWalletCommandTest() {
        using (var testable = Testables.Wallet()) {
            var result = await testable.CallImport(new ImportWalletCommand() {
                Address = "assssc",
                PrivateKey = "acccccs"
            });
            testable.VerifyImport();
        }
    }
}