using System.Linq;
using My.CoachManager.Application.Dtos.User;
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
                Name = item.Name,
                Roles = item.Roles.Select(CreateRole).ToList()
            };
        }

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Role CreateRole(RoleDto item)
        {
            if (item == null) return null;

            return new Role()
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                Permissions = item.Permissions.Select(CreatePermission).ToList(),
            };
        }

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Permission CreatePermission(PermissionDto item)
        {
            if (item == null) return null;

            return new Permission()
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order
            };
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static RoleDto GetRole(Role item)
        {
            if (item == null) return null;

            return new RoleDto()
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order,
                Permissions = item.Permissions.Select(GetPermission).ToList(),
            };
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static PermissionDto GetPermission(Permission item)
        {
            if (item == null) return null;

            return new PermissionDto()
            {
                Id = item.Id,
                Code = item.Code,
                Label = item.Label,
                Description = item.Description,
                Order = item.Order
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
                Name = item.Name,
                Roles = item.Roles.Select(GetRole).ToList()
            };
        }
    }
}