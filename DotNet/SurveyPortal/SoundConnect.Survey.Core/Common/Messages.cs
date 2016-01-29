using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Common
{
    public class Messages
    {
        public enum MessageType
        {
            Completed,
            Expired,
            NotExist,
            Succefully
        }

        public static string GetMessage(MessageType messageType, params string[] arg)
        {
            switch (messageType)
            {
                case MessageType.Completed:
                    return string.Format("Hello Dr {0}. Thank you for your interest in providing feedback for the Hospitalist Team at {1}. It looks like you have already taken this survey, but please look out for another opportunity to provide feedback next quarter.", arg[0], arg[1]);
                case MessageType.Expired:
                    return string.Format("Hello Dr {0}. Thank you for your interest in providing feedback for the Hospitalist Team at {1}. This quarter’s survey has closed, but please look out for another opportunity to provide feedback next quarter.", arg[0], arg[1]); 
                case MessageType.NotExist:
                    return "The survey does not exist.";
                case MessageType.Succefully:
                    return "The survey was completed successfully.";
                default:
                    return "";
            }

        }



    }
}
