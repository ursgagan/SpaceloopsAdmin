using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;
        private readonly ILogger _logger;
        public ContactRepository(ILogger<Contact> logger, SpaceLoopsDbContext spaceLoopsDbContext)
        {
            _logger = logger;
            _spaceloopsDbContext = spaceLoopsDbContext;

        }
        public async Task<Contact> Create(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    var addContact = _spaceloopsDbContext.Add<Contact>(contact);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addContact.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(Contact contact)
        {
            try
            {
                if (contact != null)
                {                   
                        contact.IsDeleted = true;
                        var obj = _spaceloopsDbContext.Update(contact);
                        _spaceloopsDbContext.SaveChangesAsync();                  
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Contact> GetAll()
        {
            try
            {
                var obj = _spaceloopsDbContext.Contacts.Where(x => x.IsDeleted != false).ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Contact GetById(Guid contactId)
        {
            try
            {
                if (contactId != null)
                {
                    var contact = _spaceloopsDbContext.Contacts.FirstOrDefault(x => x.Id == contactId);
                    if (contact != null) return contact;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    var obj = _spaceloopsDbContext.Update(contact);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
