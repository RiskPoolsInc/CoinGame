using App.Core.Commands.ChatMessages;
using App.Core.Requests.ChatMessages;
using App.Core.ViewModels.ChatMessages;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/chat"),
     Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class ChatContoller : BaseController
    {
        public ChatContoller(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        private readonly IDispatcher _dispatcher;

        /// <summary>
        /// Get chat messages
        /// </summary>
        /// <remarks>Method return chat messages of execution</remarks>
        /// <param name="id">Task execution id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created wallet</returns>
        /// <response code="200"></response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ChatMessageView[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExecutionChatMessages([FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var request = new GetChatMessages(id);
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Add chat message  
        /// </summary>
        /// <remarks>Method create chat message</remarks>
        /// <param name="request">Chat message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created wallet</returns>
        /// <response code="200"></response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ChatMessageView), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExecutionChatMessage([FromBody] AddChatMessageCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}