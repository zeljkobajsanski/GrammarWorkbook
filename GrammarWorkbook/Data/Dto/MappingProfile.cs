using AutoMapper;
using GrammarWorkbook.Data.Models;

namespace GrammarWorkbook.Data.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDto, Unit>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<Topic, TopicDto>();
            CreateMap<TopicDto, Topic>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(x => x.Type, opt => opt.MapFrom(new ExerciseToTypeTypeConverter()));
            CreateMap<FillTheBlanksExercise, ExerciseDto>()
                .IncludeBase<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, FillTheBlanksExercise>()
                .IncludeBase<ExerciseDto, Exercise>();
            CreateMap<ExerciseDto, Exercise>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<SentenceDto, Sentence>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Sentence, SentenceDto>();
        }
    }
    
    
    public class ExerciseToTypeTypeConverter : IValueResolver<Exercise, ExerciseDto, string>
    {
        public string Resolve(Exercise source, ExerciseDto destination, string destMember, ResolutionContext context)
        {
            if (source is FillTheBlanksExercise) return "fill";
            return null;
        }
    }
}