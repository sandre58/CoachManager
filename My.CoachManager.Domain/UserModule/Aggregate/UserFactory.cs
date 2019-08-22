using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.UserModule.Aggregate
{
    /// <summary>
    /// The user factory.
    /// </summary>
    public static class UserFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static User CreateEntity(UserDto item)
        {
            if (item == null) return null;

            return new User
            {
                Id = item.Id,
                Password = item.Password,
                Login = item.Login,
                Mail = item.Mail,
                Name = item.Name
            };
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static UserDto Get(User item)
        {
            if (item == null) return null;

            return new UserDto
            {
                Id = item.Id,
                Password = item.Password,
                Login = item.Login,
                Mail = item.Mail,
                Name = item.Name
            };
        }
    }
}
