using AutoMapper;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Core.Entities;

namespace SoundConnect.Survey.Application.Common
{
    public static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Core.Entities.Survey, SurveyDto>();
            Mapper.CreateMap<SurveyDto, Core.Entities.Survey>();

            Mapper.CreateMap<AnswerDto, Answer>();
            Mapper.CreateMap<Answer, AnswerDto>();

            Mapper.CreateMap<QuestionDto, Question>();
            Mapper.CreateMap<Question, QuestionDto>();

            Mapper.CreateMap<SurveyAnswerDto, SurveyAnswer>();
            Mapper.CreateMap<SurveyAnswer, SurveyAnswerDto>();

            Mapper.CreateMap<HospitalDetailsDto, HospitalDetails>();
            Mapper.CreateMap<HospitalDetails, HospitalDetailsDto>();
        }
    }
}