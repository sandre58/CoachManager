using System;
using System.Collections.Generic;

using CommonServiceLocator;

using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.InjuryModule;
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
        public int SavePlayer(PlayerDto dto)
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
        public CategoryDto GetCategoryFromDate(DateTime fromDate, DateTime toDate)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetCategoryFromDate(fromDate, toDate);
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public InjuryDto GetInjuryById(int id)
        {
            return ServiceLocator.Current.GetInstance<IInjuryAppService>().GetInjuryById(id);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveInjury(int playerId, InjuryDto dto)
        {
            return ServiceLocator.Current.GetInstance<IInjuryAppService>().SaveInjury(playerId, dto);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveInjury(InjuryDto dto)
        {
            ServiceLocator.Current.GetInstance<IInjuryAppService>().RemoveInjury(dto);
        }
    }
}
