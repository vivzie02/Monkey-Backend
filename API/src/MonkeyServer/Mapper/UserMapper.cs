using MonkeyServer.DTOs;
using MonkeyServer.Entities;
using System;

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
            //TODO: Add password hashing

            return new User
            {
                UserId = Guid.NewGuid(),
                Username = createUserInputDTO.Username,
                Password = createUserInputDTO.Password
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
