using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using Topic.Models.Identity;

namespace Topic.Contracts
{
    public interface IAuthService
    {
        Task Register(RegistrationRequestDTO registrationRequestDTO);
        Task RegisterAdmin(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> BlockUser(UserBlock_UnblockDTO userBlock_UnblockDTO);
        Task<User> UnblockUser(UserBlock_UnblockDTO userBlock_UnblockDTO);
        Task<List<UserInfo>> GetAllUsers();
        Task<UserInfo> GetUsersByMail(string mail);
        Task<User> UpdateUser(UserUpdate user);
    }
}
