using App.Core.Commands.Wallets;
using App.Core.ViewModels.Wallets;
using App.Interfaces.Core;

namespace App.Core.Commands.Handlers.Wallets;

public class ImportWalletHandler : IRequestHandler<ImportWalletCommand, WalletView> {
    private readonly IDispatcher _dispatcher;
    private readonly IMapper _mapper;

    public ImportWalletHandler(IDispatcher dispatcher, IMapper mapper) {
        _dispatcher = dispatcher;
        _mapper = mapper;
    }

    public Task<WalletView> Handle(ImportWalletCommand request, CancellationToken cancellationToken) {
        var createWalletCommand = _mapper.Map<CreateImportedWalletCommand>(request);
        return _dispatcher.Send(createWalletCommand, cancellationToken);
    }
}