using System.Collections.Generic;

namespace DRX.Common.Models.Vehicles.Interfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetAll();
        Vehicle Get(int id);
        void Create(Vehicle item);
        void Update(Vehicle item);
        void Delete(int id);
    }
}
