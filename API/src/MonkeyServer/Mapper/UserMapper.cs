using MonkeyServer.DTOs;
using MonkeyServer.Entities;
using MonkeyServer.Services.Security;
using System;
using static MonkeyServer.Constants.Constants;

namespace MonkeyServer.Mapper
{
    /// <summary>
    /// UserMapper
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// DTO to entity
        /// </summary>
        /// <param name="createUserInputDTO"></param>
        /// <returns></returns>
        public static User ToEntity(CreateUserInputDTO createUserInputDTO)
        {
            var salt = PasswordHasherService.GenerateSalt();
            var password = PasswordHasherService.ComputeHash(createUserInputDTO.Password, salt, HASHING_ITERATIONS);

            return new User
            {
                UserId = Guid.NewGuid(),
                Username = createUserInputDTO.Username,
                Salt = salt,
                Password = password
            };
        }

        /// <summary>
        /// Entity to DTO 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static CreateUserOutputDTO ToOutputDTO(User user)
        {
            return new CreateUserOutputDTO
            {
                UserId = user.UserId,
                Username = user.Username,
            };
        }
    }
}
