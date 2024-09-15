using System.Linq.Expressions;

using App.Common.Helpers;
using App.Core.Commands.Handlers.Wallets;
using App.Core.Commands.Wallets;
using App.Core.Enums;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Data.Sql;
using App.Interfaces.Core;
using App.Interfaces.Handlers;
using App.Interfaces.Repositories.Wallets;
using App.Repositories.Wallets;
using App.Services.WalletService;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

using MockQueryable;

using Moq;

using ShouldBeLike;

using Shouldly;

using TNET.Tests.Helpers;

namespace CG.Tests.Commands.Handlers.Cases;

public class WalletTestable : IDisposable {
    private Mock<IWalletRepository> _walletRepository;
    private Mock<IGetWalletHandler> _walletHandler;
    private CancellationToken _cancellationToken;
    private CancellationTokenSource _cts;
    private IMapper _mapper;
    private Wallet _entity = new Wallet() { Id = Guid.NewGuid() };
    private Guid _entityId = Guid.Empty;
    private readonly Wallet[] _wallets;                   // = new List<Wallet>() { _entity }.AsQueryable();
    private readonly Mock<IAsyncQueryProvider> _provider; // = new List<Wallet>() { _entity }.AsQueryable();
    private readonly Mock<IWalletService> _walletService;
    private readonly Mock<IDispatcher> _dispatcher;

    private readonly GeneratedWalletView _generatedWalletView = new GeneratedWalletView() {
        Address = "aaassdddc",
        PrivateKey = "testPrivateKey"
    };

    private CreateGeneratedWalletCommand _createWalletCommand;
    private WalletView _createWalletResult = new WalletView();
    private WalletView _importedWalletResult;
    private WalletView _generatedWalletResult;

    public WalletTestable() {
        _walletRepository = new Mock<IWalletRepository>();
        _walletHandler = new Mock<IGetWalletHandler>();
        _mapper = Mapper.Instance;
        _cts = new CancellationTokenSource();
        _cancellationToken = _cts.Token;
        _wallets = new[] { _entity };
        _provider = new Mock<IAsyncQueryProvider>();
        _walletService = new Mock<IWalletService>();
        _dispatcher = new Mock<IDispatcher>();
    }

    #region Create

    public async Task<WalletView> CallCreate(CreateWalletCommand createWalletCommand) {
        _walletRepository.Setup(m => m.GetAll()).Returns(() => _wallets.AsAsyncQueryable());
        _walletRepository.Setup(m => m.Where(It.IsAny<Expression<Func<Wallet, bool>>>())).Returns(() => _wallets.AsAsyncQueryable());
        _walletRepository.Setup(m => m.Add(It.IsAny<Wallet>())).Callback<Wallet>(e => _entity = e);
        _walletHandler.Setup(m => m.Handle(It.IsAny<GetWalletRequest>(), It.IsIn(_cts.Token))).ReturnsAsync(new WalletView());

        var createWalletHandler = new CreateWalletHandler(_mapper, _walletRepository.Object, _walletHandler.Object);
        return await createWalletHandler.Handle(createWalletCommand, _cancellationToken);
    }

    public void VerifyAdd() {
        _walletRepository.Verify(a => a.Add(_entity), () => Times.Once());
        _walletRepository.Verify(m => m.SaveAsync(It.IsIn(_cancellationToken)), Times.Once);
    }

    public void VerifyReturnView() {
        _walletHandler.Verify(m => m.Handle(It.Is<GetWalletRequest>(r => r.Id == _entity.Id), It.IsIn(_cts.Token)), Times.Once);
    }

    #endregion

    #region Generate

    public async Task<WalletView> CallGenerate(GenerateWalletCommand generateWalletCommand) {
        _walletService.Setup(m => m.GenerateWallet(_cancellationToken)).ReturnsAsync(_generatedWalletView);

        _dispatcher.Setup(m => m.Send(It.IsAny<CreateGeneratedWalletCommand>(), It.IsIn<CancellationToken>(_cancellationToken)))
                   .ReturnsAsync(_createWalletResult);


        var generateWalletHandler = new GenerateWalletHandler(_walletService.Object, _dispatcher.Object, _mapper);
        var result = await generateWalletHandler.Handle(generateWalletCommand, _cancellationToken);
        return _generatedWalletResult = result;
    }

    public void VerifyGenerate() {
        _walletService.Verify(a => a.GenerateWallet(_cancellationToken), Times.Once);
        _dispatcher.Verify(a => a.Send(It.IsAny<CreateGeneratedWalletCommand>(), It.IsIn(_cancellationToken)), Times.Once);
        _generatedWalletResult.ShouldBeEquivalentTo(_createWalletResult);
    }

    #endregion

    #region Import

    public async Task<WalletView> CallImport(ImportWalletCommand importWalletCommand) {
        _dispatcher.Setup(m => m.Send(It.IsAny<CreateImportedWalletCommand>(), It.IsIn<CancellationToken>(_cancellationToken)))
                   .ReturnsAsync(_createWalletResult);
        var importWalletHandler = new ImportWalletHandler(_dispatcher.Object, _mapper);
        var result = await importWalletHandler.Handle(importWalletCommand, _cancellationToken);
        return _importedWalletResult = result;
    }

    public void VerifyImport() {
        _dispatcher.Verify(a => a.Send(It.IsAny<CreateImportedWalletCommand>(), It.IsIn(_cancellationToken)));
        _importedWalletResult.ShouldBeEquivalentTo(_createWalletResult);
    }

    #endregion

    public void Dispose() {
    }
}