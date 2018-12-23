using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GrammarWorkbook.UseCases.Units
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitForExercise([FromRoute] GetUnitForExercise.Input input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckResults([FromBody] Dto.Unit unit)
        {
            var result = await _mediator.Send(new CheckResults.Input() {Unit = unit});
            return Ok(result);
        }
    }
}