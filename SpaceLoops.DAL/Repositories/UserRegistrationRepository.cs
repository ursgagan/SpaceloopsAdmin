using Microsoft.Extensions.Logging;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Repositories
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly SpaceLoopsDbContext _spaceloopsDbContext;

        public UserRegistrationRepository(SpaceLoopsDbContext spaceLoopsDbContext)
        {

            _spaceloopsDbContext = spaceLoopsDbContext;

        }
        public async Task<UserRegistration> Create(UserRegistration user)
        {
            try
            {
                if (user != null)
                {
                    var addUser = _spaceloopsDbContext.Add<UserRegistration>(user);
                    await _spaceloopsDbContext.SaveChangesAsync();
                    return addUser.Entity;
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

        public void Delete(UserRegistration user)
        {
            try
            {
                if (user != null)
                {
                    user.IsDeleted = true;
                    var obj = _spaceloopsDbContext.Update(user);
                    _spaceloopsDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<UserRegistration> GetAll()
        {
            try
            {
                var obj = _spaceloopsDbContext.UserRegistration.Where(x => x.IsDeleted == false).ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserRegistration GetById(Guid userId)
        {
            try
            {
                if (userId != null)
                {
                    var user = _spaceloopsDbContext.UserRegistration.FirstOrDefault(x => x.Id == userId);
                    if (user != null) return user;
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
        public void Update(UserRegistration user)
        {
            try
            {
                if (user != null)
                {
                    var obj = _spaceloopsDbContext.Update(user);
                    if (obj != null) _spaceloopsDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<City> GetCitiesByStatesId(Guid stateId)
        {
            return _spaceloopsDbContext.City
                .Where(city => city.StateId == stateId)
                .ToList();
        }

        public UserRegistration GetUserByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    var user = _spaceloopsDbContext.UserRegistration.FirstOrDefault(x => x.EmailId == email);
                    if (user != null) return user;
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

        public List<UserModel> GetUserAll()
        {
            var userData = from s in _spaceloopsDbContext.UserRegistration
                           join c in _spaceloopsDbContext.City on s.CityId equals c.Id into c2
                           from c in c2.DefaultIfEmpty()
                           join state in _spaceloopsDbContext.State on c.StateId equals state.Id into stateJoin
                           from state in stateJoin.DefaultIfEmpty()
                           join country in _spaceloopsDbContext.Country on state.CountryId equals country.Id into countryJoin
                           from country in countryJoin.DefaultIfEmpty()
                           where s.IsDeleted == false
                           select new UserModel
                           {
                               Id = s.Id,
                               FirstName = s.FirstName,
                               LastName = s.LastName,
                               PhoneNumber = s.PhoneNumber,
                               EmailId = s.EmailId,
                               IsDeleted = s.IsDeleted,
                               UpdatedOn = s.UpdatedOn,
                               CreatedOn = s.CreatedOn,
                               CityName = c.CityName,
                               StateName = state.StateName,
                               CountryName = country.CountryName,

                           };
            return userData.ToList();
        }

        public async Task<UserRegistration> UserLogin(string email, string password)
        {

            var user = _spaceloopsDbContext.UserRegistration.FirstOrDefault(x => x.EmailId == email && x.Password == password);
            if (user != null)
             return user;
            else return null;
        }

        public UserRegistration GetUserByResetCode(string resetCode)
        {
            try
            {
                if (resetCode != null)
                {
                    var user = _spaceloopsDbContext.UserRegistration.FirstOrDefault(x => x.ResetCode == resetCode);
                    if (user != null) return user;
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
    }
}
