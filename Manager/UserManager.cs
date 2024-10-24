using CustomIdentity.Context;
using CustomIdentity.Dto;
using CustomIdentity.DTO;
using CustomIdentity.Entities;
using CustomIdentity.Service;
using CustomIdentity.Types;

namespace CustomIdentity.Manager
{
    public class UserManager : IUserService
    {
        private readonly CustomIdentityDbContext _context;

        public UserManager(CustomIdentityDbContext customIdentityDbContext)
        {
            _context = customIdentityDbContext;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var addUser = new UserEntity
            {
                Email = user.Email,
                Password = user.Password
            };

            _context.Users.Add(addUser);
            _context.SaveChanges();

            return new ServiceMessage { IsSucceed = true, Message = "Registration succesfull" };
        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto loginUser)
        {
            var userEntity = _context.Users.Where(U => U.Email.ToLower() == loginUser.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto> { IsSucceed = false, Message = "Username or password incorrect." };
            }

            if (userEntity.Password == loginUser.Password)
            {
                return new ServiceMessage<UserInfoDto> { IsSucceed = true, Data = new UserInfoDto { Id = userEntity.Id, Email = userEntity.Email, UserRole = userEntity.UserRole } };
            }
            else
            {
                return new ServiceMessage<UserInfoDto> { IsSucceed = false, Message = "Username or password incorrect." };
            }

        }
    }
}
