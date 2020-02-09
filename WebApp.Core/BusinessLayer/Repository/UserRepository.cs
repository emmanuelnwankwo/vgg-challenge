using System;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.Dtos;
using WebApp.Core.EntityClass;

namespace WebApp.Core.BusinessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserEntity userEntity;
        private UserResponseData responseData;
        public UserRepository(UserEntity _userEntity)
        {
            userEntity = _userEntity;
        }
        public UserResponseData CreateUser(UserRequest userRequest)
        {
            try
            {
                var token = Guid.NewGuid();
                int result = userEntity.Create(userRequest);
                if (result != 0)
                {
                    responseData = new UserResponseData
                    {
                        Id = result,
                        Username = userRequest.Username,
                        Token = $"{token}"
                    };
                    userEntity.PersistToken(token, responseData.Username);
                    return responseData;
                }
                return responseData = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserResponseData GetOneUser(int id)
        {
            try
            {
                var token = Guid.NewGuid();
                var data = userEntity.GetOne(id);
                responseData = new UserResponseData
                {
                    Id = data.Id,
                    Username = data.Username,
                    Token = $"{token}",
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                userEntity.PersistToken(token, responseData.Username);
                return responseData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserResponseData Auth(UserRequest userRequest)
        {
            try
            {
                var data = userEntity.FindUser(userRequest);
                responseData = new UserResponseData
                {
                    Id = data.Id,
                    Username = data.Username,
                    CreatedAt = data.CreatedAt,
                    UpdatedAt = data.UpdatedAt
                };
                return responseData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
