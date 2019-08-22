using My.CoachManager.Domain.Core.Specification;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.UserModule.Aggregate
{
    public static class UserSpecification
    {
        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<User> GetUserByCredentials(string login, string password)
        {
            Specification<User> specification = new TrueSpecification<User>();

            specification &= new DirectSpecification<User>(x => x.Login == login);
            specification &= new DirectSpecification<User>(x => x.Password == password);

            return specification;
        }

        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<User> GetUserByLogin(string login)
        {
            Specification<User> specification = new TrueSpecification<User>();

            specification &= new DirectSpecification<User>(x => x.Login == login);

            return specification;
        }
    }
}
