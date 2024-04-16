using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        public List<CityModel> GetCitiesAll();
    
    }
}
