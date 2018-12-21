using System;
using System.Collections.Generic;
using FluentValidation.Results;
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
        /// <param name="validateEntity"></param>
        void Save(IEnumerable<TBaseDto> entitiesBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null);

        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        /// <param name="validateEntity"></param>
        int Save(
            TBaseDto entityBase,
            Func<TBaseDto, TEntity> createFactory,
            Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null);

        /// <summary>
        /// Add in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="validateEntity"></param>
        int Add(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory, Func<TEntity, ValidationResult> validateEntity = null);

        /// <summary>
        /// Modify in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        /// <param name="validateEntity"></param>
        int Modify(TBaseDto entityBase, Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null);

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        void Remove(TBaseDto entityBase);

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        void Remove(int id);
    }
}
