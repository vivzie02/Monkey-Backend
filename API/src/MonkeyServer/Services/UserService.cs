using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkeyServer.Database;
using MonkeyServer.DTOs;
using MonkeyServer.Mapper;
using System;
using System.Threading;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateUserOutputDTO> CreateUser(CreateUserInputDTO createUserInputDTO, CancellationToken cancellationToken)
        {
            log.Info($"saving user");

            //check if user exists
            if (await _dbContext.Users.AnyAsync(user => string.Equals(user.Username, createUserInputDTO.Username)).ConfigureAwait(false))
            {
                log.Info("User already exists");
                throw new DbUpdateException("User already exists");
            }

            var userEntity = UserMapper.ToEntity(createUserInputDTO);

            try
            {
                await _dbContext.Users.AddAsync(userEntity, cancellationToken).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                log.Info("Successfully saved new User");
                return UserMapper.ToOutputDTO(userEntity);
            }
            catch (Exception ex)
            {
                log.Error("could not create user", ex);
                throw;
            }
        }
    }
}
