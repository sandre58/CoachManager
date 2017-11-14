using System.Collections.Generic;
using System.Reflection;
using My.CoachManager.Domain.Core;
using AutoMapper;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public static class AutoMapperExtension
    {
        static AutoMapperExtension()
        {
            var myAssembly = Assembly.GetExecutingAssembly();
            Mapper.Initialize(cfg => cfg.AddProfiles(myAssembly));
        }

        /// <summary>
        /// Map this entity to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <param name="source">The entity source.</param>
        /// <returns>The new object.</returns>
        public static TDest ToDto<TDest>(this IEntity source)
            where TDest : IEntityDto
        {
            return Mapper.Map<TDest>(source);
        }

        /// <summary>
        /// Map this dto to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <param name="source">The entity source.</param>
        /// <returns>The new object.</returns>
        public static TDest ToEntity<TDest>(this IEntityDto source)
            where TDest : IEntity
        {
            return Mapper.Map<TDest>(source);
        }

        /// <summary>
        /// Map this entity to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToDtos<TDest>(this IEnumerable<IEntity> source)
            where TDest : IEntityDto
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }

        /// <summary>
        /// Map this dtos to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToEntities<TDest>(this IEnumerable<IEntityDto> source)
            where TDest : IEntity
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }
    }
}