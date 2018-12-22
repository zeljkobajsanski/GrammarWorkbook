using AutoMapper;
using GrammarWorkbook.Data.Models;

namespace GrammarWorkbook.UseCases.Exercises
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<SaveExercise.Input.Option, Data.Models.Option>();
            CreateMap<SaveExercise.Input.Sentence, Data.Models.Sentence>();

            CreateMap<SaveExercise.Input, FillTheBlanksExercise>();
            CreateMap<SaveExercise.Input, DialogExercise>();
        }
    }
}