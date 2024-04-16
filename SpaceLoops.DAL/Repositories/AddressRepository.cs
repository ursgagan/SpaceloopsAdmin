using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class AddressRepository : IAddressRepository<Country>
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;
        private readonly ILogger _logger;
        public AddressRepository(ILogger<Country> logger, SpaceLoopsDbContext spaceLoopsDbContext)
        {
            _logger = logger;
            _spaceloopsDbContext = spaceLoopsDbContext;

        }
        public async Task<Country> Create(Country country)
        {
            try
            {
                if (country != null)
                {
                    var addCountry = _spaceloopsDbContext.Add<Country>(country);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addCountry.Entity;
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
        public IEnumerable<Country> GetAll()
        {
            try
            {
                var getCountries = _spaceloopsDbContext.Country.Where(x => x.IsDeleted == false).ToList();
                if (getCountries != null) return getCountries;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Country GetById(Guid countryId)
        {
            try
            {
                if (countryId != null)
                {
                    var country = _spaceloopsDbContext.Country.FirstOrDefault(x => x.Id == countryId);
                    if (country != null) return country;
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
        public void Update(Country country)
        {
            try
            {
                if (country != null)
                {
                    var obj = _spaceloopsDbContext.Update(country);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(Country country)
        {
            try
            {
                if (country != null)
                {   
                    country.IsDeleted = true;
                    var obj = _spaceloopsDbContext.Update(country);
                    _spaceloopsDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
