using App.Core.Enums;
using App.Core.Requests.PropertyHistories;
using App.Core.Requests.Users;
using App.Core.ViewModels.ChangeReasons;
using App.Interfaces.Core;
using MediatR;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserBlockHistoryHandler : IRequestHandler<GetUserBlockHistoryRequest, ChangeReasonView?>
{
    private readonly IDispatcher _dispatcher;

    public GetUserBlockHistoryHandler(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<ChangeReasonView?> Handle(GetUserBlockHistoryRequest request, CancellationToken cancellationToken)
    {
        var lastBlock = await _dispatcher.Send(new GetHistoryBooleanRequest()
        {
            ObjectId = request.UserId,
            ObjectTypeId = (int) ObjectTypes.User,
            PropertyTypeId = (int) PropertyTypes.IsBlocked
        });
        return lastBlock.OrderByDescending(a => a.CreatedOn).FirstOrDefault()?.ChangeReason;
    }
}