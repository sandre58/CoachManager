using System;
using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Application.Services.PersonModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class PersonService : IPersonService
    {

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<CountryDto> GetCountries()
        {
            return ServiceLocator.Current.GetInstance<ICountryAppService>().GetCountries();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<PlayerDto> GetPlayers()
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetPlayers();
        }

        /// <summary>
        /// Get Player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerDto GetPlayerById(int id)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetPlayerById(id);
        }

        /// <summary>
        /// Create Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PlayerDto SavePlayer(PlayerDto dto)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().SavePlayer(dto);
        }

        /// <summary>
        /// Remove Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void RemovePlayer(PlayerDto dto)
        {
            ServiceLocator.Current.GetInstance<IPlayerAppService>().RemovePlayer(dto);
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetCategoryFromBirthdate(date);
        }
    }
}