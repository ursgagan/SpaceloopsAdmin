using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.BAL.Services
{
    public class UserRegistrationService
    {
        private readonly IRepository<UserRegistration> _userRegistrationRepository;
        private readonly IUserRegistrationRepository _userRegistrationRepository2;
        public UserRegistrationService(IRepository<UserRegistration> userRegistrationRepository, IUserRegistrationRepository userRegistrationRepository2)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRegistrationRepository2 = userRegistrationRepository2;
        }

        public async Task<UserRegistration> AddUser(UserRegistration user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                else
                {
                    user.IsDeleted = false;
                    user.CreatedOn = DateTime.Now;
                    user.UpdatedOn = DateTime.Now;
                    return await _userRegistrationRepository.Create(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<UserRegistration> GetAllUsers()
        {
            try
            {
                return _userRegistrationRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteUser(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var user = _userRegistrationRepository.GetById(id);
                    _userRegistrationRepository.Delete(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserRegistration GetUserById(Guid user)
        {
            try
            {
                return _userRegistrationRepository.GetById(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateUser(UserRegistration user)
        {
            try
            {
                if (user.Id != null || user.Id != Guid.Empty)
                {
                    var obj = _userRegistrationRepository.GetById(user.Id);
                    if (obj != null)
                    {
                        obj.FirstName = user.FirstName;
                        obj.LastName = user.LastName;
                        obj.PhoneNumber = user.PhoneNumber;
                        obj.EmailId = user.EmailId;
                        obj.CityId = user.CityId;
                        obj.UpdatedOn = DateTime.Now;
                        obj.ResetCode = user.ResetCode;
                        obj.Password = user.Password;
                        obj.ImageId = user.ImageId;
                        _userRegistrationRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<City> GetCitiesByStatesId(Guid stateId)
        {
            var getcities = _userRegistrationRepository2.GetCitiesByStatesId(stateId);
            return getcities;
        }

        public UserRegistration isUserExist(string email)
        {
            return _userRegistrationRepository2.GetUserByEmail(email);
        }
        public List<UserModel> GetUserAll()
        {
                try
                {
                    return _userRegistrationRepository2.GetUserAll().ToList();
                }
                catch (Exception)
                {
                    throw;
                }
        }

        public async Task<UserRegistration> UserLogin(string email, string password)
        {
            var userLogin = await _userRegistrationRepository2.UserLogin(email,password);
            return userLogin;
        }

        public UserRegistration GetUserByResetCode(string resetCode)
        {
            try
            {
                return _userRegistrationRepository2.GetUserByResetCode(resetCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
