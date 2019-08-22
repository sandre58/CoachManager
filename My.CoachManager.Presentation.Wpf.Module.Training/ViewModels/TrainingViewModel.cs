using System.Windows.Input;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using My.CoachManager.Presentation.Wpf.Modules.Training.Resources;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{

    public class TrainingViewModel : ItemViewModel<TrainingModel, TrainingEditViewModel>
    {
        #region Members

        /// <summary>
        /// Gets or sets the edit attendance command.
        /// </summary>
        public ICommand EditAttendancesCommand { get; set; }

        #endregion

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            EditAttendancesCommand = new DelegateCommand(EditAttendances);

        }

        #endregion Initialization

        #region Data

        protected override TrainingModel LoadItemCore(int id)
        {
            return TrainingFactory.Get(ApiHelper.GetData<TrainingDto>(ApiConstants.ApiTrainingsTraining, id));
        }

        #endregion Data

        #region EditAttendance

        /// <summary>
        /// Edit attendances.
        /// </summary>
        protected void EditAttendances()
        {
            EventAggregator.GetEvent<EditTrainingAttendancesRequestEvent>().Publish(new EditItemRequestEventArgs(Item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok) Refresh();
            }));
        }
        #endregion

        #region PropertyChanged

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected override void OnItemChanged()
        {
            base.OnItemChanged();

            if (Item != null) Title = string.Format(TrainingResources.TrainingTitle, Item.Date.ToLongDateString());
        }

        #endregion PropertyChanged
    }
}