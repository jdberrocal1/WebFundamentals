using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Repositories
{
    public interface ISurveyRepository : IRepository<Core.Entities.Survey>
    {
        Core.Entities.Survey GetBySurveyCode(string surveyCode);
        void UpdateCompleteSurvey(int surveyId);
        Core.Entities.HospitalDetails GetHospitalDetailsByName(string hospitalName);
    }
}
