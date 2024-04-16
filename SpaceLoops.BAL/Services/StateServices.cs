using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using SpaceLoops.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.BAL.Services
{
    public class StateServices
    {
        private readonly IRepository<State> _stateRepository;
        private readonly IStatesRepository _newStateRepository;
        private readonly IAddressRepository<Country> _countryRepository;
        public StateServices(IRepository<State> stateRepository, IAddressRepository<Country> countryRepository, IStatesRepository newStateRepository)
        {
            _stateRepository = stateRepository;
            _countryRepository = countryRepository;
            _newStateRepository = newStateRepository;
        }

        public async Task<State> AddState(State state)
        {
            try
            {
                if (state == null)
                {
                    throw new ArgumentNullException(nameof(state));
                }
                else
                {
                    state.IsDeleted = false;
                    state.CreatedOn = DateTime.Now;
                    state.UpdatedOn = DateTime.Now;
                    return await _stateRepository.Create(state);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<State> GetAllCountries()
        {
            try
            {
                return _stateRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<State> GetAllStates()
        {
            try
            {
                return _stateRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteState(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var state = _stateRepository.GetById(id);
                    _stateRepository.Delete(state);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public State GetStateById(Guid stateId)
        {
            try
            {
                return _stateRepository.GetById(stateId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateState(State state)
        {
            try
            {
                if (state.Id != null || state.Id != Guid.Empty)
                {
                    var obj = _stateRepository.GetAll().Where(x => x.Id == state.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.StateName = state.StateName;
                        obj.UpdatedOn = DateTime.Now;
                        _stateRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<State> GetStatesByCountryId(Guid countryId)
        {
            var getstates = _newStateRepository.GetStatesByCountryId(countryId);
            return getstates;
        }

        public IEnumerable<StateModel> GetStatesAll()
        {
            try
            {
                return _newStateRepository.GetStatesAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
