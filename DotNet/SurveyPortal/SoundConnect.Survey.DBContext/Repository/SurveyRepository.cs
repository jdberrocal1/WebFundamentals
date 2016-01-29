using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using CodeFirstStoredProcs;

namespace SoundConnect.Survey.DBContext.Repository
{
    public class SurveyRepository : EntityRepository<Core.Entities.Survey>, ISurveyRepository
    {
        public SurveyRepository(IDataContext context)
            : base(context)
        {

        }

        public Core.Entities.Survey GetBySurveyCode(string surveyCode)
        {
            
            //List Parameters
            List<SqlParameter> parameters = new List<SqlParameter>() 
            { 
                new SqlParameter("@survey_code", surveyCode)
            };            
            //Stored Proc
            var storedProc = new StoredProc("usp_get_survey", typeof(Core.Entities.Survey));
            storedProc.schema = "survey";

            //Call stored proc
            var result = DBContext.CallStoredProc(storedProc, parameters);

            //Get Result
            return result.ToList<Core.Entities.Survey>().FirstOrDefault<Core.Entities.Survey>();
        }

        public void UpdateCompleteSurvey(int surveyId)
        {
            //List Parameters
            List<SqlParameter> parameters = new List<SqlParameter>() 
            { 
                new SqlParameter("@survey_id", surveyId)
            };

            //Stored Proc
            var storedProc = new StoredProc("usp_survey_complete_update", typeof(Core.Entities.Survey));
            storedProc.schema = "survey";

            //Call stored proc
            DBContext.CallStoredProc(storedProc, parameters);
            
        }

        public Core.Entities.HospitalDetails GetHospitalDetailsByName(string hospitalName)
        {

            //List Parameters
            List<SqlParameter> parameters = new List<SqlParameter>() 
            { 
                new SqlParameter("@hospital_name", hospitalName)
            };
            //Stored Proc
            var storedProc = new StoredProc("usp_get_hospital_details", typeof(Core.Entities.HospitalDetails));
            storedProc.schema = "survey";

            //Call stored proc
            var result = DBContext.CallStoredProc(storedProc, parameters);

            //Get Result
            return result.ToList<Core.Entities.HospitalDetails>().FirstOrDefault<Core.Entities.HospitalDetails>();
        }
    }
}
