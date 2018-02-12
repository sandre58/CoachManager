using System;
using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IPersonService
    {
        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        PlayerDetailsDto GetPlayerDetails(int playerId);

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        PlayerDto GetPlayer(int playerId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        PlayerDto CreateOrUpdate(PlayerDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        void Remove(PlayerDto dto);

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        CategoryDto GetCategoryFromBirthdate(DateTime date);

        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<CountryDto> GetCountries();
    }
}