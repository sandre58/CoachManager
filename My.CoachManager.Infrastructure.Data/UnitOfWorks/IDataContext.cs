﻿using My.CoachManager.Domain.Entities;

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
        #region Properties

        DbSet<Address> Adresses { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Club> Clubs { get; set; }
        DbSet<Coach> Coachs { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Function> Functions { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<PlayerPosition> PlayerPositions { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Roster> Rosters { get; set; }
        DbSet<RosterPlayer> RosterPlayers { get; set; }
        DbSet<RosterCoach> RosterCoachs { get; set; }
        DbSet<Season> Seasons { get; set; }
        DbSet<Squad> Squads { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<User> Users { get; set; }

        #endregion Properties

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