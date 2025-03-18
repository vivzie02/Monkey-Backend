using MonkeyServer.DTOs;
using System.Threading;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateUserOutputDTO> CreateUserAsync(CreateUserInputDTO createUserInputDTO, CancellationToken cancellationToken);
    }
}
