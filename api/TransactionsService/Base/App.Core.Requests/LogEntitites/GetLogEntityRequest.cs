using App.Core.ViewModels.LogEntities;

namespace App.Core.Requests.LogEntitites;

public class GetLogEntityRequest : IRequest<LogEntityView> {
    public Guid Id { get; set; }

    public GetLogEntityRequest() {
    }

    public GetLogEntityRequest(Guid id) {
        Id = id;
    }
}