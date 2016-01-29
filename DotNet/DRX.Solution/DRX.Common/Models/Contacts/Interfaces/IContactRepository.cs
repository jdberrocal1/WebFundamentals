using System.Collections.Generic;

namespace DRX.Common.Models.Contacts.Interfaces
{
    public interface IContactRepository
    {
        List<Contact> GetAll();
        Contact Get(int id);
        void Create(Contact item);
        void Update(Contact item);
        void Delete(int id);
    }
}
