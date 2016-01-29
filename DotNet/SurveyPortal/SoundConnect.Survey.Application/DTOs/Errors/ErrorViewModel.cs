using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.DTOs
{
    public class ErrorViewModel
    {
        public ErrorInfo ErrorInfo { get; set; }

        public Exception Exception { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(Exception exception)
        {
            Exception = exception;
            ErrorInfo = new ErrorInfo(exception.GetHashCode(), exception.Message, exception.StackTrace);
        }
    }
}
