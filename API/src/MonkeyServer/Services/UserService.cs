using log4net;
using Microsoft.Extensions.DependencyInjection;
using MonkeyServer.Database;
using MonkeyServer.DTOs;
using MonkeyServer.Mapper;
using System;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// UserService
    /// </summary>
    public class UserService: IUserService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private readonly UserContext _dbContext;

        /// <summary>
        /// UserService
        /// </summary>
        /// <param name="dbContext"></param>
        public UserService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="createUserInputDTO"></param>
        /// <returns></returns>
        public async Task<CreateUserOutputDTO> CreateUser(CreateUserInputDTO createUserInputDTO)
        {
            var userEntity = UserMapper.ToEntity(createUserInputDTO);

            log.Info($"saving user");

            try
            {
                _dbContext.Users.Add(userEntity);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                log.Info("Successfully saved new User");
                return UserMapper.ToOutputDTO(userEntity);
            }
            catch (Exception ex)
            {
                log.Error("could not upload user", ex);
                throw;
            }
        }
    }
}
