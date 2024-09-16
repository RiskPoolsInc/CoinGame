using System.Linq.Expressions;

using App.Core.Commands.Handlers.Wallets;
using App.Core.Commands.Wallets;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Core;
using App.Interfaces.Handlers.Requests;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using AutoMapper;

using Moq;

using Shouldly;

using TNET.Tests.Helpers;

namespace CG.Tests.Commands.Handlers.Cases;

public class WalletTestable : IDisposable {
    private Mock<IWalletRepository> _walletRepositoryMock;
    private Mock<IGetWalletHandler> _walletHandlerMock;
    private CancellationToken _cancellationToken;
    private CancellationTokenSource _cts;
    private IMapper _mapper;
    private Wallet _entity = new Wallet() { Id = Guid.NewGuid() };
    private Guid _entityId = Guid.Empty;
    private readonly Wallet[] _wallets; // = new List<Wallet>() { _entity }.AsQueryable();
    private readonly Mock<IWalletService> _walletServiceMock;
    private readonly Mock<IDispatcher> _dispatcherMock;

    private readonly GeneratedWalletView _generatedWalletView = new GeneratedWalletView() {
        Address = "aaassdddc",
        PrivateKey = "testPrivateKey"
    };

    private CreateGeneratedWalletCommand _createWalletCommand;
    private WalletView _createWalletResult = new WalletView();
    private WalletView _importedWalletResult;
    private WalletView _generatedWalletResult;
    private readonly string _encryptedPrivateKey = "testPrivateKey";

    public WalletTestable() {
        _walletRepositoryMock = new Mock<IWalletRepository>();
        _walletHandlerMock = new Mock<IGetWalletHandler>();
        _mapper = Mapper.Instance;
        _cts = new CancellationTokenSource();
        _cancellationToken = _cts.Token;
        _wallets = new[] { _entity };
        _walletServiceMock = new Mock<IWalletService>();
        _dispatcherMock = new Mock<IDispatcher>();
    }

    #region Create

    public async Task<WalletView> CallCreate(CreateWalletCommand createWalletCommand) {
        _walletRepositoryMock.Setup(m => m.GetAll()).Returns(() => _wallets.AsAsyncQueryable());
        _walletRepositoryMock.Setup(m => m.Where(It.IsAny<Expression<Func<Wallet, bool>>>())).Returns(() => _wallets.AsAsyncQueryable());
        _walletRepositoryMock.Setup(m => m.Add(It.IsAny<Wallet>())).Callback<Wallet>(e => _entity = e);
        _walletHandlerMock.Setup(m => m.Handle(It.IsAny<GetWalletRequest>(), It.IsIn(_cts.Token))).ReturnsAsync(new WalletView());
        _walletServiceMock.Setup(m => m.EncryptPrivateKey(It.IsAny<string>(), It.IsAny<Guid>())).Returns(_encryptedPrivateKey);

        var createWalletHandler = new CreateWalletHandler(_mapper, _walletRepositoryMock.Object,
            _walletHandlerMock.Object, _walletServiceMock.Object);
        return await createWalletHandler.Handle(createWalletCommand, _cancellationToken);
    }

    public void VerifyAdd() {
        _walletRepositoryMock.Verify(a => a.Add(_entity), () => Times.Once());
        _walletRepositoryMock.Verify(m => m.SaveAsync(It.IsIn(_cancellationToken)), Times.Once);
        _entity.PrivateKey.ShouldBeEquivalentTo(_encryptedPrivateKey);
    }

    public void VerifyReturnView() {
        _walletHandlerMock.Verify(m => m.Handle(It.Is<GetWalletRequest>(r => r.Id == _entity.Id), It.IsIn(_cts.Token)), Times.Once);
    }

    #endregion

    #region Generate

    public async Task<WalletView> CallGenerate(GenerateWalletCommand generateWalletCommand) {
        _walletServiceMock.Setup(m => m.GenerateWallet(_cancellationToken)).ReturnsAsync(_generatedWalletView);

        _dispatcherMock.Setup(m => m.Send(It.IsAny<CreateGeneratedWalletCommand>(), It.IsIn<CancellationToken>(_cancellationToken)))
                       .ReturnsAsync(_createWalletResult);


        var generateWalletHandler = new GenerateWalletHandler(_walletServiceMock.Object, _dispatcherMock.Object, _mapper);
        var result = await generateWalletHandler.Handle(generateWalletCommand, _cancellationToken);
        return _generatedWalletResult = result;
    }

    public void VerifyGenerate() {
        _walletServiceMock.Verify(a => a.GenerateWallet(_cancellationToken), Times.Once);
        _dispatcherMock.Verify(a => a.Send(It.IsAny<CreateGeneratedWalletCommand>(), It.IsIn(_cancellationToken)), Times.Once);
        _generatedWalletResult.ShouldBeEquivalentTo(_createWalletResult);
    }

    #endregion

    #region Import

    public async Task<WalletView> CallImport(ImportWalletCommand importWalletCommand) {
        _dispatcherMock.Setup(m => m.Send(It.IsAny<CreateImportedWalletCommand>(), It.IsIn<CancellationToken>(_cancellationToken)))
                       .ReturnsAsync(_createWalletResult);
        var importWalletHandler = new ImportWalletHandler(_dispatcherMock.Object, _mapper);
        var result = await importWalletHandler.Handle(importWalletCommand, _cancellationToken);
        return _importedWalletResult = result;
    }

    public void VerifyImport() {
        _dispatcherMock.Verify(a => a.Send(It.IsAny<CreateImportedWalletCommand>(), It.IsIn(_cancellationToken)));
        _importedWalletResult.ShouldBeEquivalentTo(_createWalletResult);
    }

    #endregion

    public void Dispose() {
    }
}