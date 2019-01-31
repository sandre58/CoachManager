using System.Windows.Input;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Training.Resources;
using My.CoachManager.Presentation.Modules.Training.Views;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Modules.Training.ViewModels
{
    public class TrainingViewModel : ItemViewModel<TrainingModel, TrainingEditView>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the edit attendance command.
        /// </summary>
        public ICommand EditAttendancesCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingViewModel"/>.
        /// </summary>
        public TrainingViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        #endregion Constructors

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            EditAttendancesCommand = new DelegateCommand(EditAttendances, CanEditAttedances);
        }

        #endregion Initialization

        #region EditAttendance

        /// <summary>
        /// Edit attendances.
        /// </summary>
        protected void EditAttendances()
        {
            DialogManager.ShowEditDialog<TrainingAttendancesEditView>(Item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok) Refresh();
            });
        }

        /// <summary>
        /// Edit attendances.
        /// </summary>
        protected bool CanEditAttedances()
        {
            return true;
        }
        #endregion

        #region Data

        protected override TrainingModel LoadItemCore(int id)
        {
            return TrainingFactory.Get(_trainingService.GetTrainingById(id));
        }

        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();

            Title = string.Format(TrainingResources.TrainingTitle, Item.Date.ToLongDateString());
        }

        #endregion Data
    }
}