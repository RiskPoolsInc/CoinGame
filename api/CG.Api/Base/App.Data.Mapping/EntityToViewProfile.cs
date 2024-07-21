using App.Core.Enums;
using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using App.Core.ViewModels.Audits;
using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Notifications;
using App.Core.ViewModels.Security;
using App.Core.ViewModels.Transactions;
using App.Core.ViewModels.Users;
using App.Core.ViewModels.Wallets;
using App.Data.Entities;
using App.Data.Entities.Attachments;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Games;
using App.Data.Entities.Notifications;
using App.Data.Entities.TransactionReceiver;
using App.Data.Entities.Transactions;
using App.Data.Entities.UserProfiles;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class EntityToViewProfile : Profile
{
    public EntityToViewProfile()
    {
        #region UserProfiles

        CreateMap<UserProfile, UserProfileView>();

        #endregion

        #region Games

        CreateMap<Game, GameView>();
        CreateMap<GameRound, GameRoundView>();

        #endregion
        
        
        #region Transaction

        CreateMap<Transaction, TransactionView>();
        CreateMap<TransactionReceiver, TransactionReceiverView>();

        #endregion

        #region Attachments

        CreateMap<Attachment, AttachmentView>()
            .ForMember(m => m.ObjectType, e => e.MapFrom(m => m.ObjectType))
            .ForMember(m => m.FileName, e => e.MapFrom(m => m.OriginalFileName));

        CreateMap<Attachment, ExternalFile>()
            .ForMember(m => m.ObjectType, e => e.MapFrom(m => (ObjectTypes) m.ObjectTypeId))
            .ForMember(m => m.DisplayName, e => e.MapFrom(m => m.OriginalFileName));

        #endregion


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

        CreateMap<UserProfile, BaseUserView>();

        CreateMap<UserProfile, UserTinyView>();

        CreateMap<UserProfile, UserProfileView>()
            .IncludeBase<UserProfile, BaseUserView>();

        CreateMap<UserProfile, UserBaseView>();

        CreateMap<UserProfile, UserView>()
            .IncludeBase<UserProfile, UserBaseView>();

        #endregion

        #region Follow
        
        CreateMap<UserFollow, FollowView>()
            .ForMember(m => m.Id, e => e.MapFrom(m => m.UserId));

        CreateMap<Notification, NotificationView>();

        #endregion

        #region AuditLog

        CreateMap<AuditLog, AuditLogView>();

        #endregion

        #region Wallets

        CreateMap<Wallet, WalletView>();
        CreateMap<Wallet, WalletBalanceView>()
           .ForMember(a => a.Address, opt => opt.MapFrom(s => s.Hash));

        #endregion
    }
}