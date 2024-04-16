using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;
        private readonly ILogger _logger;
        public CityRepository(ILogger<State> logger, SpaceLoopsDbContext spaceLoopsDbContext)
        {
            _logger = logger;
            _spaceloopsDbContext = spaceLoopsDbContext;
        }

        public async Task<City> Create(City city)
        {
            try
            {
                if (city != null)
                {
                    var addCity= _spaceloopsDbContext.Add<City>(city);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addCity.Entity;
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

        public void Delete(City city)
        {
            try
            {
                if (city != null)
                {
                    city.IsDeleted = true;
                    var obj = _spaceloopsDbContext.Update(city);
                    _spaceloopsDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<City> GetAll()
        {
            try
            {
                var obj = _spaceloopsDbContext.City.Where(x => x.IsDeleted == false).ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public City GetById(Guid cityId)
        {
            try
            {
                if (cityId != null)
                {
                    var city = _spaceloopsDbContext.City.FirstOrDefault(x => x.Id == cityId);
                    if (city != null) return city;
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
        public void Update(City city)
        {
            try
            {
                if (city != null)
                {
                    var obj = _spaceloopsDbContext.Update(city);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CityModel> GetCitiesAll()
        {
            var cityData = from c in _spaceloopsDbContext.City.Where(x => x.IsDeleted == false)
                           join s in _spaceloopsDbContext.State on c.StateId equals s.Id into s2
                            from s in s2.DefaultIfEmpty()
                            select new CityModel
                            {
                                Id = c.Id,
                                CityName = c.CityName,
                                StateId = c.StateId,              
                                IsDeleted = c.IsDeleted,
                                UpdatedOn = c.UpdatedOn,
                                CreatedOn = c.CreatedOn,
                                StateName = s.StateName
                            };
            return cityData.ToList();
        }
    }
}
