using Microsoft.EntityFrameworkCore;
using My.CoachManager.Infrastructure.Data.Tools.Configuration;
using My.CoachManager.Infrastructure.Data.UnitOfWorks;

namespace My.CoachManager.Infrastructure.Data.Tools.UnitOfWorks
{
    /// <inheritdoc cref="DataContext" />
    /// <summary>
    /// Database Context for Entity Framework Core
    /// </summary>
    public sealed class CoachManagerContext : DataContext
    {

        #region ----- Overrides Methods -----

        /// <summary>
        /// Configures context.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(DbConfigurationManager.ConnectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Create Rules for database creation.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Seed
            modelBuilder.Seed();
        }

        #endregion ----- Overrides Methods -----
    }
}