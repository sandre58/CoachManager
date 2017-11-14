using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
using My.CoachManager.Infrastructure.Data.Core;
using System.Data.Entity.Core.Objects;

namespace My.CoachManager.Infrastructure.Data.Extensions
{
    /// <summary>
    /// Class representing a ContextExtension.
    /// </summary>
    internal static class ContextExtensions
    {
        #region ----- Internal Members -----

        /// <summary>
        /// Returns the table name from the specified entity type.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns>The table name.</returns>
        internal static string GetTableName<T>(this IQueryableUnitOfWork context)
            where T : class
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            return objectContext.GetTableName<T>();
        }

        /// <summary>
        /// Returns the table name from the specified object context.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns>The table name.</returns>
        private static string GetTableName<T>(this ObjectContext context)
            where T : class
        {
            var sql = context.CreateObjectSet<T>().ToTraceString();
            var regex = new Regex("FROM (?<table>.*) AS");
            var match = regex.Match(sql);

            var table = match.Groups["table"].Value;
            return table;
        }

        #endregion ----- Internal Members -----
    }
}