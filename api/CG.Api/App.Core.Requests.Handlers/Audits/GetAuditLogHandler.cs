using App.Core.Requests.Audits;
using App.Core.Requests.Handlers.Helpers;
using App.Core.ViewModels;
using App.Core.ViewModels.Audits;
using App.Data.Criterias.AuditLogs;
using App.Data.Entities;
using App.Interfaces.Core.Requests;
using App.Interfaces.Repositories;
using AutoMapper;

namespace App.Core.Requests.Handlers.Audits {

public class GetAuditLogHandler : PagedRequestHandler<GetAuditLogRequest, AuditLogView>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IMapper _mapper;

    public GetAuditLogHandler(IAuditLogRepository auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public override Task<IPagedList<AuditLogView>> Handle(GetAuditLogRequest request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<AuditLogFilter>(request);
        return _auditLogRepository.Where(filter.Build()).ToLookupAsync<AuditLog, AuditLogView>(filter, cancellationToken);
    }
}
}