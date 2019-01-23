using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;
using My.CoachManager.Domain.SquadModule.Aggregate;
using My.CoachManager.Domain.SquadModule.Services;

namespace My.CoachManager.Application.Services.RosterModule
{
    /// <summary>
    /// Implementation of the ISquadAppService class.
    /// </summary>
    public class SquadAppService : ISquadAppService
    {
        #region ---- Fields ----
        
        private readonly IRepository<Squad> _squadRepository;
        private readonly ICrudDomainService<Squad, SquadDto> _crudDomainService;
        private readonly ISquadDomainService _squadDomainService;
        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="SquadAppService"/> class.
        /// </summary>
        /// <param name="squadRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="squadDomainService"></param>
        public SquadAppService(IRepository<Squad> squadRepository, ICrudDomainService<Squad, SquadDto> crudDomainService, ISquadDomainService squadDomainService)
        {
            _squadRepository = squadRepository;
            _crudDomainService = crudDomainService;
            _squadDomainService = squadDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveSquad(int rosterId, SquadDto dto)
        {
            dto.RosterId = rosterId;
            return _crudDomainService.Save(dto, SquadFactory.CreateEntity, SquadFactory.UpdateEntity, x => _squadDomainService.Validate(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveSquad(SquadDto dto)
        {
            _crudDomainService.Remove(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public SquadDto GetSquadById(int id)
        {
            var entity = _squadRepository.GetEntity(id);
            return entity != null ? SquadFactory.Get(entity) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<SquadDto> GetSquads(int rosterId)
        {
            return _squadRepository.Query
                .Where(x => x.RosterId == rosterId)
                .Select(SquadSelectBuilder.SelectSquads()).ToList();
        }

        /// <summary>
        /// Gets Roster From Squad
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
       public RosterDto GetRosterFromSquad(int squadId)
        {
            var roster = _squadRepository.Query
                .Include(x => x.Roster)
                .SingleOrDefault(x => x.Id == squadId)
                ?.Roster;

            return RosterFactory.Get(roster);
        }

        #endregion Methods
    }
}