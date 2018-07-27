using System;
using System.Collections.Generic;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.AppModule.Services
{
    /// <summary>
    /// The crud domain service interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TBaseDto">The type of the base dto.</typeparam>
    public interface ICrudDomainService<TEntity, TBaseDto>
        where TEntity : class, IEntity, new()
        where TBaseDto : EntityDto, new()
    {
        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entitiesBase">The entities base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        void Save(IEnumerable<TBaseDto> entitiesBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory);

        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        TBaseDto Save(
            TBaseDto entityBase,
            Func<TBaseDto, TEntity> createFactory,
            Func<TBaseDto, TEntity, bool> modifyFactory);

        /// <summary>
        /// Add in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        TBaseDto Add(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory);

        /// <summary>
        /// Modify in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        TBaseDto Modify(TBaseDto entityBase, Func<TBaseDto, TEntity, bool> modifyFactory);

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        void Remove(TBaseDto entityBase);
    }
}
