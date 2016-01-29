using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.ErrorLogger
{
    public static class Global
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get { return _logger ?? (_logger = new Logger()); }
            set { _logger = value; }
        }
    }
}
