using App.Interfaces.Core;
using App.Interfaces.Repositories;
using CF.App.ViewModels.Wallets;
using CF.Core.Commands.ExternalSystems.Wallets;
using CF.Core.Commands.Wallets;
using CF.Core.Requests.Wallets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CF.Core.Commands.Handlers.Wallets;

public class TryUpdateWalletAddressHandler : IRequestHandler<TryUpdateWalletAddressCommand, WalletView>
{
    private readonly IAppDispatcher _dispatcher;
    private readonly ITaskRepository _taskRepository;

    public TryUpdateWalletAddressHandler(IAppDispatcher dispatcher, ITaskRepository taskRepository)
    {
        _dispatcher = dispatcher;
        _taskRepository = taskRepository;
    }

    public async Task<WalletView> Handle(TryUpdateWalletAddressCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Where(a => a.WalletId == request.WalletId).SingleAsync(cancellationToken);
        try
        {
            var address = await _dispatcher.Send(new SendCreateWalletCommand() {Name = task.Title}, cancellationToken);
            return await _dispatcher.Send(new UpdateWalletAddressCommand()
            {
                Id = request.WalletId,
                Address = address
            }, cancellationToken);
        }
        catch (Exception e)
        {
            return await _dispatcher.Send(new GetWalletRequest() {Id = request.WalletId}, cancellationToken);
        }
    }
}