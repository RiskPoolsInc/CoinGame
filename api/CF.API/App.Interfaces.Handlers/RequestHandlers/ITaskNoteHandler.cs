using App.Core.Requests.Tasks;
using App.Core.ViewModels.Tasks;
using MediatR;

namespace App.Interfaces.Handlers.RequestHandlers {

public interface ITaskNoteHandler : IRequestHandler<GetTaskNoteRequest, TaskNoteView>
{
}
}