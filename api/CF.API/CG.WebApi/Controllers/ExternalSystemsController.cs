using App.Core.Commands.Database;
using App.Core.Commands.ExternalSystems.Referrals;
using App.Core.Commands.ExternalSystems.Users;
using App.Core.Exceptions;
using App.Core.Requests.Companies;
using App.Core.Requests.ReferralPairs;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.ReferralPairs;
using App.Core.ViewModels.Users;
using App.Data.Entities.Companies;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Resources;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]/{customerSecret}")]
[ApiController]
[AllowAnonymous]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class ExternalSystemsController : BaseController
{
    private readonly IDispatcher _dispatcher; 

    public ExternalSystemsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("referrals/export")]
    [ProducesResponseType(typeof(ReferralPairView), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportReferralAsync(string customerSecret,
        [FromBody] CreateReferralCommand request,
        CancellationToken cancellationToken
    )
    {
        var customerId = await GetCustomerId(customerSecret, cancellationToken);
        request.ImportedFromId = customerId;

        var result = await _dispatcher.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("referrals/export/batch")]
    [ProducesResponseType(typeof(ReferralPairView[]), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportReferralsAsync(string customerSecret,
        [FromBody] CreateReferralsCommand request,
        CancellationToken cancellationToken
    )
    {
        var customerId = await GetCustomerId(customerSecret, cancellationToken);

        foreach (var createReferralCommand in request.Referrals)
            createReferralCommand.ImportedFromId = customerId;


        var result = await _dispatcher.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("referrals/list")]
    [ProducesResponseType(typeof(IPagedList<ReferralPairView>), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetReferralsAsync(string customerSecret,
        [FromBody] GetReferralPairsExternalRequest externalRequest,
        CancellationToken cancellationToken
    )
    {
        var customerId = await GetCustomerId(customerSecret, cancellationToken);

        var request = new GetReferralPairsRequest(externalRequest)
        {
            CompanyId = customerId
        };
        var result = await _dispatcher.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("users/block")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> BlockUsersAsync(string customerSecret,
        [FromBody] BlockUsersCommand request,
        CancellationToken cancellationToken
    )
    {
        var customerId = await GetCustomerId(customerSecret, cancellationToken);

        await _dispatcher.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("users/restore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> RestoreUsersAsync(string customerSecret,
        [FromBody] RestoreUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var customerId = await GetCustomerId(customerSecret, cancellationToken);

        await _dispatcher.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("users/{userId:guid}")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> CompleteTaskAsync(string customerSecret,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        await GetCustomerId(customerSecret, cancellationToken);
        return Ok(await _dispatcher.Send(new GetUserProfileByUbikiriIdRequest(userId), cancellationToken));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportAsync(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportDataFromPromobuzzCommand(), cancellationToken));
    }


    [HttpGet]
    [AllowAnonymous]
    [Route("import/users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportUsersAsync(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportUsersCommand(), cancellationToken));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("users/sync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> SyncUsersIdsAsync(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new SyncUsersUbikiriIdAndIdCommand(), cancellationToken));
    }   
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/attachments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportAttachmentsAsync(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportAttachmentsCommand(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/chatmessages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportChatMessages(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportChatMessages(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/notifications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportNotifications(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportNotifications(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/tasks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTasks(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTasks(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/requiredattachments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportRequiredAttachments(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportRequiredAttachments(), cancellationToken));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTaskExecutions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTaskExecutions(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTaskExecutions(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTaskExecutionsHistory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTaskExecutionsHistory(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTaskExecutionsHistory(), cancellationToken));
    }
    
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTaskExecutionNotes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTaskExecutionNotes(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTaskExecutionNotes(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTaskHistories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTaskHistories(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTaskHistories(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTaskSteps")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTaskSteps(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTaskSteps(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportTelegramMessages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportTelegramMessages(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportTelegramMessages(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportWallets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportWallets(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportWallets(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportAuditLogs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportAuditLogs(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportAuditLogs(), cancellationToken));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("import/ImportFollows")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> ImportFollows(string customerSecret, CancellationToken cancellationToken)
    {
        if (customerSecret != "ART*123*456*!!!!ABZ")
            throw new ArgumentException("Incorrect secret");

        return Ok(await _dispatcher.Send(new ImportFollows(), cancellationToken));
    }
    

    private async Task<Guid> GetCustomerId(string customerSecret, CancellationToken cancellationToken)
    {
        var customer =
            await _dispatcher.Send(new GetCustomerCompanyRequest {Secret = customerSecret}, cancellationToken);

        if (customer == null)
            throw new BadRequestException(nameof(CustomerCompany),
                new Exception(string.Format(SecurityErrorMessages.UnknownSecurityCode, customerSecret)));
        return customer.Id;
    }
}