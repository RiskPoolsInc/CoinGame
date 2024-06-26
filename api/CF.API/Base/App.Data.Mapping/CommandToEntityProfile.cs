using App.Core.Commands.Attachments;
using App.Core.Commands.UserProfiles;
using App.Core.Commands.Wallets;
using App.Data.Entities;
using App.Data.Entities.Attachments;
using App.Data.Entities.UserProfiles;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class CommandToEntityProfile : Profile {
    public CommandToEntityProfile() {

        #region Attachments

        CreateMap<AttachmentCommand, Attachment>()
           .ForMember(m => m.ObjectType, e => e.Ignore())
           .ForMember(m => m.ObjectTypeId, e => e.MapFrom(m => (int)m.ObjectType))
           .ForMember(m => m.OriginalFileName, e => e.MapFrom(m => m.File.FileName));

        #endregion

        CreateMap<UpdateUserProfileWalletCommand, UserProfile>()
           .ForMember(a => a.UbikiriUserId, opt => opt.Ignore());

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