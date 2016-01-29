using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.ErrorLogger
{
    public interface ILogger
    {
        void Warning(Exception e);
        void Error(Exception e);
    }
}
