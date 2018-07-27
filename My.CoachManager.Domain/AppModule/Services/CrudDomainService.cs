using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.AppModule.Services
{
    /// <summary>
    /// The crud domain service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TBaseDto">The type of the base dto.</typeparam>
    /// <seealso cref="QM.Absolu.Domain.AppModule.Services.ICrudDomainService{TEntity, TBaseDTO}" />
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
        public void Save(IEnumerable<TBaseDto> entitiesBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory)
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
                Add(item, createFactory);
            }

            foreach (var item in entitiesBaseList.Where(w => w.CrudStatus == CrudStatus.Updated))
            {
                Modify(item, modifyFactory);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Save in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        /// <param name="modifyFactory">The modify factory.</param>
        public TBaseDto Save(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory, Func<TBaseDto, TEntity, bool> modifyFactory)
        {
            // Delete
            if (entityBase.CrudStatus == CrudStatus.Deleted)
            {
                Remove(entityBase);
            }

            if (entityBase.CrudStatus == CrudStatus.Created)
            {
                return Add(entityBase, createFactory);
            }

            if (entityBase.CrudStatus == CrudStatus.Updated)
            {
                return Modify(entityBase, modifyFactory);
            }

            return entityBase;
        }

        /// <summary>
        /// Remove in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        public void Remove(TBaseDto entityBase)
        {
            var entity = _entityRepository.GetEntity(entityBase.Id);

            if (entity == null)
            {
                return;
            }

            _entityRepository.Remove(entity);
            _entityRepository.UnitOfWork.Commit();
        }

        /// <inheritdoc />
        /// <summary>
        /// Add in database the entity base.
        /// </summary>
        /// <param name="entityBase">The entity base.</param>
        /// <param name="createFactory">The create factory.</param>
        public TBaseDto Add(TBaseDto entityBase, Func<TBaseDto, TEntity> createFactory)
        {
            var entity = createFactory.Invoke(entityBase);
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
        public TBaseDto Modify(TBaseDto entityBase, Func<TBaseDto, TEntity, bool> modifyFactory)
        {
            var entity = _entityRepository.GetEntity(entityBase.Id);

            if (entity == null)
            {
                throw new NullReferenceException();
            }

            modifyFactory.Invoke(entityBase, entity);
            _entityRepository.Modify(entity);
            _entityRepository.UnitOfWork.Commit();

            entityBase.CrudStatus = CrudStatus.Unchanged;

            return entityBase;
        }
    }
}
