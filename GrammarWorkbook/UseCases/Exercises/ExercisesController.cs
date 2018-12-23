using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GrammarWorkbook.UseCases.Exercises
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExercisesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> SaveExercise([FromBody] SaveExercise.Input input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] DeleteExercise.Input input)
        {
            await _mediator.Send(input);
            return Ok();
        }
    }
}