using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Interfaces
{
    public interface IUserRegistrationRepository : IRepository<UserRegistration>
    {
        public List<City> GetCitiesByStatesId(Guid stateId);
        public UserRegistration GetUserByEmail(string email);
        public List<UserModel> GetUserAll();
        public Task<UserRegistration> UserLogin(string email, string password);
        public UserRegistration GetUserByResetCode(string resetCode);
    }
}
