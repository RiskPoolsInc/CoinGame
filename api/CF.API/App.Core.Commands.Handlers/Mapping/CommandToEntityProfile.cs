using App.Core.Commands.Attachments;
using App.Core.Commands.Security;
using App.Core.Commands.Wallets;
using App.Data.Entities;
using App.Data.Entities.Wallets;

using Attachment = App.Data.Entities.Attachments.Attachment;
using AuditLogAttachment = App.Data.Entities.Attachments.AuditLogAttachment;

namespace App.Core.Commands.Handlers.Mapping
{
    public class CommandToEntityProfile : Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<DictionaryItem, DictionaryName>();

            #region Attachments

            CreateMap<AttachmentCommand, Attachment>()
                .ForMember(m => m.ObjectType, e => e.Ignore())
                .ForMember(m => m.ObjectTypeId, e => e.MapFrom(m => (int) m.ObjectType))
                .ForMember(m => m.OriginalFileName, e => e.MapFrom(m => m.File.FileName));

            CreateMap<CreateAuditLogAttachCommand, AuditLogAttachment>()
                .IncludeBase<AttachmentCommand, Attachment>();

            #endregion
 
            #region Wallets

            CreateMap<CreateWalletCommand, Wallet>();

            #endregion
        }
    }
}