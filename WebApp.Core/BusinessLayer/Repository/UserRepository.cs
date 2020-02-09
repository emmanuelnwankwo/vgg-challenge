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
                int result = userEntity.Create(userRequest);
                if (result != 0)
                {
                    responseData = new UserResponseData
                    {
                        Id = result,
                        Username = userRequest.Username,
                        Token = "Token"
                    };
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
                var data = userEntity.GetOne(id);
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
