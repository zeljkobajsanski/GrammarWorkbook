using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GrammarWorkbook.UseCases.Topics
{
    public class GetTopic
    {
        public class Input : IRequest<TopicDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : HandlerBase<Input, TopicDto>
        {
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<TopicDto> Handle(Input request, CancellationToken cancellationToken)
            {
                /*var query = await Context.Topics.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                var fillBlanksExercises = await Context.FillTheBlanksExercises.Include(x => x.Sentences)
                    .Where(x => x.TopicId == request.Id).ToListAsync(cancellationToken);
                var exercises = fillBlanksExercises.Select(x => Mapper.Map<ExerciseDto>(x));
                var topic = Mapper.Map<TopicDto>(query);
                topic.Exercises = exercises.ToList();
                return topic;*/
                var query = await Context.Topics.Include(x => x.Exercises)
                                                    .ThenInclude(x => ((FillTheBlanksExercise)x).Sentences)
                                          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                return Mapper.Map<TopicDto>(query);
            }
        }
    }
}