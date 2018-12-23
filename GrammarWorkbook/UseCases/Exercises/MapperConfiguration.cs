using AutoMapper;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.Data.Models;

namespace GrammarWorkbook.UseCases.Exercises
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<SaveExercise.Input, Exercise>()
                .IncludeBase<ExerciseDto, Exercise>();
        }
    }

    
}