using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Common
{
    public sealed class QuestionKey
    {
        private readonly string[] keys;

        //Is an array with the key for the questions that are in the radio button section 
        public static readonly QuestionKey PCPSurvey = new QuestionKey("PCP_Q_2A,PCP_Q_2B,PCP_Q_2C,PCP_Q_2D,PCP_Q_2E,PCP_Q_2F,PCP_Q_2G,PCP_Q_2H");
        public static readonly QuestionKey SpecialistSurvey = new QuestionKey("SP_Q_2A,SP_Q_2B,SP_Q_2C,SP_Q_2D,SP_Q_2E,SP_Q_2F");
        public static readonly QuestionKey CareTeamSurvey = new QuestionKey("CT_Q_3A,CT_Q_3B,CT_Q_3C,CT_Q_3D,CT_Q_3E,CT_Q_3F,CT_Q_3G,CT_Q_3H,CT_Q_3I,CT_Q_3J,CT_Q_3K,CT_Q_3L,CT_Q_3M,CT_Q_3N,CT_Q_3O");

        private QuestionKey(string key)
        {
            this.keys = key.Split(',');
        }

        public bool ContainsKey(string key)
        {
            return this.keys.Contains(key);
        }

        public int NumberOfKeys()
        {
            return this.keys.Length;
        }
    }
}
