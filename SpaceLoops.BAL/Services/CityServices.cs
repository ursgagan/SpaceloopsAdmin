using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using SpaceLoops.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.BAL.Services
{
    public class CityServices
    {
        private readonly IRepository<City> _cityRepository;
        private readonly ICityRepository _citiesRepository;
        public CityServices(IRepository<City> cityRepository, ICityRepository citiesRepository)
        {
            _cityRepository = cityRepository;
            _citiesRepository = citiesRepository;
        }

        public async Task<City> AddCity(City city)
        {
            try
            {
                if (city == null)
                {
                    throw new ArgumentNullException(nameof(city));
                }
                else
                {
                    city.IsDeleted = false;
                    city.CreatedOn = DateTime.Now;
                    city.UpdatedOn = DateTime.Now;
                    return await _cityRepository.Create(city);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<City> GetAllCountries()
        {
            try
            {
                return _cityRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<City> GetAllCities()
        {
            try
            {
                return _cityRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteCity(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var city = _cityRepository.GetById(id);
                    _cityRepository.Delete(city);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public City GetCityById(Guid cityId)
        {
            try
            {
                return _cityRepository.GetById(cityId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public void UpdateCity(City city)
        //{
        //    try
        //    {
        //        if (city.Id != null || city.Id != Guid.Empty)
        //        {
        //            //var obj = _stateRepository.GetAll().Where(x => x.Id == state.Id).FirstOrDefault();

        //            var updatedcity = _cityRepository.GetAll()
        //                .Where(x => x.Id == city.Id)
        //                .Join(
        //                    _cityRepository.GetAll(),  // Assuming you have a repository for the Country entity
        //                    state => state.CountryId,
        //                    country => country.Id,
        //                    (state, country) => new
        //                    {
        //                        State = state,
        //                        CountryName = country.CountryName
        //                    }
        //                )
        //                .FirstOrDefault();
        //            if (updatedState != null)
        //            {
        //                var obj = updatedState.State;
        //                obj.CountryId = state.CountryId;
        //                obj.StateName = state.CityName;
        //                obj.UpdatedOn = DateTime.Now;
        //                _cityRepository.Update(obj);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void UpdateCity(City city)
        {
            try
            {
                if (city.Id != null || city.Id != Guid.Empty)
                {
                    var obj = _cityRepository.GetAll().Where(x => x.Id == city.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.CityName = city.CityName;
                        obj.UpdatedOn = DateTime.Now;
                        _cityRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CityModel> GetCitiesAll()
        {
            try
            {
                return _citiesRepository.GetCitiesAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
