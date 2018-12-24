using AutoMapper;
using GrammarWorkbook.Data.Dto;
using GrammarWorkbook.UseCases.Units;

namespace GrammarWorkbook.UnitTests.Utils
{
    public static class MapperFactory
    {
        public static IMapper GetMapper()
        {
            var mappingCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
                cfg.AddProfile<MappingProfile>();
            });
            return new Mapper(mappingCfg);
        }
    }
}