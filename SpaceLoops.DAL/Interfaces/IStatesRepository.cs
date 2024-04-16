using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Interfaces
{
    public interface IStatesRepository : IRepository<State>
    {
        public List<State> GetStatesByCountryId(Guid countryId);

        public List<StateModel> GetStatesAll();
    }
}
