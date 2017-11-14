﻿using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
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
        public static TDest ToDto<TDest>(this EntityViewModelBase source)
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
        public static TDest ToViewModel<TDest>(this IEntityDto source)
            where TDest : EntityViewModelBase
        {
            return Mapper.Map<TDest>(source);
        }

        /// <summary>
        /// Map this entity to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToDtos<TDest>(this IEnumerable<EntityViewModelBase> source)
            where TDest : IEntityDto
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }

        /// <summary>
        /// Map this dtos to object source.
        /// </summary>
        /// <typeparam name="TDest">The destination type.</typeparam>
        /// <returns>The new object.</returns>
        public static IEnumerable<TDest> ToViewModels<TDest>(this IEnumerable<IEntityDto> source)
            where TDest : EntityViewModelBase
        {
            return Mapper.Map<IEnumerable<TDest>>(source);
        }
    }
}