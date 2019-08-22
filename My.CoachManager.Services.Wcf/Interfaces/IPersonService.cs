using System;
using System.Collections.Generic;
using System.ServiceModel;

using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Administration Service Interface.
    /// </summary>
    [ServiceContract]
    public interface IPersonService
    {
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<CountryDto> GetCountries();

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<PlayerDto> GetPlayers();

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        PlayerDto GetPlayerById(int playerId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        int SavePlayer(PlayerDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        void RemovePlayer(PlayerDto dto);

        /// <summary>
        /// Get category from birthdate.
        /// </summary>
        /// <returns></returns>>
        [OperationContract]
        CategoryDto GetCategoryFromDate(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        InjuryDto GetInjuryById(int id);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int SaveInjury(int playerId, InjuryDto dto);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void RemoveInjury(InjuryDto dto);
    }
}
