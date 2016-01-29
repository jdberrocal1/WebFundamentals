using System;
using System.Web.Mvc;

namespace SoundConnect.Survey.Portal.Modules
{
    public class QuestionKeyAttribute : Attribute, IMetadataAware
    {
        public string QuestionKey { get; private set; }
        
        public QuestionKeyAttribute(string questionKey)
        {            
            QuestionKey = questionKey;
        }        

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["QuestionKey"] = QuestionKey;
        }
    }
}