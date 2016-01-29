using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Data;
using System.Collections.Generic;
using System.Linq;

namespace SoundConnect.Survey.DBContext.Repository
{
    public class QuestionRepository : EntityRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDataContext context)
            : base(context)
        {

        }

        public ICollection<Question> GetBySurveyType(SurveyType surveyType)
        {
            var questions = this.Context.Question.Where(d => d.SurveyType.Equals((int)surveyType));
            return questions.ToList();
        }


    }
}
