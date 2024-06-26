using App.Core.Commands.Settings;
using App.Core.ViewModels.Settings;

using MediatR;

namespace App.Interfaces.Handlers.CommandHandlers;

public interface IUpdateTaxWalletHandler : IRequestHandler<UpdateTaxWalletCommand, SettingPropertyView> {
}