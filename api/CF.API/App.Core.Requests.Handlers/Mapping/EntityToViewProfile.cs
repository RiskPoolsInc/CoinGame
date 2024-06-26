using App.Core.Enums;
using App.Core.Requests.Handlers.Helpers;
using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using App.Core.ViewModels.Audits;
using App.Core.ViewModels.ChatMessages;
using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Log;
using App.Core.ViewModels.Log.Properties;
using App.Core.ViewModels.Notifications;
using App.Core.ViewModels.Security;
using App.Core.ViewModels.TaskExecutions;
using App.Core.ViewModels.Tasks;
using App.Core.ViewModels.TaskTakeRequests;
using App.Core.ViewModels.Wallets;
using App.Data.Entities;
using App.Data.Entities.Attachments;
using App.Data.Entities.ChatMessages;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Notifications;
using App.Data.Entities.PropertyHistories;
using App.Data.Entities.TaskExecutions;
using App.Data.Entities.Tasks;
using App.Data.Entities.TaskTakeRequests;
using App.Data.Entities.Wallets;
using AutoMapper;

namespace App.Core.Requests.Handlers.Mapping
{
    public class EntityToViewProfile : Profile
    {
        public EntityToViewProfile()
        {
            CreateMap<FilterEntityView, FilterEntityView>();
            CreateMap<FilterEntityEnumView, FilterEntityView>();

            CreateMap<TaskExecutionState, TaskExecutionStateView>();
            CreateMap<TaskExecutionStatus, TaskExecutionStatusView>();

            CreateMap<TaskTakeRequest, TakeRequestBaseView>()
                .ForMember(a => a.TaskId, opt => opt.MapFrom(s => s.TaskId))
                .ForMember(a => a.UserId, opt => opt.MapFrom(s => s.UserProfileId))
                .ForMember(a => a.UserLinkedWallet, opt => opt.MapFrom(s => s.UserProfile.LinkedWallet))
                .ForMember(a => a.StateId, opt => opt.MapFrom(s => s.StateId))
                ;

            #region Attachments

            CreateMap<Attachment, AttachmentView>()
                .ForMember(m => m.ObjectType, e => e.MapFrom(m => m.ObjectType))
                .ForMember(m => m.FileName, e => e.MapFrom(m => m.OriginalFileName));

            CreateMap<TaskAttachment, AttachmentView>()
                .IncludeBase<Attachment, AttachmentView>();

            CreateMap<AuditLogAttachment, AttachmentView>()
                .IncludeBase<Attachment, AttachmentView>();

            CreateMap<TaskExecutionNoteAttachment, AttachmentView>()
                .IncludeBase<Attachment, AttachmentView>();

            CreateMap<Attachment, ExternalFile>()
                .ForMember(m => m.ObjectType, e => e.MapFrom(m => (ObjectTypes) m.ObjectTypeId))
                .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.OriginalFileName));

            #endregion

            #region Users

            CreateMap<UserType, UserTypeView>();

            #endregion

            #region Chat messages

            CreateMap<ChatObjectType, ChatObjectTypeView>();
            CreateMap<ChatMessage, ChatMessageView>();

            #endregion

            #region Tasks

            CreateMap<TaskSubject, TaskSubjectView>();

            CreateMap<BaseTask, TaskBasicView>()
                .ForMember(a => a.CountExecutionsByStateId, opt => opt.Ignore());

            CreateMap<TaskEntity, TaskBasicView>()
                .IncludeBase<BaseTask, TaskBasicView>();

            CreateMap<BaseTask, TaskView>()
                .IncludeBase<BaseTask, TaskBasicView>();
            CreateMap<TaskEntity, TaskView>()
                .IncludeBase<TaskEntity, TaskBasicView>();


            CreateMap<TaskEntity, TaskGridView>()
                .ForMember(m => m.CreatedBy,
                    o =>
                        o.MapFrom(m => $"{(m.CreatedByUser != null ? m.CreatedByUser.Email : "NoName")}"))
                .ForMember(m => m.State, o => o.MapFrom(m => m.State.Name));

            CreateMap<TaskHistory, TaskHistoryView>();

            CreateMap<TaskNote, TaskNoteView>();

            CreateMap<TaskType, FilterEntityView>()
                .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.Name));

            CreateMap<TaskStateType, FilterEntityView>()
                .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.Name));

            CreateMap<TaskEntity, TaskListView>()
                .ForMember(a => a.CreatedById, opt => opt.MapFrom(a => a.CreatedBy.Id));

            #endregion

            #region TaskExecution

            CreateMap<TaskExecution, TaskExecutionView>();
            CreateMap<TaskExecution, TaskExecutionTinyView>()
                .ForMember(a => a.Title,
                    opt => opt.MapFrom(s => s.Task.Title));

            CreateMap<TaskExecutionNote, TaskExecutionNoteView>();

            #endregion

            #region Follow

            CreateMap<TaskFollow, FollowView>()
                .ForMember(m => m.Id, e => e.MapFrom(m => m.TaskId));

            CreateMap<UserFollow, FollowView>()
                .ForMember(m => m.Id, e => e.MapFrom(m => m.UserId));

            CreateMap<Notification, NotificationView>();

            #endregion

            #region AuditLog

            CreateMap<AuditLog, AuditLogView>();

            #endregion

            #region Wallets

            CreateMap<Wallet, WalletView>();

            #endregion

            #region PropertyHistory

            CreateMap<PropertyHistory, BasePropertyHistoryView>();

            CreateMap<PropertyHistory, PropertyHistoryView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();

            CreateMap<PropertyHistoryView, HistoryBooleanView>();
            CreateMap<PropertyHistoryView, HistoryDoubleView>();
            CreateMap<PropertyHistoryView, HistoryGuidView>();
            CreateMap<PropertyHistoryView, HistoryIntView>();
            CreateMap<PropertyHistoryView, HistoryStringView>();

            CreateMap<PropertyHistory, HistoryBooleanView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();
            CreateMap<PropertyHistory, HistoryDoubleView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();
            CreateMap<PropertyHistory, HistoryGuidView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();
            CreateMap<PropertyHistory, HistoryIntView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();
            CreateMap<PropertyHistory, HistoryStringView>()
                .IncludeBase<PropertyHistory, BasePropertyHistoryView>();

            #endregion

            CreateMap<TaskStateType, FilterEntityView>()
                .ForMember(m => m.Id, e => e.MapFrom(m => m.Id))
                .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.Name));
        }
    }
}