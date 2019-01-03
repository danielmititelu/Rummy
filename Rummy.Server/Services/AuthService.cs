using Rummy.Server.Models;
using Rummy.Shared.Models;
using System;
using System.Linq;

namespace Rummy.Server.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AreCredentialsValid(User user)
        {
            //Todo: encrypt password
            return (from u in _dbContext.User
                    where string.Equals(u.Email, user.Email, StringComparison.CurrentCultureIgnoreCase)
                          && u.Password == user.Password
                    select true).FirstOrDefault();
        }

        public void Register(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
