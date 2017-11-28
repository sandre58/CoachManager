using My.CoachManager.Domain.Core.Specification;

namespace My.CoachManager.Domain.UserModule.Aggregate
{
    public static class UserSpecification
    {
        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<Entities.User> GetUserByCredentials(string login, string password)
        {
            Specification<Entities.User> specification = new TrueSpecification<Entities.User>();

            specification &= new DirectSpecification<Entities.User>(x => x.Login == login);
            specification &= new DirectSpecification<Entities.User>(x => x.Password == password);

            return specification;
        }

        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<Entities.User> GetUserByLogin(string login)
        {
            Specification<Entities.User> specification = new TrueSpecification<Entities.User>();

            specification &= new DirectSpecification<Entities.User>(x => x.Login == login);

            return specification;
        }
    }
}