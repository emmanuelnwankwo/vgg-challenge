using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Core.Dtos;
using WebApp.Data;
using WebApp.Data.Models.Entities;

namespace WebApp.Core.EntityClass
{
    public class UserEntity
    {
        private ApiDbContext DbContext;
        public UserEntity(ApiDbContext dbContext)
        {
            DbContext = dbContext;
        }
        internal int Create(UserRequest userRequest)
        {
            try
            {
                var hashedPassword = userRequest.Password;
                User newUser = new User
                {
                    Username = userRequest.Username,
                    Password = hashedPassword
                };
                DbContext.Users.Add(newUser);
                DbContext.SaveChanges();
                return newUser.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal User GetOne(int id)
        {
            var user = DbContext.Users.Single(x => x.Id == id);
            return user;
        }
    }
}
