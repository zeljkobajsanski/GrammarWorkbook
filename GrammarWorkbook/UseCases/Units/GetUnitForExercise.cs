using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = GrammarWorkbook.UseCases.Units.Dto.Unit;

namespace GrammarWorkbook.UseCases.Units
{
    public class GetUnitForExercise
    {
        
        
        public class Input : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Handler : HandlerBase<Input, Unit>
        {
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<Unit> Handle(Input request, CancellationToken cancellationToken)
            {
                var unit = await Context.Units.Include(x => x.Topics)
                    .ThenInclude(x => x.Exercises)
                    .ThenInclude(x => ((FillTheBlanksExercise) x).Sentences)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Mapper.Map<Unit>(unit);
            }
        }
    }
}