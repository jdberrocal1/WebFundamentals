using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Core.Entities;
using System.Collections.Generic;

namespace SoundConnect.Survey.Core.Repositories
{
    public interface IQuestionRepository : IRepository<Question> 
    {
        ICollection<Question> GetBySurveyType(SurveyType surveyType);
    }
}
