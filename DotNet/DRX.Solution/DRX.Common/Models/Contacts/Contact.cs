using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DRX.Common.Models.Vehicles;

namespace DRX.Common.Models.Contacts
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }

        public List<Vehicle> Vehicles { get; set; } 
    }
}
