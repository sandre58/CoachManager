using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Infrastructure.Data.UnitOfWorks
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Core;

    ///<sumary>
    ///Interface for context in Main Module
    ///</sumary>
    public interface IDataContext : IQueryableUnitOfWork
    {
        #region ----- Properties -----

        DbSet<Category> Categories { get; }
        DbSet<Person> Persons { get; }
        DbSet<Coach> Coachs { get; }
        DbSet<Player> Players { get; }
        DbSet<Country> Countries { get; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Permission> Permissions { get; }
        DbSet<PlayerPosition> PlayerPositions { get; }
        DbSet<Position> Positions { get; }
        DbSet<Role> Roles { get; set; }
        DbSet<Season> Seasons { get; }
        DbSet<SeasonCoach> SeasonCoaches { get; }
        DbSet<SeasonPlayer> SeasonPlayers { get; }
        DbSet<Squad> Squad { get; set; }
        DbSet<SquadPlayer> SquadPlayers { get; }
        DbSet<User> Users { get; set; }

        #endregion ----- Properties -----

        #region ----- IQueryableUnitOfWork Methods -----

        /// <summary>
        /// Gets the state of the entity object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The state of the entity object.</returns>
        EntityState GetObjectStateEntry<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Gets a System.Data.Entity.Infrastructure.DbEntityEntry{TEntity} object for
        //     the given entity providing access to information about the entity and the
        //     ability to perform actions on the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns> An entry for the entity.</returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        #endregion ----- IQueryableUnitOfWork Methods -----
    }
}