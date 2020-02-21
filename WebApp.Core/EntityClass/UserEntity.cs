using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Core.Dtos;
using WebApp.Core.Utility;
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
                var hashedPassword = Helper.HashPassword(userRequest.Password);
                User newUser = new User
                {
                    Username = userRequest.Username,
                    Password = hashedPassword,
                    CreatedAt = DateTime.Now
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
            var user = DbContext.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        internal User FindUser(UserRequest userRequest)
        {
            var user = DbContext.Users.FirstOrDefault(x => x.Username == userRequest.Username);
            return user;
        }
    }
}
