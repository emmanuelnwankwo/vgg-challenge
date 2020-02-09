using WebApp.Core.Dtos;

namespace WebApp.Core.BusinessLayer.Interface
{
    public interface IUserRepository
    {
        UserResponseData CreateUser(UserRequest userRequest);
        UserResponseData GetOneUser(int id);
        UserResponseData Auth(UserRequest userRequest);
    }
}