using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AddressModule.Aggregate
{
    public static class AddressOrderBuilder
    {
        /// <summary>
        /// Creates the order builder.
        /// </summary>
        public static QueryOrder<Address> OrderByCity(bool descending = false)
        {
            var order = new QueryOrder<Address>();

            if (descending)
                order.OrderByDescending(x => x.City);
            else
                order.OrderBy(x => x.City);
            return order;
        }
    }
}
