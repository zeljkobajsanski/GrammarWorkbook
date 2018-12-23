using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;
using MediatR;
using Unit = MediatR.Unit;

namespace GrammarWorkbook.UseCases.Exercises
{
    public class DeleteExercise
    {
        public class Input : IRequest
        {
            public Guid Id { get; set; }
        }
        
        public class Handler : HandlerBase<Input>
        {
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<Unit> Handle(Input request, CancellationToken cancellationToken)
            {
                var exercise = await Context.Exercises.FindAsync(request.Id);
                if (exercise is FillTheBlanksExercise)
                {
                    await Context.Entry((FillTheBlanksExercise) exercise)
                                 .Collection(x => x.Sentences)
                                 .LoadAsync(cancellationToken);
                }
                Context.Exercises.Remove(exercise);
                await Context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}