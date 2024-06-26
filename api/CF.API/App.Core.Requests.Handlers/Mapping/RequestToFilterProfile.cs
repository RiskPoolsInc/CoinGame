using App.Core.Requests.PropertyHistories;
using App.Core.Requests.TaskExecutions;
using App.Core.Requests.Tasks;
using App.Data.Criterias.PropertyHistories;
using App.Data.Criterias.TaskExecutions;
using App.Data.Criterias.Tasks;
using AutoMapper;

namespace App.Core.Requests.Handlers.Mapping
{
    public class RequestToFilterProfile : Profile
    {
        public RequestToFilterProfile()
        {
            CreateMap<GetTaskExecutionsRequest, TaskExecutionFilter>();
            CreateMap<GetTasksRequest, TaskFilter>();
            CreateMap<GetTaskExecutionsPagedRequest, TaskExecutionPagedFilter>();

            #region BatchPropertyHistory

            CreateMap<BatchPropertyHistoryRequest,PropertyHistoryFilter>();

            CreateMap<GetBatchHistoryBooleanRequest, PropertyHistoryFilter>()
                .IncludeBase<BatchPropertyHistoryRequest, PropertyHistoryFilter>();
            
            
            CreateMap<GetBatchHistoryIntRequest, PropertyHistoryFilter>()
                .IncludeBase<BatchPropertyHistoryRequest, PropertyHistoryFilter>();
            #endregion

            #region PropertyHistory
            CreateMap<PropertyHistoryRequest,PropertyHistoryFilter>();

            CreateMap<GetHistoryBooleanRequest, PropertyHistoryFilter>()
                .IncludeBase<PropertyHistoryRequest, PropertyHistoryFilter>();
            CreateMap<GetHistoryIntRequest, PropertyHistoryFilter>()
                .IncludeBase<PropertyHistoryRequest, PropertyHistoryFilter>();

            #endregion
            
        }
    }
}