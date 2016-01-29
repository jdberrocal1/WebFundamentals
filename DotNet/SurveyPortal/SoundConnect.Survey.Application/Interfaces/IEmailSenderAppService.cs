using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.Interfaces
{
    public interface IEmailSenderAppService
    {
        bool SendEmail(byte[] attachment, string toEmailAdrres);
    }
}
