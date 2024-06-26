using App.Core.Commands.Settings;
using App.Core.Requests.Settings;
using App.Core.ViewModels.Settings;
using App.Interfaces.Core;
using App.Web.Core;

using AutoMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers; 

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiVersion("1.0")]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class SettingsController : BaseController {
    private readonly IDispatcher _dispatcher;
    private readonly IMapper _mapper;

    public SettingsController(IDispatcher dispatcher, IMapper mapper) {
        _dispatcher = dispatcher;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(SettingPropertyView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSettingsAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetSettings(), cancellationToken));
    }

    [HttpGet("{typeId:int}")]
    [ProducesResponseType(typeof(SettingPropertyView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSettingPropertyAsync([FromRoute] int   typeId,
                                                             CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetSettingProperty(typeId), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(typeof(SettingPropertyView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSettingPropertyAsync([FromBody] UpdateSettingProperty request,
                                                                CancellationToken                cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }
}