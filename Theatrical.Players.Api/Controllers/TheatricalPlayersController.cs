using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Commands.PrintInvoice;

namespace Theatrical.Players.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatricalPlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TheatricalPlayersController(
            IMediator mediator
        ) =>       
            _mediator = mediator;
        

        [HttpPost("create")]
        public async Task<PrintInvoiceCommandResponse> Post() 
        {
            return await _mediator.Send(new PrintInvoiceCommand());
        }
    }
}
