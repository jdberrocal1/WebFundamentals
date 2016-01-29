using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Common
{
    public sealed class EmailDetail
    {
        private readonly string detail;

        public static readonly EmailDetail MessageBody = new EmailDetail("Escalation report for partner satisfaction surveys received on ");
        public static readonly EmailDetail Subject = new EmailDetail("Partner Satisfaction Surveys Escalation Report");
        public static readonly EmailDetail AttachmentNameTemplate = new EmailDetail("Escalation_Report");
        public static readonly EmailDetail XLSXFormat = new EmailDetail(".xlsx");

        private EmailDetail(string detail)
        {
            this.detail = detail;
        }

        public override string ToString()
        {
            return this.detail;
        }
    }
}
