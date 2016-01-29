using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRX.Common.Models.Contacts;
using DRX.Common.Models.Contacts.Interfaces;
using DRX.Common.Models.Vehicles.Interfaces;

namespace DRX.Repositories.Repositories
{
    public class ContactRepository : IContactRepository
    {
        //private readonly IVehicleRepository _vehicleRepository;
        private readonly DBContext _context;

        public ContactRepository(IVehicleRepository vehicleRepository)
        {
            _context = new DBContext();
            //_vehicleRepository = vehicleRepository;
        }

        public List<Contact> GetAll()
        {
           return _context.Set<Contact>().ToList();
        }

        public Contact Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Contact item)
        {
            throw new NotImplementedException();
        }

        public void Update(Contact item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
