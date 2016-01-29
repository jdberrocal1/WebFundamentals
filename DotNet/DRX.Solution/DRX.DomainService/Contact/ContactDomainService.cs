using DRX.Common.Models.Contacts.Interfaces;
using System;
using System.Collections.Generic;
using DRX.Common.Models.Contacts.DTO;
using AutoMapper;

namespace DRX.DomainService.Contact
{
    public class ContactDomainService : IContactDomainService
    {
        private readonly IContactRepository _contactRepository;

        public ContactDomainService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public List<ContactDTO> GetAll()
        {  
            var result = _contactRepository.GetAll();
            return Mapper.Map<List<ContactDTO>>(result);
        }

        public ContactDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(ContactDTO item)
        {
            throw new NotImplementedException();
        }

        public void Update(ContactDTO item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
