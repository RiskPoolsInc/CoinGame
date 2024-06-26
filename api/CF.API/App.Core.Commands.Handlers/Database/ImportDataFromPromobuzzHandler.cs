using App.Core.Commands.Database;
using App.Interfaces.Core;

namespace App.Core.Commands.Handlers.Database;

public class ImportDataFromPromobuzzHandler : IRequestHandler<ImportDataFromPromobuzzCommand, Unit>
{
    private readonly IDispatcher _dispatcher;

    public ImportDataFromPromobuzzHandler(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<Unit> Handle(ImportDataFromPromobuzzCommand request, CancellationToken cancellationToken)
    {
        // await _dispatcher.Send(new ImportUsersCommand(), cancellationToken);
        // await _dispatcher.Send(new ImportAttachmentsCommand(), cancellationToken);
        await _dispatcher.Send(new ImportChatMessages(), cancellationToken);
        await _dispatcher.Send(new ImportNotifications(), cancellationToken);
        await _dispatcher.Send(new ImportTasks(), cancellationToken);
        await _dispatcher.Send(new ImportRequiredAttachments(), cancellationToken);
        // await _dispatcher.Send(new ImportTaskExecutions(), cancellationToken);
        await _dispatcher.Send(new ImportTaskExecutionsHistory(), cancellationToken);
        // await _dispatcher.Send(new ImportTaskExecutionNotes(), cancellationToken);
        await _dispatcher.Send(new ImportTaskHistories(), cancellationToken);
        await _dispatcher.Send(new ImportTaskSteps(), cancellationToken);
        await _dispatcher.Send(new ImportTelegramMessages(), cancellationToken);
        await _dispatcher.Send(new ImportWallets(), cancellationToken);
        await _dispatcher.Send(new ImportAuditLogs(), cancellationToken);
        await _dispatcher.Send(new ImportFollows(), cancellationToken);
        
        return Unit.Value;
    }
}