using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PlayerOrderBuilder
    {
        /// <summary>
        /// Creates the order builder.
        /// </summary>
        public static QueryOrder<Player> OrderByProperty(string propertyName, bool descending = false)
        {
            var order = new QueryOrder<Player>();

            if (!string.IsNullOrEmpty(propertyName))
            {
                var property = propertyName;
                var sortDescending = descending;

                switch (propertyName.ToLower())
                {
                    case "age":
                    case "birthdate":
                        property = "Birthdate";
                        sortDescending = !descending;
                        break;
                }

                var expression = ExpressionsExtensions.ExpressionFromString<Player>(property);
                if (sortDescending)
                    order.OrderByDescending(expression);
                else
                    order.OrderBy(expression);
            }
            else
            {
                order.OrderBy(x => x.LastName);
            }
            return order;
        }
    }
}