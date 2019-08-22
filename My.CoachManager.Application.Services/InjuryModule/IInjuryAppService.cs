using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.InjuryModule
{
    /// <summary>
    /// Interface defining the player application services.
    /// </summary>
    public interface IInjuryAppService
    {
        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        InjuryDto GetInjuryById(int id);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveInjury(int playerId, InjuryDto dto);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveInjury(int id);
    }
}
