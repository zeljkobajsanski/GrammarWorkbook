using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GrammarWorkbook.UseCases.Exercises
{
    public class SaveExercise
    {
        public class Input : ExerciseDto, IRequest<ExerciseDto>
        {
        }

        public class Handler : HandlerBase<Input, ExerciseDto>
        {
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<ExerciseDto> Handle(Input request, CancellationToken cancellationToken)
            {
                Exercise exercise;
                if (request.Id == Guid.Empty)
                {
                    exercise = ExerciseFactory.Create(request.Type);
                    Context.Exercises.Add(exercise);
                }
                else
                {
                    switch (request.Type)
                    {
                        case "fill":
                            exercise = await Context.Exercises.OfType<FillTheBlanksExercise>()
                                .Include(x => x.Sentences)
                                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                            break;
                        default: throw new Exception("Unrecognized exercise type");
                    }
                }
                
                Mapper.Map(request, exercise);

                /*if (exercise is FillTheBlanksExercise)
                {
                    var fillTheBlanksExercise = (FillTheBlanksExercise) exercise;
                    foreach (var sentence in fillTheBlanksExercise.Sentences)
                    {
                        if (sentence.Id == Guid.Empty)
                        {
                            
                        }
                    }
                }*/
                
                await Context.SaveChangesAsync(cancellationToken);
                return Mapper.Map<ExerciseDto>(exercise);
            }
        }
    }
}