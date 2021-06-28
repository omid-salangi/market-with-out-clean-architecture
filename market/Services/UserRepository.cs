using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using market.Data.Repositories;
using market.Models;

namespace market.Services
{
    public class UserRepository:IUserRepository
    {
        private MarketContext _context;

        public UserRepository(MarketContext context)
        {
            _context = context;
        }
        public bool IsExistUserByEmail(string email)
        {
            bool ans = _context.Users.Any(u => u.Email == email);
            return ans;
        }

        public User GetUserForLogin(string email,string pass)
        {
            User user= _context.Users.SingleOrDefault(u => u.Email == email && u.Password == pass);
            return user;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
