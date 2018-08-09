using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.AppModule.Services
{
    /// <summary>
    /// The crud domain service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TBaseDto">The type of the base dto.</typeparam>
    /// <seealso cref="ICrudDomainService{TEntity,TBaseDto}" />
    public class CrudDomainService<TEntity, TBaseDto> : ICrudDomainService<TEntity, TBaseDto>
        where TEntity : class, IEntity, new()
        where TBaseDto : EntityDto, new()
    {
        /// <summary>
        /// The entity repository.
        /// </summary>
        private readonly IRepository<TEntity> _entityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudDomainService{TEntity, TBaseDTO}"/> class.
        /// </summary>
        /// <param name="entityRepository">The entity repository.</param>
        public CrudDomainService(IRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entitiesBase">The entities base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        /// <param name="validateEntity"></param>
        public void Save(IEnumerable<TBaseDto> entitiesBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null)
        {
            var entitiesBaseList = entitiesBase.ToList();

            var removedItem = entitiesBaseList.Where(w => w.CrudStatus == CrudStatus.Deleted).ToList();

            // Delete
            foreach (var item in removedItem)
            {
                Remove(item);
                entitiesBaseList.Remove(item);
                ((IList<TBaseDto>)entitiesBase).Remove(item);
            }

            foreach (var item in entitiesBaseList.Where(w => w.CrudStatus == CrudStatus.Created))
            {
                Add(item, createFactory, validateEntity);
            }

            foreach (var item in entitiesBaseList.Where(w => w.CrudStatus == CrudStatus.Updated))
            {
                Modify(item, modifyFactory, validateEntity);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        /// <param name="validateEntity"></param>
        public TBaseDto Save(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null)
        {
            // Delete
            if (entityBase.CrudStatus == CrudStatus.Deleted)
            {
                Remove(entityBase);
            }

            if (entityBase.CrudStatus == CrudStatus.Created)
            {
                return Add(entityBase, createFactory, validateEntity);
            }

            if (entityBase.CrudStatus == CrudStatus.Updated)
            {
                return Modify(entityBase, modifyFactory, validateEntity);
            }

            return entityBase;
        }

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        public void Remove(TBaseDto entityBase)
        {
            Remove(entityBase.Id);
        }

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        public void Remove(int id)
        {
            var entity = _entityRepository.GetEntity(id);

            if (entity == null)
            {
                return;
            }

            _entityRepository.Remove(entity);
            _entityRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Add in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="validateEntity"></param>
        public TBaseDto Add(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory, Func<TEntity, ValidationResult> validateEntity = null)
        {
            var entity = createFactory.Invoke(entityBase);

            if (validateEntity != null)
            {
                var result = validateEntity.Invoke(entity);

                if (!result.IsValid)
                {
                    throw new ValidationBusinessException(ValidationMessageResources.InvalidFields, result.Errors.Select(x => x.ErrorMessage).ToList());
                }
            }
            
            _entityRepository.Add(entity);
            _entityRepository.UnitOfWork.Commit();

            entityBase.CrudStatus = CrudStatus.Unchanged;
            entityBase.Id = entity.Id;

            return entityBase;
        }

        /// <summary>
        /// Modify in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        /// <param name="validateEntity"></param>
        public TBaseDto Modify(TBaseDto entityBase, Func<TBaseDto, TEntity, bool> modifyFactory, Func<TEntity, ValidationResult> validateEntity = null)
        {
            var entity = _entityRepository.GetEntity(entityBase.Id);
            modifyFactory.Invoke(entityBase, entity);

            if (entity == null)
            {
                throw new NullReferenceException();
            }

            if (validateEntity != null)
            {
                var result = validateEntity.Invoke(entity);

                if (!result.IsValid)
                {
                    throw new ValidationBusinessException(ValidationMessageResources.InvalidFields, result.Errors.Select(x => x.ErrorMessage).ToList());
                }
            }

            _entityRepository.Modify(entity);
            _entityRepository.UnitOfWork.Commit();

            entityBase.CrudStatus = CrudStatus.Unchanged;

            return entityBase;
        }
    }
}
