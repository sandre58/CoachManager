﻿using System;
using System.Collections.Generic;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Services.Persons;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class PersonService : IPersonService
    {
        /// <summary>
        /// Get Player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerDetailsDto GetPlayerDetails(int id)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetPlayerDetails(id);
        }

        /// <summary>
        /// Get Player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerDto GetPlayer(int id)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetPlayer(id);
        }

        /// <summary>
        /// Create Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PlayerDto CreateOrUpdate(PlayerDto dto)
        {
            return UnityFactory.Resolve<IPlayerAppService>().CreateOrUpdate(dto);
        }

        /// <summary>
        /// Remove Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Remove(PlayerDto dto)
        {
            UnityFactory.Resolve<IPlayerAppService>().Remove(dto);
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return UnityFactory.Resolve<IPlayerAppService>().GetCategoryFromBirthdate(date);
        }

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDto> GetCountries()
        {
            return UnityFactory.Resolve<ICountryAppService>().GetList();
        }
    }
}