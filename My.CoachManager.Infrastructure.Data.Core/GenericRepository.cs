using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Core.Specification;
using My.CoachManager.Infrastructure.Data.Core.Extensions;

namespace My.CoachManager.Infrastructure.Data.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Repository Generic.
    /// </summary>
    /// <typeparam name="TEntity">Type Of Entity.</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region ----- Fields ------

        /// <summary>
        /// The current Unit of Work.
        /// </summary>
        private readonly IQueryableUnitOfWork _currentUnitOfWork;

        #endregion ----- Fields ------

        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="unitOfWork">A unit of work for this repository.</param>
        public GenericRepository(IQueryableUnitOfWork unitOfWork)
        {
            // Check preconditions

            // Set internal values
            _currentUnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #endregion ----- Constructor -----

        #region ----- IRepository<TEntity> Members -----

        #region ----- Properties -----

        /// <inheritdoc />
        /// <summary>
        /// Gets a unit of work in this repository.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public IUnitOfWork UnitOfWork => _currentUnitOfWork;

        #endregion ----- Properties -----

        #region ----- CUD Methods -----

        /// <inheritdoc />
        /// <summary>
        /// Add an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        public virtual void Add(TEntity item)
        {
            // Check item
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            CreateSet().Add(item);
        }

        /// <summary>
        /// Add an Entities Object in Context.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            // Check item
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // Get the dbset

            if (CreateSet() is DbSet<TEntity> databaseSet)
            {
                databaseSet.AddRange(items);
            }
        }

        /// <summary>
        /// Remove an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        public virtual void Remove(TEntity item)
        {
            // Check item
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet<TEntity> objectSet = CreateSet();

            // Attach object to unit of works and delete this
            objectSet.Attach(item);

            // Delete object to DbSet Object
            objectSet.Remove(item);
        }

        /// <summary>
        /// Modify an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        public virtual void Modify(TEntity item)
        {
            // Check arguments
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Apply changes for item object
            _currentUnitOfWork.SetModified(item);
        }

        /// <summary>
        /// Modify a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        public virtual void Modify(ICollection<TEntity> items)
        {
            // Check arguments
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // For each element in collection apply changes
            foreach (TEntity item in items)
            {
                if (item != null)
                {
                    _currentUnitOfWork.SetModified(item);
                }
            }
        }

        /// <summary>
        /// Modify an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        public virtual void Attach(TEntity item)
        {
            // Check arguments
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Apply changes for item object
            //_currentUnitOfWork.AddOrUpdate(item);
            _currentUnitOfWork.Attach(item);
        }

        /// <summary>
        /// Modify a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        public virtual void Attach(ICollection<TEntity> items)
        {
            // Check arguments
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // For each element in collection apply changes
            foreach (TEntity item in items)
            {
                if (item != null)
                {
                    //_currentUnitOfWork.AddOrUpdate(item);
                    _currentUnitOfWork.Attach(item);
                }
            }
        }

        #endregion ----- CUD Methods -----

        #region ----- CountMethods -----

        /// <summary>
        /// Count the element from the filter.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <returns>The number of element.</returns>
        public virtual int CountByFilter(Expression<Func<TEntity, bool>> filter)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Add filter condition
            var result = objectSet.Where(filter).Count();

            return result;
        }

        /// <summary>
        /// Count the element from the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>The number of element.</returns>
        public virtual int CountBySpec(ISpecification<TEntity> specification)
        {
            // Checking arguments for this query
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Add Specification condition
            var result = objectSet.Where(specification.SatisfiedBy()).Count();

            return result;
        }

        #endregion ----- CountMethods -----

        #region ----- Any Methods -----

        /// <inheritdoc />
        /// <summary>
        /// Provide an interface to request and manage a data source.
        /// </summary>
        /// <typeparam name="TEntity">A class.</typeparam>
        public virtual bool Exists(int id)
        {
            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            return objectSet.Any(t => t.Id == id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if any element check rules.
        /// </summary>
        /// <param name="filter">
        /// Lambda Expression for filtering Query in where parameters.
        /// </param>
        /// <returns>
        /// A value indicating whether at least one element match with condition.
        /// </returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> filter)
        {
            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Return true if any match with filter condition
            return objectSet.Any(filter);
        }

        /// <summary>
        /// Check if any element check rules.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>A value indicating whether at least one element match with condition.</returns>
        public virtual bool Any(ISpecification<TEntity> specification)
        {
            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Return true if any match with filter condition
            return objectSet.Any(specification.SatisfiedBy());
        }

        #endregion ----- Any Methods -----

        #region ----- Get All Methods -----

        /// <summary>
        /// Get All Elements.
        /// </summary>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>All TEntity.</returns>
        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            // Call Private Methods
            return GetFilteredElements<int, TEntity>(x => x, null, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>All TEntity.</returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            // Call Private Methods
            return GetFilteredElements<int, TResult>(selectResult, null, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List of Elements.</returns>
        public virtual IEnumerable<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Methods
            return GetFilteredElements(x => x, null, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List of Elements.</returns>
        public virtual IEnumerable<TEntity> GetAll(QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Methods
            return GetFilteredElements<int, TEntity>(x => x, null, null, true, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List of Elements.</returns>
        public virtual IEnumerable<TResult> GetAll<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Methods
            return GetFilteredElements(selectResult, null, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List of Elements.</returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Methods
            return GetFilteredElements<int, TResult>(selectResult, null, null, false, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements.</returns>
        public virtual IEnumerable<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Methods
            return GetFilteredElements(x => x, null, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements.</returns>
        public virtual IEnumerable<TEntity> GetAll(QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Methods
            return GetFilteredElements<int, TEntity>(x => x, null, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By and Number of Element.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements and Number of Element.</returns>
        public virtual Tuple<IEnumerable<TEntity>, int> GetAllAndCount<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements(x => x, null, orderByExpression, ascending, pageIndex, pageCount, true, null, includes);
        }

        /// <summary>
        /// Get All Elements Ordered By and Number of Element.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements and Number of Element.</returns>
        public virtual Tuple<IEnumerable<TEntity>, int> GetAllAndCount(QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements<int, TEntity>(x => x, null, null, false, pageIndex, pageCount, true, order, includes);
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements.</returns>
        public virtual IEnumerable<TResult> GetAll<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Methods
            return GetFilteredElements(selectResult, null, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>List Of Elements.</returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Methods
            return GetFilteredElements<int, TResult>(selectResult, null, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        #endregion ----- Get All Methods -----

        #region ----- Get Methods With Filter -----

        /// <summary>
        /// Get Elements of Entity By filter, with Includes.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // Call Private Methode
            return GetFilteredElements<int, TEntity>(x => x, filter, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // Call Private Methode
            return GetFilteredElements<int, TResult>(selectResult, filter, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By filter, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetByFilter<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Methode
            return GetFilteredElements(x => x, filter, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By filter, with Ordering and Includes.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Methode
            return GetFilteredElements<int, TEntity>(x => x, filter, null, false, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetByFilter<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Method
            return GetFilteredElements(selectResult, filter, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Method
            return GetFilteredElements<int, TResult>(selectResult, filter, null, false, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetByFilter<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Method
            return GetFilteredElements(x => x, filter, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Method
            return GetFilteredElements<int, TEntity>(x => x, filter, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        public virtual Tuple<IEnumerable<TEntity>, int> GetByFilterAndCount<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            // Call Private Method
            return GetFilteredElements(x => x, filter, orderByExpression, ascending, pageIndex, pageCount, true, null, includes);
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        public virtual Tuple<IEnumerable<TEntity>, int> GetByFilterAndCount(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // Call Private Method
            return GetFilteredElements<int, TEntity>(x => x, filter, null, false, pageIndex, pageCount, true, order, includes);
        }

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetByFilter<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements(selectResult, filter, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements<int, TResult>(selectResult, filter, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        public Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements(selectResult, filter, orderByExpression, ascending, pageIndex, pageCount, true, null, includes);
        }

        /// <summary>
        /// Gets the by filter and count.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">The select result.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="order">The order.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        public Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            // Call Private Method
            return GetFilteredElements<int, TResult>(selectResult, filter, null, false, pageIndex, pageCount, true, order, includes);
        }

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        public Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // Call Private Method
            return GetFilteredElements(selectResult, filter, f => f, true, 0, 0, true, null, includes);
        }

        #endregion ----- Get Methods With Filter -----

        #region ----- Get Methods With Specification -----

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Includes.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, params Expression<Func<TEntity, object>>[] includes)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return GetBySpecElements<int, TEntity>(x => x, specification, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking query arguments
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return GetBySpecElements<int, TResult>(selectResult, specification, null, true, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetBySpec<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            return GetBySpecElements(x => x, specification, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering and Includes.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return GetBySpecElements<int, TEntity>(x => x, specification, null, false, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetBySpec<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            return GetBySpecElements(selectResult, specification, orderByExpression, ascending, 0, 0, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering and Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return GetBySpecElements<int, TResult>(selectResult, specification, null, false, 0, 0, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetBySpec<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements(x => x, specification, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        public virtual IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements<int, TEntity>(x => x, specification, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetBySpec<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements(selectResult, specification, orderByExpression, ascending, pageIndex, pageCount, false, null, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        public virtual IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements<int, TResult>(selectResult, specification, null, false, pageIndex, pageCount, false, order, includes).Item1;
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        public virtual Tuple<IEnumerable<TResult>, int> GetBySpecAndCount<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(nameof(orderByExpression));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements(selectResult, specification, orderByExpression, ascending, pageIndex, pageCount, true, null, includes);
        }

        /// <summary>
        /// Gets the by spec and count.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">The select result.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="order">The order.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        public virtual Tuple<IEnumerable<TResult>, int> GetBySpecAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, params Expression<Func<TEntity, object>>[] includes)
        {
            // Checking arguments for this query
            if (selectResult == null)
            {
                throw new ArgumentNullException(nameof(selectResult));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageIndexArgumentException, nameof(pageIndex));
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException(ValidationMessageResources.RepositoryPageCountArgumentException, nameof(pageCount));
            }

            return GetBySpecElements<int, TResult>(selectResult, specification, null, false, pageIndex, pageCount, true, order, includes);
        }

        #endregion ----- Get Methods With Specification -----

        #endregion ----- IRepository<TEntity> Members -----

        #region ----- Private Methods -----

        /// <summary>
        /// Method for verify if Context Exist and transform the Entity Creation query to IObjectSet.
        /// </summary>
        /// <returns>Entity Creation query.</returns>
        protected virtual DbSet<TEntity> CreateSet()
        {
            if (_currentUnitOfWork == null)
            {
                throw new InvalidOperationException("Context cannot be null");
            }

            return _currentUnitOfWork.CreateSet<TEntity>();
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By filter, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="getFilteredCount">If true, Launch a count on objectSet Query after filtering and before pagination.</param>
        /// <param name="order">The order.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object, Count of records (0 if not used).</returns>
        protected virtual Tuple<IEnumerable<TResult>, int> GetFilteredElements<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, bool getFilteredCount, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Add Filter condition
            if (filter != null)
            {
                objectSet = objectSet.Where(filter);
            }

            var count = 0;
            if (getFilteredCount)
            {
                count = objectSet.Select(selectResult).Count();
            }

            // Return List of Entity Object and count
            return GetElements(objectSet, count, selectResult, orderByExpression, ascending, pageIndex, pageCount, order, includes);
        }

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and Includes.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="getFilteredCount">If true, Launch a count on objectSet Query after filtering and before pagination.</param>
        /// <param name="order">The order.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object, Count of records (0 if not used).</returns>
        protected virtual Tuple<IEnumerable<TResult>, int> GetBySpecElements<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, bool getFilteredCount, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Create IObjectSet for this particular type and query this
            IQueryable<TEntity> objectSet = CreateSet();

            // Add Specification condition
            if (specification != null)
            {
                objectSet = objectSet.Where(specification.SatisfiedBy());
            }

            var count = 0;
            if (getFilteredCount)
            {
                count = objectSet.Count();
            }

            // Return List of Entity Object and count
            return GetElements(objectSet, count, selectResult, orderByExpression, ascending, pageIndex, pageCount, order, includes);
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="objectSet">The object set.</param>
        /// <param name="count">The count of element.</param>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="order">The order.</param>
        /// <param name="includes">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object, Count of records (0 if not used).</returns>
        private Tuple<IEnumerable<TResult>, int> GetElements<TKey, TResult>(IQueryable<TEntity> objectSet, int count, Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, QueryOrder<TEntity> order, params Expression<Func<TEntity, object>>[] includes)
        {
            // Add All Include Object in Query
            objectSet = objectSet.Include(includes);

            // Ordering Query
            if (orderByExpression != null)
            {
                objectSet = ascending ? objectSet.OrderBy(orderByExpression) : objectSet.OrderByDescending(orderByExpression);
            }

            if (order != null)
            {
                objectSet = objectSet.ApplyQueryOrder(order);
            }

            // Cut Result for Paging
            if (pageIndex >= 0 && pageCount > 0)
            {
                objectSet = objectSet.Skip(pageIndex * pageCount).Take(pageCount);
            }

            var result = objectSet.Select(selectResult);

            // Return List of Entity Object and count
            return Tuple.Create(result.AsEnumerable(), count);
        }

        #endregion ----- Private Methods -----

        #region ----- Get Methods -----

        /// <summary>
        /// Get Element by it's ID.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>Single or Default TEntity.</returns>
        public virtual TEntity GetEntity(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetFilteredElements<int, TEntity>(x => x, x => x.Id.Equals(id), null, true, 0, 0, false, null, includes).Item1.SingleOrDefault();
        }

        #endregion ----- Get Methods -----
    }
}