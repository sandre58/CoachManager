using System.Collections.Generic;
using System.Reflection;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.ViewModels;
using AutoMapper;

namespace My.CoachManager.Presentation.ViewModels.Mapping
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
        public static TDest ToDto<TDest>(this EntityViewModel source)
            where TDest : EntityDto
        {
            return Mapper.Map<TDest>(source);
        }

        /// <summary>
        /// Map this dto to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <param name="source">The entity source.</param>
        /// <returns>The new object.</returns>
        public static TDest ToViewModel<TDest>(this EntityDto source)
            where TDest : EntityViewModel
        {
            return Mapper.Map<TDest>(source);
        }

        /// <summary>
        /// Map this entity to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToDtos<TDest>(this IEnumerable<EntityViewModel> source)
            where TDest : EntityDto
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }

        /// <summary>
        /// Map this dtos to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToViewModels<TDest>(this IEnumerable<EntityDto> source)
            where TDest : EntityViewModel
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }
    }
}