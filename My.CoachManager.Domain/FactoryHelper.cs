using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain
{
    public static class FactoryHelper
    {
        /// <summary>
        /// Update entities list.
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dtos"></param>
        /// <param name="entities"></param>
        /// <param name="createFactory"></param>
        /// <param name="modifyFactory"></param>
        public static void UpdateListEntity<TDto, TEntity>(IEnumerable<TDto> dtos, ICollection<TEntity> entities, Func<TDto, TEntity> createFactory, Action<TDto, TEntity> modifyFactory)
            where TEntity : class, IEntity
            where TDto : EntityDto
        {
            if (dtos != null && entities != null)
            {
                // Delete
                var toDelete = entities.Where(x => !dtos.Select(y => y.Id).Contains(x.Id)).ToList();
                toDelete.ForEach(x => entities.Remove(x));

                // Update
                var toUpdate = entities.Where(x => dtos.Select(y => y.Id).Contains(x.Id)).ToList();
                toUpdate.ForEach(x =>
                    modifyFactory.Invoke(dtos.First(y => y.Id == x.Id), entities.First(y => y.Id == x.Id)));

                // Add
                var toAdd = dtos.Where(x => !entities.Select(y => y.Id).Contains(x.Id)).ToList();
                toAdd.ForEach(x => entities.Add(createFactory(x)));
            }
        }

    }
}
