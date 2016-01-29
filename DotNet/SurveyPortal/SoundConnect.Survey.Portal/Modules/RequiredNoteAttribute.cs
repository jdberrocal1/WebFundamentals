using SoundConnect.Survey.Portal.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundConnect.Survey.Portal.Modules
{
    public class RequiredNoteAttribute : ValidationAttribute
    {
        public string ValueRequiredNote { get; private set; }

        public RequiredNoteAttribute(string valueRequiredNote)
        {
            ValueRequiredNote = valueRequiredNote;
        }   
  
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(Question))
                {
                    var question = (Question)value;
                    if (question.Response == ValueRequiredNote)
                    {
                        if (string.IsNullOrEmpty(question.Note))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}