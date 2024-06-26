using App.Interfaces.Core;
using App.Services.WalletService;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[AllowAnonymous]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class UbikiriController : BaseController {
    private readonly IDispatcher _dispatcher;
    private readonly UbikiriWalletService _ubikiriWalletService;

    public UbikiriController(IDispatcher dispatcher, UbikiriWalletService ubikiriWalletService) {
        _dispatcher = dispatcher;
        _ubikiriWalletService = ubikiriWalletService;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("login")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> Login(CancellationToken cancellationToken) {
        await _ubikiriWalletService.Login(cancellationToken);
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("renew")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> Renew(CancellationToken cancellationToken) {
        await _ubikiriWalletService.RenewRefreshToken(cancellationToken);
        return Ok();
    }
}