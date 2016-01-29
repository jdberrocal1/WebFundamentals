using SoundConnect.Survey.Portal.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SoundConnect.Survey.Portal.Modules
{
    public class RequiredWhyResponseAttribute : ValidationAttribute
    {
        private  string[] _otherProperty;
        public RequiredWhyResponseAttribute(string[] otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (string field in _otherProperty)
            {
                PropertyInfo property = validationContext.ObjectType.GetProperty(field);
                if (property == null)
                    return new ValidationResult(string.Format("Property '{0}' is undefined.", field));

                var fieldValue = property.GetValue(validationContext.ObjectInstance, null);

                if (fieldValue != null || !String.IsNullOrEmpty(fieldValue.ToString()))
                {
                    var question = (Question)fieldValue;
                    var questionValue = (Question)value;
                    if (string.IsNullOrEmpty(questionValue.Response))
                    {
                        switch (question.Response)
                        {
                            case "Disagree":
                                return new ValidationResult("This question requires an answer");
                            case "Strongly Disagree":
                                return new ValidationResult("This question requires an answer");
                        }
                    }
                    
                }
            }

            return null;
        }

    }
}