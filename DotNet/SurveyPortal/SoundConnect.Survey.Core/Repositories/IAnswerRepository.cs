using SoundConnect.Survey.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Repositories
{
    public interface IAnswerRepository : IRepository<Answer> 
    {
        ICollection<Answer> GetBySurveyId(int surveyId);
    }
}
