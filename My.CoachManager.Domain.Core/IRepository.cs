using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using My.CoachManager.Domain.Core.Specification;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Repository Interface.
    /// </summary>
    /// <typeparam name="TEntity">Entity Object.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region ----- Property -----

        /// <summary>
        /// Gets Pattern Unit of Work.
        /// </summary>
        /// <value>
        /// Pattern Unit of Work.
        /// </value>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets Query.
        /// </summary>
        IQueryable<TEntity> Query { get; }

        #endregion ----- Property -----

        #region ----- Add Methods -----

        /// <summary>
        /// Add an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Add(TEntity item);

        #endregion

        #region ----- Remove Methods -----

        /// <summary>
        /// Remove an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Remove(TEntity item);

        #endregion

        #region ----- Modify Methods -----

        /// <summary>
        /// Modify an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Modify(TEntity item);

        /// <summary>
        /// Modify a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        void Modify(ICollection<TEntity> items);

        #endregion

        #region ----- Attach Methods -----

        /// <summary>
        /// Attach an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Attach(TEntity item);

        /// <summary>
        /// Attach a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        void Attach(ICollection<TEntity> items);

        #endregion

        #region ----- Get Methods -----

        /// <summary>
        /// Get Element by it's ID.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>Single or Default TEntity.</returns>
        TEntity GetEntity(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements.
        /// </summary>
        /// <param name="action">action Parameters.</param>
        /// <returns>All TEntity.</returns>
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>All TEntity.</returns>
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List of Elements.</returns>
        IEnumerable<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List of Elements.</returns>
        IEnumerable<TEntity> GetAll(QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List of Elements.</returns>
        IEnumerable<TResult> GetAll<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List of Elements.</returns>
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements.</returns>
        IEnumerable<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements.</returns>
        IEnumerable<TEntity> GetAll(QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By and Number of Element.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordered Field.</typeparam>
        /// <param name="orderByExpression">Ordered Expression.</param>
        /// <param name="ascending">Direction of sort.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements and Number of Element.</returns>
        Tuple<IEnumerable<TEntity>, int> GetAllAndCount<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By and Number of Element.
        /// </summary>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements and Number of Element.</returns>
        Tuple<IEnumerable<TEntity>, int> GetAllAndCount(QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

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
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements.</returns>
        IEnumerable<TResult> GetAll<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get All Elements Ordered By.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="order">Ordered Expression.</param>
        /// <param name="pageIndex">Index of Page.</param>
        /// <param name="pageCount">Number of Elements by Page.</param>
        /// <param name="action">action Parameters.</param>
        /// <returns>List Of Elements.</returns>
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectResult, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        #endregion ----- Get All Methods -----

        #region ----- Count Methods -----

        /// <summary>
        /// Count the element from the filter.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <returns>The number of element.</returns>
        int CountByFilter(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Count the element from the specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>The number of element.</returns>
        int CountBySpec(ISpecification<TEntity> specification);

        #endregion ----- Count Methods -----

        #region ----- Any Methods -----

        /// <summary>
        /// Check if an id exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        /// Check if any element check rules.
        /// </summary>
        /// <param name="filter">
        /// Lambda Expression for filtering Query in where parameters.
        /// </param>
        /// <returns>
        /// A value indicating whether at least one element match with condition.
        /// </returns>
        bool Any(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Check if any element check rules.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>A value indicating whether at least one element match with condition.</returns>
        bool Any(ISpecification<TEntity> specification);

        #endregion ----- Any Methods -----

        #region ----- Get Methods With filter -----

        /// <summary>
        /// Get Elements of Entity By filter, with action.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By filter, with Ordering and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        IEnumerable<TEntity> GetByFilter<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By filter, with Ordering and action.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        IEnumerable<TResult> GetByFilter<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering and action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        IEnumerable<TEntity> GetByFilter<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object.</returns>
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        Tuple<IEnumerable<TEntity>, int> GetByFilterAndCount<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        Tuple<IEnumerable<TEntity>, int> GetByFilterAndCount(Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Gets the by filter and count.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">The select result.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="order">The order.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="action">The action.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        IEnumerable<TResult> GetByFilter<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with Selected Columns of Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Selected column of Entity Object.</returns>
        IEnumerable<TResult> GetByFilter<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements Entity By filter, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="filter">Lambda Expression for filtering Query in where parameters.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Entity Object and count of Element.</returns>
        Tuple<IEnumerable<TResult>, int> GetByFilterAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        #endregion ----- Get Methods With filter -----

        #region ----- Get Methods With Specification -----

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with action.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        IEnumerable<TEntity> GetBySpec<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering and action.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        IEnumerable<TResult> GetBySpec<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering and action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        IEnumerable<TEntity> GetBySpec<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements of Entity By Specification Pattern, with Ordering, Paging and action.
        /// </summary>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements of Entity Object.</returns>
        IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        IEnumerable<TResult> GetBySpec<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="order">Lambda Expression for Ordering Query.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object.</returns>
        IEnumerable<TResult> GetBySpec<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Get Elements with selected Columns of Entity By Specification Pattern, with Ordering, Paging and action.
        /// </summary>
        /// <typeparam name="TKey">Type of Ordering.</typeparam>
        /// <typeparam name="TResult">Type of Selected return.</typeparam>
        /// <param name="selectResult">Lambda Expression for Select on query.</param>
        /// <param name="specification">Specification Used for Filtering Query.</param>
        /// <param name="orderByExpression">Lambda Expression for Ordering Query.</param>
        /// <param name="ascending">Direction of Ordering.</param>
        /// <param name="pageIndex">Index of page.</param>
        /// <param name="pageCount">Number of elements in each page.</param>
        /// <param name="action">Array of String for adding include in query.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        Tuple<IEnumerable<TResult>, int> GetBySpecAndCount<TKey, TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        /// <summary>
        /// Gets the by spec and count.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selectResult">The select result.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="order">The order.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="action">The action.</param>
        /// <returns>List of Elements with selected Columns of Entity Object and count.</returns>
        Tuple<IEnumerable<TResult>, int> GetBySpecAndCount<TResult>(Expression<Func<TEntity, TResult>> selectResult, ISpecification<TEntity> specification, QueryOrder<TEntity> order, int pageIndex, int pageCount, Func<IQueryable<TEntity>, IQueryable<TEntity>> action = null);

        #endregion ----- Get Methods With Specification -----

    }
}
