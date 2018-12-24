using System;
using System.Collections.Generic;
using System.Threading;
using GrammarWorkbook.Data;
using GrammarWorkbook.UnitTests.Utils;
using GrammarWorkbook.UseCases.Units;
using GrammarWorkbook.UseCases.Units.Dto;
using Xunit;

namespace GrammarWorkbook.UnitTests.UseCases.Units
{
    public class CheckResultsTests
    {
        private CheckResults.Handler _handler;
        private readonly DatabaseContext _databaseContext;

        public CheckResultsTests()
        {
            _databaseContext = DatabaseFactory.Get();
            _handler = new CheckResults.Handler(_databaseContext, MapperFactory.GetMapper());
        }

        [Fact]
        public async void CheckResults()
        {
            var sentences = new[]
            {
                new Sentence()
                {
                    Id = Guid.NewGuid(),
                    Words = new List<Word>()
                    {
                        new Word() {Text = "What "}, new Word() {IsBlank = true, Text = "is"},
                        new Word() {Text = " your "}, new Word() {Text = "name", IsBlank = true}
                    }
                },
                new Sentence()
                {
                    Id = Guid.NewGuid(),
                    Words = new List<Word>()
                    {
                        new Word() {Text = "How "}, new Word() {IsBlank = true, Text = "are"},
                        new Word() {Text = " you?"}
                    }
                },
            };
            var unit = new Unit()
            {
                Topics = new List<Topic>()
                {
                    new Topic()
                    {
                        Exercises = new List<Exercise>()
                        {
                            new Exercise()
                            {
                                Sentences = new List<Sentence>()
                                {
                                    sentences[0]
                                }
                            },
                            new Exercise()
                            {
                                Sentences = new List<Sentence>()
                                {
                                    sentences[1]
                                }
                            }
                        }
                    },
                    new Topic()
                    {
                        Exercises = new List<Exercise>()
                        {
                            new Exercise()
                            {
                                Sentences = new List<Sentence>()
                                {
                                }
                            }
                        }
                    }
                }
            };
            _databaseContext.Sentences.Add(
                new GrammarWorkbook.Data.Models.Sentence(){Id = sentences[0].Id, Text = "What (is|are,is) your (name|name,nickname)"}
            );
            _databaseContext.Sentences.Add(
                new GrammarWorkbook.Data.Models.Sentence() { Id = sentences[1].Id, Text = "How (are|are,is) you?" }
            );
            _databaseContext.SaveChanges();

            // Act
            await _handler.Handle(new CheckResults.Input() {Unit = unit}, CancellationToken.None);

            // Assert
            Assert.True(sentences[0].IsCorrect);
            Assert.True(sentences[1].IsCorrect);
        }
    }
}