using App.Core.Enums;
using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using App.Core.ViewModels.Audits;
using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Wallets;
using App.Data.Entities;
using App.Data.Entities.Attachments;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Core.Requests.Handlers.Mapping
{
    public class EntityToViewProfile : Profile
    {
        public EntityToViewProfile()
        {

            #region Attachments

            CreateMap<Attachment, AttachmentView>()
                .ForMember(m => m.ObjectType, e => e.MapFrom(m => m.ObjectType))
                .ForMember(m => m.FileName, e => e.MapFrom(m => m.OriginalFileName));

            CreateMap<AuditLogAttachment, AttachmentView>()
                .IncludeBase<Attachment, AttachmentView>();

            CreateMap<Attachment, ExternalFile>()
                .ForMember(m => m.ObjectType, e => e.MapFrom(m => (ObjectTypes) m.ObjectTypeId))
                .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.OriginalFileName));

            #endregion

            #region Users

            CreateMap<UserType, UserTypeView>();

            #endregion

            #region AuditLog

            CreateMap<AuditLog, AuditLogView>();

            #endregion

            #region Wallets

            CreateMap<Wallet, WalletView>();

            #endregion
        }
    }
}