using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRX.Common.Models.Vehicles.DTO;
using DRX.Common.Models.Vehicles.Interfaces;

namespace DRX.DomainService.Vehicle
{
    public class VehicleDomainService : IVehicleDomainService
    {

        private readonly IVehicleRepository _vehicleRepository;

        public VehicleDomainService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            //"data source=JBERROCAL-AS\MAIN;initial catalog=DRX;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
        }

        public List<VehicleDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public VehicleDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(VehicleDTO item)
        {
            throw new NotImplementedException();
        }

        public void Update(VehicleDTO item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
