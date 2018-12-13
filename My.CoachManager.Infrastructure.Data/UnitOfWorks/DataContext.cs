using My.CoachManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
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
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
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
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new LoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.UseSqlServer(DbConfigurationManager.ConnectionString,
            x => x.EnableRetryOnFailure())
                .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Create Rules for database creation.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Address
            modelBuilder.Entity<Address>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Address>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Category
            modelBuilder.Entity<Category>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Category>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Position
            modelBuilder.Entity<Position>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Position>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Contacts
            modelBuilder.Entity<Contact>()
                .ToTable("Contacts")
                .HasDiscriminator<ContactType>("Type")
                .HasValue<Phone>(ContactType.Phone)
                .HasValue<Email>(ContactType.Email);

            modelBuilder.Entity<Contact>()
                .HasOne(x => x.Person)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Contact>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Country
            modelBuilder.Entity<Country>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Country>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Person
            modelBuilder.Entity<Player>()
                .HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Player>()
                .HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Player>()
                .Property(x => x.Gender)
                .HasDefaultValue(PlayerConstants.DefaultGender);

            modelBuilder.Entity<Player>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            //Player
            modelBuilder.Entity<Player>()
                .ToTable("Players")
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .Property(x => x.Laterality)
                .HasDefaultValue(PlayerConstants.DefaultLaterality);

            modelBuilder.Entity<PlayerPosition>()
                .HasAlternateKey(x => new { x.PlayerId, x.PositionId });

            modelBuilder.Entity<PlayerPosition>()
                .HasOne(x => x.Player)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.PositionId);

            modelBuilder.Entity<PlayerPosition>()
                .HasOne(x => x.Position)
                .WithMany()
                .HasForeignKey(x => x.PlayerId);
            
            modelBuilder.Entity<PlayerPosition>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Roster
            modelBuilder.Entity<Roster>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roster>()
                .HasOne(x => x.Season)
                .WithMany()
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roster>()
                .HasOne(x => x.Season)
                .WithMany()
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roster>()
                .HasAlternateKey(x => new { x.SeasonId, x.CategoryId });

            modelBuilder.Entity<Roster>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<RosterPlayer>()
                .HasAlternateKey(x => new { x.RosterId, x.PlayerId });

            modelBuilder.Entity<RosterPlayer>()
                .HasOne(x => x.Roster)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.RosterId);

            modelBuilder.Entity<RosterPlayer>()
                .HasOne(x => x.Player)
                .WithMany()
                .HasForeignKey(x => x.PlayerId);

            modelBuilder.Entity<RosterPlayer>()
                .Property(x => x.IsMutation)
                .HasDefaultValue(PlayerConstants.DefaultMutation);

            modelBuilder.Entity<RosterPlayer>()
                .Property(x => x.LicenseState)
                .HasDefaultValue(PlayerConstants.DefaultLicenseState);

            modelBuilder.Entity<RosterPlayer>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Season
            modelBuilder.Entity<Season>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Season>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // User
            modelBuilder.Entity<User>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");
            
            // Seed
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Save Change on DataBase
        /// </summary>
        /// <returns>Number of Modified Element</returns>
        public override int SaveChanges()
        {
            int result = 0;

            var strategy = Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using (var transaction = Database.BeginTransaction())
                {
                    try
                    {
                        result = base.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
                
            });

            return result;
        }

        /// <summary>
        /// Handle exception.
        /// </summary>
        /// <param name="exception"></param>
        private void HandleException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException concurrencyEx)
            {
                ServiceLocator.Current.GetInstance<ILogger>().Error(concurrencyEx);
                // A custom exception of yours for concurrency issues
                throw new ConcurrencyException();
            }

            if (exception is DbUpdateException dbUpdateEx)
            {
                if ((dbUpdateEx.InnerException is SqlException sqlException))
                {

                    ServiceLocator.Current.GetInstance<ILogger>().Error(sqlException);
                    switch (sqlException.Number)
                    {
                        case 2627: // Unique constraint error
                            throw new UniqueException();

                        case 547: // Constraint check violation
                            throw new ConstraintCheckException();

                        case 2601: // Duplicated key row error
                            throw new ConcurrencyException();
                        default:
                            // A custom exception of yours for other DB issues
                            throw new BusinessException(dbUpdateEx.Message);
                    }
                }
            }

            ServiceLocator.Current.GetInstance<ILogger>().Error(exception);
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

        #region Methods

        /// <summary>
        /// Gets the changed property of an entry.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <returns></returns>
        public static IEnumerable<IProperty> GetChangedProperties(EntityEntry dbEntry)
        {
            var properties = dbEntry.State == EntityState.Added ? dbEntry.CurrentValues.Properties : dbEntry.OriginalValues.Properties;
            foreach (var property in properties)
            {
                if (IsValueChanged(dbEntry, property))
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// Tests if the value changed.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static bool IsValueChanged(EntityEntry dbEntry, IProperty property)
        {
            return !Equals(OriginalValue(dbEntry, property), CurrentValue(dbEntry, property));
        }

        /// <summary>
        /// Gets the original value of an entry by property name.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static object OriginalValue(EntityEntry dbEntry, IProperty property)
        {
            object originalValue = null;

            if (dbEntry.State == EntityState.Modified)
            {
                originalValue = dbEntry.OriginalValues.GetValue<object>(property);
            }

            return originalValue;
        }

        /// <summary>
        /// Gets the current value of an entry by property name.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static object CurrentValue(EntityEntry dbEntry, IProperty property)
        {
            object newValue;

            try
            {
                newValue = dbEntry.CurrentValues.GetValue<object>(property);
            }
            catch (InvalidOperationException) // It will be invalid operation when its in deleted state. in that case, new value should be null
            {
                newValue = null;
            }

            return newValue;
        }

        #endregion

    }
}