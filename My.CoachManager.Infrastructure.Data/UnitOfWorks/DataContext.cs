using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq.Expressions;
using System.Reflection;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Infrastructure.Data.UnitOfWorks
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Core;
    using Core.Exceptions;
    using Domain.Core;
    using Extensions;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using CrossCutting.Unity;
    using CrossCutting.Logging;

    /// <summary>
    /// Database Context for Entity Framework 6.0
    /// </summary>
    public class DataContext : DbContext, IDataContext
    {
        #region Properties

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Coach> Coachs { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PlayerPosition> PlayerPositions { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<SeasonCoach> SeasonCoaches { get; set; }
        public virtual DbSet<SeasonPlayer> SeasonPlayers { get; set; }
        public virtual DbSet<Squad> Squad { get; set; }
        public virtual DbSet<SquadPlayer> SquadPlayers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion Properties

        public DataContext()
            : base("name=CoachManager")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>("CoachManager"));

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            Database.Log = LogQuery;
        }

        #region ----- Overrides Methods -----

        /// <summary>
        /// Do Not used in Model First
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("SqlDefaultValue",
                (p, attributes) =>
                {
                    var value = attributes.Single().Value;
                    if (value is Enum || value is bool)
                    {
                        return Convert.ToInt32(value).ToString();
                    }
                    return value.ToString();
                }));
            
            modelBuilder.Entity<Player>().HasRequired(p => p.Category).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Contact>().HasRequired(x => x.CurrentPerson).WithMany(g => g.Contacts).HasForeignKey(x => x.PersonId);
            modelBuilder.Entity<Person>().HasMany(x => x.Contacts).WithRequired(x => x.CurrentPerson).WillCascadeOnDelete();
            
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
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessage = string.Empty;

                foreach (var item in ex.EntityValidationErrors)
                    foreach (var error in item.ValidationErrors)
                        errorMessage += String.Format(" - {0} on Entity {1}", error.ErrorMessage, item.Entry.Entity.GetType().FullName);

                // Changes are canceled after an error
                RollbackChanges();

                throw new InfrastructureDataException(errorMessage, ex);
            }
            catch (DbUpdateException ex)
            {
                // Log exception information
                Trace.TraceError(ex.ToString());

                // Changes are canceled after an error
                RollbackChanges();
                throw;
            }
        }

        /// <summary>
        /// Locks the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Lock<TEntity>(TEntity entity)
            where TEntity : class
        {
            var context = this as IQueryableUnitOfWork;

            var tableName = context.GetTableName<TEntity>();
            var primarykeyName = String.Concat(tableName.Split('_')[1].ToLower(), "_id");

            context.ExecuteCommand(String.Format("SELECT * FROM {0} WITH (ROWLOCK) WHERE {1}={2};", tableName, primarykeyName, ((IEntity)entity).Id));
        }

        #endregion ----- Overrides Methods -----

        #region ----- IQueryableUnitOfWork Methods -----

        /// <summary>
        /// <see cref="IQueryableUnitOfWork.CreateSet{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="IQueryableUnitOfWork.CreateSet{TEntity}"/></typeparam>
        /// <returns><see cref="IQueryableUnitOfWork.CreateSet{TEntity}"/></returns>
        public IDbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// <see cref="IQueryableUnitOfWork.Attach{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="IQueryableUnitOfWork.Attach{TEntity}"/></typeparam>
        /// <param name="item"><see cref="IQueryableUnitOfWork.Attach{TEntity}"/></param>
        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            if (Entry(item).State != EntityState.Unchanged)
                Entry(item).State = EntityState.Unchanged;
        }

        /// <summary>
        /// <see cref="IQueryableUnitOfWork.AddOrUpdate{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="IQueryableUnitOfWork.AddOrUpdate{TEntity}"/></typeparam>
        /// <param name="entity"><see cref="IQueryableUnitOfWork.AddOrUpdate{TEntity}"/></param>
        /// <param name="ignoreProperties"></param>
        public void AddOrUpdate<TEntity>(TEntity entity, params string[] ignoreProperties) where TEntity : class, IEntity
        {
            if (entity == null || Entry(entity).State == EntityState.Added || Entry(entity).State == EntityState.Modified) { return; }

            //var state = Set<TEntity>().Any(x => x.Id == entity.Id) ? EntityState.Modified : EntityState.Added;
            var state = entity.Id != 0 ? EntityState.Modified : EntityState.Added;
            Entry(entity).State = state;

            var type = typeof(TEntity);
            RelationshipManager relationship;
            var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;
            if (stateManager.TryGetRelationshipManager(entity, out relationship))
            {
                foreach (var end in relationship.GetAllRelatedEnds())
                {
                    var propertyInfo = end.GetType().GetProperty("IsForeignKey", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (propertyInfo != null)
                    {
                        var isForeignKey = propertyInfo.GetValue(end) as bool?;
                        var memberInfo = end.GetType().GetProperty("NavigationProperty", BindingFlags.Instance | BindingFlags.NonPublic);
                        if (memberInfo != null)
                        {
                            var navigationProperty = memberInfo.GetValue(end);
                            if (navigationProperty != null)
                            {
                                var info = navigationProperty.GetType().GetProperty("Identity", BindingFlags.Instance | BindingFlags.NonPublic);
                                if (info != null)
                                {
                                    var propertyName = info.GetValue(navigationProperty) as string;
                                    if (string.IsNullOrWhiteSpace(propertyName) || ignoreProperties.Contains(propertyName)) { continue; }

                                    var property = type.GetProperty(propertyName);
                                    if (property == null) { continue; }

                                    if (end is IEnumerable)
                                    {
                                        UpdateChildrenInternal(entity, property, isForeignKey == true);
                                    }
                                    else
                                    {
                                        var value = property.GetValue(entity) as IEntity;
                                        if (value != null) AddOrUpdate(value, ignoreProperties);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (state == EntityState.Modified)
            {
                Entry(entity).OriginalValues.SetValues(Entry(entity).GetDatabaseValues());
                Entry(entity).State = GetChangedProperties(Entry(entity)).Any() ? state : EntityState.Unchanged;
            }
        }

        /// <summary>
        /// <see cref="IQueryableUnitOfWork.ApplyCurrentValues{TEntity}(TEntity, TEntity)"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="IQueryableUnitOfWork.ApplyCurrentValues{TEntity}(TEntity, TEntity)"/></typeparam>
        /// <param name="original"><see cref="IQueryableUnitOfWork.ApplyCurrentValues{TEntity}(TEntity, TEntity)"/></param>
        /// <param name="current"><see cref="IQueryableUnitOfWork.ApplyCurrentValues{TEntity}(TEntity, TEntity)"/></param>
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            Entry(original).CurrentValues.SetValues(current);
        }

        /// <summary>
        /// Gets the state of the entity object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The state of the entity object.</returns>
        public EntityState GetObjectStateEntry<TEntity>(TEntity item)
            where TEntity : class
        {
            return Entry(item).State;
        }

        #endregion ----- IQueryableUnitOfWork Methods -----

        #region ----- IUnitOfWork Methods -----

        /// <summary>
        /// <see cref="IUnitOfWork.Commit"/>
        /// </summary>
        public void Commit()
        {
            var auditables = ChangeTracker.Entries<IAuditable>();

            foreach (var auditable in auditables)
            {
                if (auditable == null || auditable.State == EntityState.Unchanged || auditable.State == EntityState.Deleted) continue;

                var identityName = Thread.CurrentPrincipal.Identity.Name;
                var now = DateTime.UtcNow;

                if (auditable.State == EntityState.Added)
                {
                    auditable.Entity.CreatedBy = identityName;
                    auditable.Entity.CreatedDate = now;
                }
                else
                {
                    auditable.Property(x => x.CreatedBy).IsModified = false;
                    auditable.Property(x => x.CreatedDate).IsModified = false;
                }

                auditable.Entity.ModifiedBy = identityName;
                auditable.Entity.ModifiedDate = now;
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
                    if (ex.Entries != null)
                        ex.Entries.ToList().ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
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

        /// <summary>
        /// <see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/></typeparam>
        /// <param name="sqlQuery"><see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/></param>
        /// <param name="parameters"><see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/></param>
        /// <returns><see cref="ISql.ExecuteQuery{TEntity}(string, object[])"/></returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
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
        public void LogQuery(string sqlQuery)
        {
            UnityFactory.Resolve<ILogger>().Trace(sqlQuery);
        }

        /// <summary>
        /// Update the children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="isForeignKey"></param>
        private void UpdateChildrenInternal<T>(T entity, PropertyInfo property, bool isForeignKey) where T : class,IEntity
        {
            var type = typeof(T);
            var objType = property.PropertyType.GetGenericArguments()[0];
            var enumerable = typeof(IEnumerable<>).MakeGenericType(objType);

            var param = Expression.Parameter(type, "x");
            var body = Expression.Property(param, property);
            var lambda = Expression.Lambda(Expression.Convert(body, enumerable), property.Name, new[] { param });

            if (typeof(IEntity).IsAssignableFrom(objType))
            {
                var method = isForeignKey ? typeof(DataContext).GetMethod("UpdateForeignChildren") : typeof(DataContext).GetMethod("UpdateChildren");
                if (method != null)
                {
                    var generic = method.MakeGenericMethod(type, objType);
                    generic.Invoke(this, new object[] { entity, lambda });
                }
            }
        }

        /// <summary>
        /// Update the foreign Children of a parent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childSelector"></param>
        public void UpdateForeignChildren<T, TProperty>(T parent, Expression<Func<T, IEnumerable<TProperty>>> childSelector)
            where T : class, IEntity
            where TProperty : class, IEntity
        {
            var children = childSelector.Compile().Invoke(parent).ToList();
            foreach (var child in children) { AddOrUpdate(child); }

            var existingChildren = CreateSet<T>().Where(x => x.Id == parent.Id).SelectMany(childSelector).AsNoTracking().ToList();

            foreach (var child in existingChildren.Except(children)) { Entry(child).State = EntityState.Deleted; }
        }

        /// <summary>
        /// Uppdate the children of a parent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childSelector"></param>
        public void UpdateChildren<T, TProperty>(T parent, Expression<Func<T, IEnumerable<TProperty>>> childSelector)
            where T : class, IEntity
            where TProperty : class, IEntity
        {
            var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;
            var currentChildren = childSelector.Compile().Invoke(parent).ToList();
            var existingChildren = CreateSet<T>().Where(x => x.Id == parent.Id).SelectMany(childSelector).AsNoTracking().ToList();

            var addedChildren = currentChildren.Except(existingChildren).AsEnumerable();
            var deletedChildren = existingChildren.Except(currentChildren).AsEnumerable();

            foreach (var child in currentChildren) { AddOrUpdate(child); }
            foreach (var child in addedChildren) { stateManager.ChangeRelationshipState(parent, child, childSelector.Name, EntityState.Added); }
            foreach (var child in deletedChildren)
            {
                Entry(child).State = EntityState.Unchanged;
                stateManager.ChangeRelationshipState(parent, child, childSelector.Name, EntityState.Deleted);
            }
        }

        /// <summary>
        /// Gets the changed property of an entry.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetChangedProperties(DbEntityEntry dbEntry)
        {
            var propertyNames = dbEntry.State == EntityState.Added ? dbEntry.CurrentValues.PropertyNames : dbEntry.OriginalValues.PropertyNames;
            foreach (var propertyName in propertyNames)
            {
                if (IsValueChanged(dbEntry, propertyName))
                {
                    yield return propertyName;
                }
            }
        }

        /// <summary>
        /// Tests if the value changed.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static bool IsValueChanged(DbEntityEntry dbEntry, string propertyName)
        {
            return !Equals(OriginalValue(dbEntry, propertyName), CurrentValue(dbEntry, propertyName));
        }

        /// <summary>
        /// Gets the original value of an entry by property name.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string OriginalValue(DbEntityEntry dbEntry, string propertyName)
        {
            string originalValue = null;

            if (dbEntry.State == EntityState.Modified)
            {
                originalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null
                    ? null
                    : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
            }

            return originalValue;
        }

        /// <summary>
        /// Gets the current value of an entry by property name.
        /// </summary>
        /// <param name="dbEntry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string CurrentValue(DbEntityEntry dbEntry, string propertyName)
        {
            string newValue;

            try
            {
                newValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null
                    ? null
                    : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
            }
            catch (InvalidOperationException) // It will be invalid operation when its in deleted state. in that case, new value should be null
            {
                newValue = null;
            }

            return newValue;
        }

        #endregion ----- Methods -----
    }
}