using SoundConnect.Survey.Application.ErrorLogger;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application
{
    public class EmailSenderAppService : IEmailSenderAppService
    {

        private readonly ILogger _logger;

        public EmailSenderAppService(ILogger logger)
        {
            _logger = logger;
        }

        public bool SendEmail(byte[] attachment, string toEmailAddress)
        {
            string attachmentName = String.Format("{0}_{1}", EmailDetail.AttachmentNameTemplate, DateTime.Now.ToString("MM/dd/yyyy"));
            return SendEmailClient(toEmailAddress, EmailDetail.Subject.ToString(), attachment, attachmentName);
        }
        private bool SendEmailClient(string toEmailAddress, string subject, byte[] attachment, string attachmentName)
        {
            try
            {
                MailMessage message = new MailMessage();

                toEmailAddress = toEmailAddress.Replace(';', '\n');
                toEmailAddress = toEmailAddress.Replace(' ', '\n');
                toEmailAddress = toEmailAddress.Replace(',', '\n');

                List<string> recipientList = toEmailAddress.Split('\n').ToList<string>();
                for (int i = 0; i < recipientList.Count; i++)
                {
                    recipientList[i] = recipientList[i].Trim();
                    recipientList[i] = recipientList[i].Trim('\r');
                }

                for (int i = recipientList.Count - 1; i > -1; i--)
                    if (string.IsNullOrEmpty(recipientList[i]))
                        recipientList.RemoveAt(i);

                string[] recipients = recipientList.ToArray();
                foreach (string recipient in recipients)
                    message.To.Add(recipient);

                message.Subject = subject;

                message.Body = String.Format("{0}{1}",EmailDetail.MessageBody , DateTime.Now.ToString("MM/dd/yyyy"));
                Stream s = new MemoryStream(attachment);
                Attachment att = new Attachment(s, String.Format("{0}{1}", attachmentName , EmailDetail.XLSXFormat));
                message.Attachments.Add(att);

            
                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }

            return true;
        }
    }
}
