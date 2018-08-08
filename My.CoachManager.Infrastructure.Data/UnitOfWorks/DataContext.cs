﻿using My.CoachManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Domain.Core;
using My.CoachManager.Infrastructure.Data.Core;
using ILogger = My.CoachManager.CrossCutting.Logging.ILogger;

namespace My.CoachManager.Infrastructure.Data.UnitOfWorks
{
    /// <inheritdoc cref="DbContext" />
    /// <summary>
    /// Database Context for Entity Framework Core
    /// </summary>
    public sealed class DataContext : DbContext, IQueryableUnitOfWork
    {
        #region Properties

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<RosterPlayer> RosterPlayers { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion Properties

        #region ----- Overrides Methods -----

        /// <summary>
        /// Configures context.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConfigurationManager.ConnectionString,
            x => x.EnableRetryOnFailure())
                .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseLoggerFactory(new LoggerFactory());

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Create Rules for database creation.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //// Default Value
            //modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("SqlDefaultValue",
            //    (p, attributes) =>
            //    {
            //        var value = attributes.Single().Value;
            //        if (value is Enum || value is bool)
            //        {
            //            return Convert.ToInt32(value).ToString();
            //        }
            //        return value.ToString();
            //    }));

            // Address
            modelBuilder.Entity<Address>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Save Change on DataBase
        /// </summary>
        /// <returns>Number of Modified Element</returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Changes are canceled after an error
                RollbackChanges();

                ServiceLocator.Current.GetInstance<ILogger>().Error(ex);
                throw new BusinessException(ex.Message);
            }
            catch (Exception ex)
            {
                try
                {
                    // Log exception information
                    ServiceLocator.Current.GetInstance<ILogger>().Error(ex);
                }
                catch (Exception)
                {
                    // ignore
                }

                // Changes are canceled after an error
                RollbackChanges();
                throw;
            }
        }

        #endregion ----- Overrides Methods -----

        #region ----- IQueryableUnitOfWork Methods -----

        /// <inheritdoc />
        /// <summary>
        /// <see cref="M:My.CoachManager.Infrastructure.Data.Core.IQueryableUnitOfWork.CreateSet``1" />
        /// </summary>
        /// <typeparam name="TEntity"><see cref="M:My.CoachManager.Infrastructure.Data.Core.IQueryableUnitOfWork.CreateSet``1" /></typeparam>
        /// <returns><see cref="M:My.CoachManager.Infrastructure.Data.Core.IQueryableUnitOfWork.CreateSet``1" /></returns>
        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Attaches entity to context.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        public new void Attach<TEntity>(TEntity item) where TEntity : class
        {
            base.Attach(item);
        }

        /// <summary>
        /// Set Object as Modified.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="item">Item to set modified in context.</param>
        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            Update(item);
        }

        #endregion ----- IQueryableUnitOfWork Methods -----

        #region ----- IUnitOfWork Methods -----

        /// <inheritdoc />
        /// <summary>
        /// <see cref="M:My.CoachManager.Domain.Core.IUnitOfWork.Commit" />
        /// </summary>
        public void Commit()
        {
            var identityName = Thread.CurrentPrincipal.Identity.Name;
            var now = DateTime.UtcNow;

            var addedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedDate = now;
                added.ModifiedDate = now;
                added.CreatedBy = identityName;
                added.ModifiedBy = identityName;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.ModifiedDate = now;
                modified.ModifiedBy = identityName;
            }

            SaveChanges();
        }

        /// <summary>
        /// <see cref="IUnitOfWork.CommitAndRefreshChanges"/>
        /// </summary>
        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    Commit();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // if there is an optimistic concurrence, we reload all Entry value from database
                    ex.Entries?.ToList().ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        /// <summary>
        /// <see cref="IUnitOfWork.RollbackChanges"/>
        /// </summary>
        public void RollbackChanges()
        {
            // Change all Entities in change tracker as 'unchanged' state
            ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion ----- IUnitOfWork Methods -----

        #region ----- ISql Methods -----

        /// <inheritdoc />
        /// <summary>
        /// <see cref="M:My.CoachManager.Domain.Core.ISql.ExecuteQuery``1(System.String,System.Object[])" />
        /// </summary>
        /// <typeparam name="TEntity"><see cref="M:My.CoachManager.Domain.Core.ISql.ExecuteQuery``1(System.String,System.Object[])" /></typeparam>
        /// <param name="sqlQuery"><see cref="M:My.CoachManager.Domain.Core.ISql.ExecuteQuery``1(System.String,System.Object[])" /></param>
        /// <param name="parameters"><see cref="M:My.CoachManager.Domain.Core.ISql.ExecuteQuery``1(System.String,System.Object[])" /></param>
        /// <returns><see cref="M:My.CoachManager.Domain.Core.ISql.ExecuteQuery``1(System.String,System.Object[])" /></returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters) where TEntity : class
        {
            return Set<TEntity>().FromSql(sqlQuery, parameters);
        }

        /// <summary>
        /// <see cref="ISql.ExecuteCommand(string, object[])"/>
        /// </summary>
        /// <param name="sqlCommand"><see cref="ISql.ExecuteCommand(string, object[])"/></param>
        /// <param name="parameters"><see cref="ISql.ExecuteCommand(string, object[])"/></param>
        /// <returns><see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/></returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion ----- ISql Methods -----

        #region ----- Methods -----

        /// <summary>
        /// Log a sql query.
        /// </summary>
        /// <param name="sqlQuery">The executed sql query.</param>
        private void LogQuery(string sqlQuery)
        {
            try
            {
                ServiceLocator.Current.GetInstance<ILogger>().Trace(sqlQuery);
            }
            catch (Exception)
            {
                // ignore
            }
        }

        #endregion ----- Methods -----
    }
}