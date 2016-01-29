using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DRX.Common.Models.Contacts;
using DRX.Common.Models.Contacts.DTO;
using DRX.Common.Models.Vehicles.DTO;
using DRX.Common.Models.Vehicles;

namespace DRX.DomainServices
{
    public class DtoMapping
    {
        public static void Map()
        {
            Mapper.CreateMap<Vehicle, VehicleDTO>();
            Mapper.CreateMap<VehicleDTO, Vehicle>();
            Mapper.CreateMap<Contact, ContactDTO>();
            Mapper.CreateMap<ContactDTO, Contact>();
        }
    }
}
