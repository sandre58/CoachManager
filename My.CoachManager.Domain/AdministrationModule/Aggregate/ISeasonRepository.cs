using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.AdministrationModule.Aggregate
{
    /// <summary>
    /// Interface used for representing a ICountryRepository.
    /// </summary>
    public interface ISeasonRepository : IGenericRepository<Season>
    {
    }
}