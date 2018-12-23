using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrammarWorkbook.Data.Models;
using GrammarWorkbook.Utils;

namespace GrammarWorkbook.UseCases.Units
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Unit, Dto.Unit>()
                .ForMember(x => x.UnitId, opt => opt.MapFrom(x => x.Id));
            CreateMap<Topic, Dto.Topic>();

            CreateMap<Sentence, Dto.Sentence>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Words, opt => opt.MapFrom(new WordsResolver()));

            CreateMap<Exercise, Dto.Exercise>()
                .ForMember(x => x.Options, opt => opt.MapFrom(new OptionsResolver()))
                .ForMember(x => x.Type, opt => opt.MapFrom(new TypeResolver()));
            CreateMap<FillTheBlanksExercise, Dto.Exercise>()
                .IncludeBase<Exercise, Dto.Exercise>();

        }
    }

    public class OptionsResolver : IValueResolver<Exercise, Dto.Exercise, string[]>
    {
        public string[] Resolve(Exercise source, Dto.Exercise destination, string[] destMember, ResolutionContext context)
        {
            if (!source.UseOptions || !(source is FillTheBlanksExercise))
            {
                return new string[0];
            }

            var fillTheBlanksExercise = source as FillTheBlanksExercise;
            var options = fillTheBlanksExercise.Sentences.SelectMany(x => x.ExtractPlaceholders()).ToArray().Shuffle();
            return options;
        }
    }

    public class TypeResolver : IValueResolver<Exercise, Dto.Exercise, string>
    {
        public string Resolve(Exercise source, Dto.Exercise destination, string destMember, ResolutionContext context)
        {
            if (source is FillTheBlanksExercise) return "fill";
            throw new Exception("Unrecognized exercise type");
        }
    }

    public class WordsResolver : IValueResolver<Sentence, Dto.Sentence, ICollection<Dto.Word>>
    {
        public ICollection<Dto.Word> Resolve(Sentence source, Dto.Sentence destination, ICollection<Dto.Word> destMember, ResolutionContext context)
        {
            var words = source.TokenizeWords().Select(x =>
            {
                if (x.Text == "_") return new Dto.Word {IsBlank = true, Options = x.Options.Shuffle()};
                return new Dto.Word {Text = x.Text, Options = new string[0]};
            });

            return words.ToList();
        }
    }
}