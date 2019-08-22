using FluentValidation.Results;

using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Services;
using My.CoachManager.Domain.SeasonModule.Aggregate;

namespace My.CoachManager.Domain.SeasonModule.Services
{
    public class SeasonDomainService : ReferenceDomainService<Season>, ISeasonDomainService
    {
        #region Fields

        private readonly IRepository<Roster> _rosterRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonDomainService"/>.
        /// </summary>
        public SeasonDomainService(IRepository<Season> seasonRepository, IRepository<Roster> rosterRepository) : base(seasonRepository)
        {
            _rosterRepository = rosterRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if the item can be removed.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CanBeRemoved(Season item)
        {
            return !IsUsed(item.Id);
        }

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUsed(int id)
        {
            return _rosterRepository.Any(x => x.SeasonId == id);
        }

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ValidationResult Validate(Season entity)
        {
            var validator = new SeasonValidator(Repository);
            return validator.Validate(entity);
        }

        #endregion Methods
    }
}
