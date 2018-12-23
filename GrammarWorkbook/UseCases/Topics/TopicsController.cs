using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GrammarWorkbook.UseCases.Topics
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic([FromRoute] GetTopic.Input input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTopic(SaveTopic.Input input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }
    }
}