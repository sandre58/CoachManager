using System;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.TrainingModule.Aggregate;
using My.CoachManager.Domain.TrainingModule.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;

namespace My.CoachManager.Application.Services.TrainingModule
{
    /// <summary>
    /// Implementation of the ITrainingAppService class.
    /// </summary>
    public class TrainingAppService : ITrainingAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Training> _trainingRepository;
        private readonly IRepository<RosterPlayer> _rosterPlayerRepository;

        private readonly ICrudDomainService<Training, TrainingDto> _crudDomainService;
        private readonly ITrainingDomainService _trainingDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingAppService"/> class.
        /// </summary>
        /// <param name="trainingRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="trainingDomainService"></param>
        /// <param name="rosterPlayerRepository"></param>
        public TrainingAppService(IRepository<Training> trainingRepository,
            ICrudDomainService<Training, TrainingDto> crudDomainService,
            ITrainingDomainService trainingDomainService,
            IRepository<RosterPlayer> rosterPlayerRepository)
        {
            _trainingRepository = trainingRepository;
            _crudDomainService = crudDomainService;
            _trainingDomainService = trainingDomainService;
            _rosterPlayerRepository = rosterPlayerRepository;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveTraining(int rosterId, TrainingDto dto)
        {
            dto.RosterId = rosterId;
           return _crudDomainService.Save(dto, TrainingFactory.CreateEntity, TrainingFactory.UpdateEntity, x => _trainingDomainService.Validate(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveTraining(TrainingDto dto)
        {
            _crudDomainService.Remove(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public TrainingDto GetTrainingById(int id)
        {
            var entity = _trainingRepository
                .Query
                .Include(x => x.Attendances)
                    .ThenInclude(x => x.RosterPlayer)
                        .ThenInclude(x => x.Player)
                            .ThenInclude(x => x.Category)
                .Include(x => x.Attendances)
                    .ThenInclude(x => x.RosterPlayer)
                        .ThenInclude(x => x.Squad)
                .FirstOrDefault(x => x.Id == id);
            return entity != null ? TrainingFactory.Get(entity) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<TrainingDto> GetTrainings(int rosterId)
        {
            return _trainingRepository.Query
                .Where(x => x.RosterId == rosterId)
                .Select(TrainingSelectBuilder.SelectTrainings()).ToList();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<TrainingDto> AddTrainings(int rosterId, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, string place, IList<DayOfWeek> days)
        {
            var dates = new List<DateTime>();
            var trainings = new List<Training>();

            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
            {
                if (days.Contains(date.DayOfWeek))
                {
                    dates.Add(date);
                }
            }

            foreach (var date in dates)
            {
                var training = TrainingFactory.CreateTraining(rosterId, date, startTime, endTime, place);

                var result = _trainingDomainService.Validate(training);

                    if (!result.IsValid)
                    {
                        throw new ValidationBusinessException(ValidationMessageResources.InvalidFields, result.Errors.Select(x => x.ErrorMessage).ToList());
                    }

                _trainingRepository.Add(training);

                trainings.Add(training);
            }
            
            _trainingRepository.UnitOfWork.Commit();

            return trainings.Select(TrainingFactory.Get).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveTrainingAttendances(int trainingId, IList<TrainingAttendanceDto> attendances)
        {
            var training = _trainingRepository.GetEntity(trainingId);
            training.Attendances = new List<TrainingAttendance>(attendances.Select(x => TrainingFactory.CreateAttendance(trainingId, x)));

            _trainingRepository.Modify(training);
            _trainingRepository.UnitOfWork.Commit();

            return training.Id;

        }
    
        /// <summary>
        /// Gets players for a specific training.
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public IList<RosterPlayerDto> GetPlayersForTraining(int trainingId)
        {
            var training = _trainingRepository.GetEntity(trainingId);

            var players = _rosterPlayerRepository.Query
                .Include(x => x.Player)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Squad)
                .Where(x => x.RosterId == training.RosterId && (!x.Player.FromDate.HasValue || x.Player.FromDate <= training.EndDate));

            return players.Select(TrainingSelectBuilder.SelectPlayersForTraining()).ToList();
        }

        #endregion Methods
    }
}