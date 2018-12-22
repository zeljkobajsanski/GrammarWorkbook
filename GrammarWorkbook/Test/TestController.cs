using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrammarWorkbook.Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Test([FromQuery] Test.Input input)
        {
            var output = await _mediator.Send(input);
            return Ok(output.Message);
        }
    }

    public class Test
    {
        public class Input : IRequest<Output> { }

        public class Output
        {
            public string Message { get; set; }
        }

        public class Handler : RequestHandler<Input, Output>
        {
            protected override Output Handle(Input request)
            {
                return new Output() {Message = "It works fine"};
            }
        }
    }
}