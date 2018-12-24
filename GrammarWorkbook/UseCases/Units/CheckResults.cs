using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = GrammarWorkbook.UseCases.Units.Dto.Unit;

namespace GrammarWorkbook.UseCases.Units
{
    public class CheckResults
    {
        public class Input : IRequest<Dto.Unit>
        {
            public Dto.Unit Unit { get; set; }
        }

        public class Handler : HandlerBase<Input, Dto.Unit>
        {
            public Handler(DatabaseContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public override async Task<Unit> Handle(Input request, CancellationToken cancellationToken)
            {
                var sentences = request.Unit.Topics.SelectMany(x => x.Exercises).SelectMany(x => x.Sentences);
                foreach (var sentence in sentences)
                {
                    var s = await Context.Sentences.SingleAsync(x => x.Id == sentence.Id, cancellationToken);
                    var createdSentence = sentence.MakeSentence();
                    var correctAnswer = s.GetCorrectAnswer();
                    sentence.IsCorrect = correctAnswer == createdSentence;
                    sentence.CorrectText = correctAnswer;
                }

                return request.Unit;
            }
        }
    }
    
    
}