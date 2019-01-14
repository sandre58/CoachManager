using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.TrainingModule.Aggregate;
using My.CoachManager.Domain.TrainingModule.Services;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Application.Services.TrainingModule
{
    /// <summary>
    /// Implementation of the ITrainingAppService class.
    /// </summary>
    public class TrainingAppService : ITrainingAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Training> _trainingRepository;

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
        public TrainingAppService(IRepository<Training> trainingRepository,
            ICrudDomainService<Training, TrainingDto> crudDomainService,
            ITrainingDomainService trainingDomainService)
        {
            _trainingRepository = trainingRepository;
            _crudDomainService = crudDomainService;
            _trainingDomainService = trainingDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveTraining(TrainingDto dto)
        {
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
            var entity = _trainingRepository.GetEntity(id);
            return entity != null ? TrainingFactory.Get(entity) : null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<TrainingDto> GetTrainings()
        {
            return _trainingRepository.GetAll(TrainingSelectBuilder.SelectTrainings()).ToList();
        }

        #endregion Methods
    }
}