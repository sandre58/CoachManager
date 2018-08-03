using System;
using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Services.Persons;
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
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetPlayerDetails(id);
        }

        /// <summary>
        /// Get Player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerDto GetPlayer(int id)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetPlayer(id);
        }

        /// <summary>
        /// Create Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PlayerDto CreateOrUpdate(PlayerDto dto)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().CreateOrUpdate(dto);
        }

        /// <summary>
        /// Remove Player.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Remove(PlayerDto dto)
        {
            ServiceLocator.Current.GetInstance<IPlayerAppService>().Remove(dto);
        }

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryFromBirthdate(DateTime date)
        {
            return ServiceLocator.Current.GetInstance<IPlayerAppService>().GetCategoryFromBirthdate(date);
        }

        /// <summary>
        /// Get categories list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDto> GetCountries()
        {
            return ServiceLocator.Current.GetInstance<ICountryAppService>().GetList();
        }
    }
}