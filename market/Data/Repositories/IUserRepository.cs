using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Models;

namespace market.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistUserByEmail(string email);
        User GetUserForLogin(string email, string pass);
        void AddUser(User user);

    }
}
