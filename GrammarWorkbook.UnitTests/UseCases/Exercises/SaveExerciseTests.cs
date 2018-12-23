using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.UnitTests.Utils;
using GrammarWorkbook.UseCases.Exercises;
using GrammarWorkbook.UseCases.Units;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MapperConfiguration = AutoMapper.MapperConfiguration;

namespace GrammarWorkbook.UnitTests.UseCases.Exercises
{
    public class SaveExerciseTests
    {
        private readonly SaveExercise.Handler _handler;
        private readonly DatabaseContext _database;

        public SaveExerciseTests()
        {
            _database = DatabaseFactory.Get();
            var mapperConfig = new MapperConfiguration((configure) =>
            {
                
                configure.AddProfile(new MappingProfile());
                configure.AddProfile(new MapperProfile());
            });
            var mapper = new Mapper(mapperConfig);
            _handler = new SaveExercise.Handler(_database, mapper);
        }
        
        [Fact]
        public async void SaveExercise_InsertNew()
        {
            // Arrange
            var input = new SaveExercise.Input()
            {
                Title = "Fill the blanks",
                IsDialog = false,
                Type = "fill",
                UseOptions = true,
                TopicId = Guid.NewGuid(),
                Sentences = new List<SentenceDto>()
                {
                    new SentenceDto(){Text = "What (is) your name?"}
                }
            };
            
            // Act
            var actual = await _handler.Handle(input, CancellationToken.None);

            // Assert
            var exercise = _database.FillTheBlanksExercises.Include(x => x.Sentences).SingleOrDefault(x => x.Id == actual.Id);
            Assert.NotNull(exercise);
            Assert.Equal(input.Title, exercise.Title);
            Assert.Equal(input.IsDialog, exercise.IsDialog);
            Assert.Equal(input.UseOptions, exercise.UseOptions);
            Assert.Equal(input.TopicId, exercise.TopicId);
            Assert.Equal(1, exercise.Sentences.Count);
            Assert.Equal(input.Sentences.ElementAt(0).Text, exercise.Sentences.ElementAt(0).Text);
            Assert.Equal(input.Title, actual.Title);
            Assert.Equal(input.IsDialog, actual.IsDialog);
            Assert.Equal(input.Type, actual.Type);
            Assert.Equal(input.UseOptions, actual.UseOptions);
            Assert.Equal(input.TopicId, actual.TopicId);
            Assert.Equal(input.Sentences.Count, actual.Sentences.Count);
            Assert.NotEqual(Guid.Empty, actual.Sentences.ElementAt(0).Id);
            Assert.Equal(input.Sentences.ElementAt(0).Text, actual.Sentences.ElementAt(0).Text);
        }
    }
}