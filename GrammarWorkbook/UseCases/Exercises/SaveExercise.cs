using System;
using System.Collections.Generic;
using AutoMapper;
using GrammarWorkbook.Data;
using MediatR;

namespace GrammarWorkbook.UseCases.Exercises
{
    public class SaveExercise
    {
        public class Input : IRequest<Output>
        {
            public Guid Id { get; set; }
            public string Type { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
            public string[] Options { get; set; }
            public Sentence[] Sentences { get; set; }

            public class Sentence
            {
                public Guid Id { get; set; }
                public string Text { get; set; }
            }

            public class Option
            {
                public Guid Id { get; set; }
                public string Value { get; set; }
            }

            public class Dialog
            {
                public Sentence Sentence { get; set; }
                public ICollection<Option> Options { get; set; }
            }
        }

        public class Output
        {
            
        }

        public class Handler : RequestHandler<Input, Output>
        {
            private IMapper _mapper;
            private DatabaseContext _databaseContext;

            public Handler(IMapper mapper, DatabaseContext databaseContext)
            {
                _mapper = mapper;
                _databaseContext = databaseContext;
            }

            protected override Output Handle(Input request)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}