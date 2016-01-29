using AutoMapper;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Core.Repositories;
using System.Collections.Generic;

namespace SoundConnect.Survey.Application
{

    public class QuestionAppService : IQuestionAppService
    {
        private readonly IQuestionRepository _repository;

        public QuestionAppService(IQuestionRepository context)
        {
            _repository = context;
        }
        
        public ICollection<QuestionDto> GetBySurveyType(SurveyType surveyType)
        {
            if (surveyType >= 0)
            {
                var result = _repository.GetBySurveyType(surveyType);

                return Mapper.Map<List<QuestionDto>>(result);
            }

            return null;
        }


    }

}
