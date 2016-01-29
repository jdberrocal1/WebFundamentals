using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.DBContext.Tests.FakeObject
{
    public class FakeAnswer
    {
        public int Id { get; set; }

        public int SurveyAnswerId { get; set; }

        public int QuestionId { get; set; }

        public string ResponseText { get; set; }

        public string Note { get; set; }
    }
}
