using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class StateRepository : IStatesRepository
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;
        private readonly ILogger _logger;
        public StateRepository(ILogger<State> logger, SpaceLoopsDbContext spaceLoopsDbContext)
        {
            _logger = logger;
            _spaceloopsDbContext = spaceLoopsDbContext;
        }

        public async Task<State> Create(State state)
        {
            try
            {
                if (state != null)
                {
                    var addState = _spaceloopsDbContext.Add<State>(state);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addState.Entity;
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

        public void Delete(State state)
        {
            try
            {
                if (state != null)
                {
                    state.IsDeleted = true;
                    var obj = _spaceloopsDbContext.Update(state);
                    _spaceloopsDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<State> GetAll()
        {
            try
            {
                var obj = _spaceloopsDbContext.State.Where(x => x.IsDeleted == false).ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public State GetById(Guid stateId)
        {
            try
            {
                if (stateId != null)
                {
                    var state = _spaceloopsDbContext.State.FirstOrDefault(x => x.Id == stateId);
                    if (state != null) return state;
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
        public void Update(State state)
        {
            try
            {
                if (state != null)
                {
                    var obj = _spaceloopsDbContext.Update(state);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<State> GetStatesByCountryId(Guid countryId)
        {
            return _spaceloopsDbContext.State
                .Where(state => state.CountryId == countryId)
                .ToList();
        }

        public List<StateModel> GetStatesAll()
        {
            var stateData = from s in _spaceloopsDbContext.State.Where(x => x.IsDeleted == false)
                            join c in _spaceloopsDbContext.Country on s.CountryId equals c.Id into c2
                            from c in c2.DefaultIfEmpty()
                            select new StateModel {
                                Id = s.Id,
                                StateName = s.StateName,
                                CountryId = s.CountryId,
                                IsDeleted = s.IsDeleted,
                                UpdatedOn = s.UpdatedOn,
                                CreatedOn = s.CreatedOn,
                                CountryName = c.CountryName
                            };
            return stateData.ToList();
        }
    }
}
