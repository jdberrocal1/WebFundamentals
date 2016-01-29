using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoredProcs;

namespace SoundConnect.Survey.Core.Entities
{
    public class HospitalDetails
    {
        [StoredProcAttributes.Name("region_name")]
        public string RegionName { get; set; }
        [StoredProcAttributes.Name("site_number")]
        public string SiteNumber { get; set; }
    }
}
