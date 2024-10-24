using CustomIdentity.Dto;
using CustomIdentity.DTO;
using CustomIdentity.Types;

namespace CustomIdentity.Service
{
    public interface IUserService
    {

        Task<ServiceMessage> AddUser(AddUserDto user);
        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);
    }
}
