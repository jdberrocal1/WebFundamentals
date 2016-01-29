using SoundConnect.Survey.Portal.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundConnect.Survey.Portal.Modules
{
    public class RequiredResponseAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(Question))
                {
                    var question = (Question)value;
                    if (string.IsNullOrEmpty(question.Response))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}