using App.Core.Commands.Settings;
using App.Core.ViewModels.Settings;

using MediatR;

namespace App.Interfaces.Handlers.CommandHandlers;

public interface IUpdateDefaultReferralAwardHandler : IRequestHandler<UpdateDefaultReferralAwardCommand, SettingPropertyView> {
}