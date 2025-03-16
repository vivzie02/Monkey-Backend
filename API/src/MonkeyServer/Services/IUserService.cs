using MonkeyServer.DTOs;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="createUserInputDTO"></param>
        /// <returns></returns>
        Task<CreateUserOutputDTO> CreateUser(CreateUserInputDTO createUserInputDTO);
    }
}
