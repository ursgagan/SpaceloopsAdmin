using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.BAL.Services
{
    public class AddressServices
    {
        private readonly IAddressRepository<Country> _countryRepository;
        public AddressServices(IAddressRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> AddCountry(Country country)
        {
            try
            {
                if (country == null)
                {
                    throw new ArgumentNullException(nameof(country));
                }
                else
                {
                    country.IsDeleted = false;
                    country.CreatedDate = DateTime.Now;
                    country.UpdatedDate = DateTime.Now;
                    return await _countryRepository.Create(country);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Country> GetAllCountries()
        {
            try
            {
                return _countryRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Country GetCountryById(Guid Id)
        {
            try
            {
                return _countryRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateCountry(Country country)
        {
            try
            {
                if (country.Id != null || country.Id != Guid.Empty)
                {
                    var obj = _countryRepository.GetAll().Where(x => x.Id == country.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.CountryName = country.CountryName;
                        obj.UpdatedDate = DateTime.Now;
                        _countryRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCountry(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var country = _countryRepository.GetById(id);
                    _countryRepository.Delete(country);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
