using Microsoft.Extensions.Options;
using System;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.Dtos;
using WebApp.Core.EntityClass;
using WebApp.Core.Utility;

namespace WebApp.Core.BusinessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserEntity userEntity;
        private readonly AppSettings appSettings;
        private UserResponseData responseData;
        public UserRepository(UserEntity _userEntity,
            IOptions<AppSettings> _appSettings)
        {
            userEntity = _userEntity;
            appSettings = _appSettings.Value;
        }
        public UserResponseData CreateUser(UserRequest userRequest)
        {
            try
            {
                int result = userEntity.Create(userRequest);
                string token = Helper.GenerateToken(result, userRequest.Username, appSettings.Secret);
                if (result != 0)
                {
                    responseData = new UserResponseData
                    {
                        Id = result,
                        Username = userRequest.Username,
                        Token = token
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
                string token = Helper.GenerateToken(data.Id, data.Username, appSettings.Secret);
                responseData = new UserResponseData
                {
                    Id = data.Id,
                    Username = data.Username,
                    Token = token,
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

        public UserResponseData Auth(UserRequest userRequest)
        {
            try
            {
                var data = userEntity.FindUser(userRequest);
                if (data == null)
                {
                    throw new Exception("User does not exists");
                }
                else if (!Helper.ComparePassword(userRequest.Password, data.Password))
                {
                    throw new Exception("Password is not correct, try again.");
                }
                string token = Helper.GenerateToken(data.Id, data.Username, appSettings.Secret);
                responseData = new UserResponseData
                {
                    Id = data.Id,
                    Username = data.Username,
                    Token = token,
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
