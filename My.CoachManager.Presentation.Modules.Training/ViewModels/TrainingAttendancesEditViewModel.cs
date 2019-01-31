using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Training.Resources;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Modules.Training.ViewModels
{
    public class TrainingAttendancesEditViewModel : EditViewModel<TrainingModel>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or set toogle attendance command.
        /// </summary>
        public DelegateCommand<Attendance?> ToggleAttendanceToCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingEditViewModel"/>.
        /// </summary>
        public TrainingAttendancesEditViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
            NewItemMessage = TrainingResources.EditAttencance;
            EditItemMessage = TrainingResources.EditAttencance;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();
            
            ToggleAttendanceToCommand = new DelegateCommand<Attendance?>(ToggleAttendanceTo);
        }

        #endregion Initialization

        #region Data

        protected override int SaveItemCore()
        {
            return _trainingService.SaveTrainingAttendances(Item.Id, Item.Attendances.Select(TrainingFactory.Get).ToArray());
        }

        protected override TrainingModel LoadItemCore(int id)
        {
            return TrainingFactory.Get(_trainingService.GetTrainingById(id));
        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (!Item.Attendances.Any())
            {
                    var players = _trainingService.GetPlayersForTraining(Item.Id).Select(RosterFactory.Get);
                    Item.Attendances = players.Select(x => TrainingFactory.CreateAttendance(x, x.IsInjuredAtDate(Item.StartDate) ? Attendance.Injured : Attendance.Unknown)).ToObservableCollection();
            }

            base.OnLoadDataCompleted();
        }

        #endregion Data

        #region ToggleAttendance

        /// <summary>
        /// Toggle attendance.
        /// </summary>
        /// <param name="attendance"></param>
        protected void ToggleAttendanceTo(Attendance? attendance)
        {
            Item.Attendances.ForEach(x =>
            {
                if (attendance != null) x.Attendance = attendance.Value;
            });
        }

        #endregion

        #endregion Methods
    }
}