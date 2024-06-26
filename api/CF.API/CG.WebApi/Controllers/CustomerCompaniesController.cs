using App.Core.Commands.CustomerCompanies;
using App.Core.Requests.Companies;
using App.Core.ViewModels.CustomerCompanies;
using App.Interfaces.Core;
using App.Web.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CustomerCompaniesController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CustomerCompaniesController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(CustomerCompanyBaseView), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }   
    
    [HttpPost]
    [Route("create-by-user")]
    [ProducesResponseType(typeof(CustomerCompanyBaseView), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateByUserAsync([FromBody] CreateCompanyByUserCommand request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("update")]
    [ProducesResponseType(typeof(CustomerCompanyBaseView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerCompanyCommand request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpDelete]
    [Route("{id:guid}/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(new DeleteCustomerCompanyCommand(id), cancellationToken));
    }

    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(CustomerCompanyBaseView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCompaniesAsync(CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(new GetCustomerCompaniesRequest(), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerCompanyBaseView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompaniesAsync([FromBody] GetCustomerCompaniesRequest request,
        CancellationToken cancellationToken
    )
    {
        var companies = await _dispatcher.Send(request, cancellationToken);
        return Ok(companies);
    }
}