using AutoMapper;
using GrammarWorkbook.Data.Models;

namespace GrammarWorkbook.UseCases.Topics
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<SaveTopic.Input, Topic>();
        }
    }
}