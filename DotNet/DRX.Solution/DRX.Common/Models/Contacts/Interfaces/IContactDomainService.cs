using System.Collections.Generic;
using DRX.Common.Models.Contacts.DTO;

namespace DRX.Common.Models.Contacts.Interfaces
{
    public interface IContactDomainService
    {
        List<ContactDTO> GetAll();
        ContactDTO Get(int id);
        void Create(ContactDTO item);
        void Update(ContactDTO item);
        void Delete(int id);
    }
}
