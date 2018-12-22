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

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveExercise(SaveExercise.Input input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }
    }
}