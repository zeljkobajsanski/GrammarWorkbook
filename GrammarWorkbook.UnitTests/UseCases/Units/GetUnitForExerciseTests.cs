using System;
using System.Linq;
using System.Threading;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.UnitTests.Utils;
using GrammarWorkbook.UseCases.Units;
using Xunit;

namespace GrammarWorkbook.UnitTests.UseCases.Units
{
    public class GetUnitForExerciseTests
    {
        private readonly DatabaseContext _database;
        private GetUnitForExercise.Handler _handler;

        public GetUnitForExerciseTests()
        {
            _database = DatabaseFactory.Get();
            var mapperConfig = new MapperConfiguration((configure) =>
            {
                
                configure.AddProfile(new MappingProfile());
                configure.AddProfile(new MapperProfile());
            });
            var mapper = new Mapper(mapperConfig);
            _handler = new GetUnitForExercise.Handler(_database, mapper);
        }

        [Fact]
        public async void Execute()
        {
            var id = Guid.NewGuid();
            var input = new GetUnitForExercise.Input(){Id = id};
            var unit = new Unit
            {
                Id = id,
                Title = "Unit 1",
                Topics =
                {
                    new Topic()
                    {
                        Title = "Grammar",
                        SubTitle = "Check grammar fro A1 level",
                        Exercises = { 
                            new FillTheBlanksExercise()
                            {
                                Title = "Fill the blanks",
                                UseOptions = true,
                                Sentences =
                                {
                                    new Sentence(){Text = "What (is) your name?"}
                                }
                            }
                        }
                    }
                }
            };
            _database.Units.Add(unit);
            _database.SaveChanges();
            
            // Act
            GrammarWorkbook.UseCases.Units.Dto.Unit actual = await _handler.Handle(input, CancellationToken.None);
            
            Assert.Equal(unit.Id, actual.UnitId);
            Assert.Equal(unit.Title, actual.Title);
            Assert.Equal(unit.Topics.Count, actual.Topics.Count);
            Assert.Equal(unit.Topics.ElementAt(0).Title, actual.Topics.ElementAt(0).Title);
            Assert.Equal(unit.Topics.ElementAt(0).SubTitle, actual.Topics.ElementAt(0).SubTitle);
            Assert.Equal(unit.Topics.ElementAt(0).Exercises.Count, actual.Topics.ElementAt(0).Exercises.Count);
            Assert.Equal(unit.Topics.ElementAt(0).Exercises.ElementAt(0).Title, actual.Topics.ElementAt(0).Exercises.ElementAt(0).Title);
            
            Assert.Equal(((FillTheBlanksExercise)unit.Topics.ElementAt(0).Exercises.ElementAt(0)).Id,
                actual.Topics.ElementAt(0).Exercises.ElementAt(0).Id);
            Assert.Equal(((FillTheBlanksExercise)unit.Topics.ElementAt(0).Exercises.ElementAt(0)).IsDialog,
                actual.Topics.ElementAt(0).Exercises.ElementAt(0).IsDialog);
            Assert.Equal(((FillTheBlanksExercise)unit.Topics.ElementAt(0).Exercises.ElementAt(0)).Title,
                actual.Topics.ElementAt(0).Exercises.ElementAt(0).Title);
            Assert.Single(actual.Topics.ElementAt(0).Exercises.ElementAt(0).Options);
            Assert.Equal("is", actual.Topics.ElementAt(0).Exercises.ElementAt(0).Options[0]);
            Assert.Equal("fill", actual.Topics.ElementAt(0).Exercises.ElementAt(0).Type);
        }
    }
}