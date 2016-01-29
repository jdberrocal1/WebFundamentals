using System.Collections.Generic;
using DRX.Common.Models.Vehicles.DTO;

namespace DRX.Common.Models.Vehicles.Interfaces
{
    public interface IVehicleDomainService
    {
        List<VehicleDTO> GetAll();
        VehicleDTO Get(int id);
        void Create(VehicleDTO item);
        void Update(VehicleDTO item);
        void Delete(int id);
    }
}
