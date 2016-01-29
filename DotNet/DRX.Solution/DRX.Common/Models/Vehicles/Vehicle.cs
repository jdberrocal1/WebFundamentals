using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRX.Common.Models.Contacts;

namespace DRX.Common.Models.Vehicles
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(100)]
        public string LicensePlate { get; set; }

        public int? ContactId { get; set; }

        [ForeignKey("ContactId")]

        public Contact Contact { get; set; }
    }
}
