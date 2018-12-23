using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;
using MediatR;

namespace GrammarWorkbook.UseCases.Topics
{
    public class SaveTopic
    {
        public class Input : IRequest<Output>
        {
            public Guid Id { get; set; }
            public Guid UnitId { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
        }

        public class Output
        {
            public Guid Id { get; set; }
        }
        
        public class Handler : HandlerBase<Input, Output>
        {
            
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<Output> Handle(Input request, CancellationToken cancellationToken)
            {
                Topic topic;
                if (request.Id != Guid.Empty)
                {
                    topic = await Context.Topics.FindAsync(request.Id);
                    Mapper.Map(request, topic);
                }
                else
                {
                    topic = Mapper.Map<Topic>(request);
                    Context.Topics.Add(topic);
                }

                await Context.SaveChangesAsync(cancellationToken);
                return new Output(){Id = topic.Id};
            }
        }
    }
}