using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.BAL.Services
{
    public class ContactServices
    {
        private readonly IRepository<Contact> _contactRepository;
        public ContactServices(IRepository<Contact> contactrepository)
        {
            _contactRepository = contactrepository;
        }
        public async Task<Contact> AddContact(Contact contact)
        {
            try
            {
                if (contact == null)
                {
                    throw new ArgumentNullException(nameof(contact));
                }
                else
                {
                    return await _contactRepository.Create(contact);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteContact(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var contact = _contactRepository.GetById(id);
                    _contactRepository.Delete(contact);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateContact(Contact contact)
        {
            try
            {
                if (contact.Id != null || contact.Id != Guid.Empty)
                {
                    var obj = _contactRepository.GetAll().Where(x => x.Id == contact.Id).FirstOrDefault();
                    if (obj != null) _contactRepository.Update(contact);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Contact> GetAllContact()
        {
            try
            {
                return _contactRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Contact GetContactById(Guid contactId)
        {
            try
            {
                return _contactRepository.GetById(contactId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
