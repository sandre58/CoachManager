using System;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Contract for ‘UnitOfWork pattern’. For more
    /// related info see http://martinfowler.com/eaaCatalog/unitOfWork.html or
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// In this solution, the Unit Of Work is implemented using the out-of-box
    /// Entity Framework Context (EF 4.0) persistence engine.
    /// </summary>
    public interface IUnitOfWork : ISql, IDisposable
    {
        /// <summary>
        /// Commit all changes made in a context.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown.</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in  a context and refresh data.
        /// </summary>
        /// <remarks>If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins.</remarks>
        void CommitAndRefreshChanges();

        /// <summary>
        /// Rollback changes are not stored in the database at
        /// this moment. See references of UnitOfWork pattern.
        /// </summary>
        void RollbackChanges();
    }
}