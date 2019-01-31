using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Core.Constants;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Training.Resources;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;

namespace My.CoachManager.Presentation.Modules.Training.ViewModels
{
    public class TrainingsAddViewModel : TrainingEditViewModel
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region  Members

        /// <summary>
        /// Get or sets days.
        /// </summary>
        public IList<string> AllDays { get; set; }

        /// <summary>
        /// Get or sets days.
        /// </summary>
        public IList<object> Days { get; set; }

        /// <summary>
        /// Get or sets count trainings.
        /// </summary>
        public int CountTrainingsAdd { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingEditViewModel"/>.
        /// </summary>
        public TrainingsAddViewModel(ITrainingService trainingService) : base(trainingService)
        {
            _trainingService = trainingService;
            NewItemMessage = TrainingResources.NewTrainings;
        }

        #endregion Constructors

        #region Methods
        
        #region Data

        protected override int SaveItemCore()
        {
            var firstDayOfWeek = DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek;
            var allDays = AllDays.ToList();
            var days = Days.Select(x => (DayOfWeek)((allDays.IndexOf(x.ToString()) + (int)firstDayOfWeek) % AllDays.Count));
            CountTrainingsAdd = _trainingService.AddTrainings(Thread.CurrentPrincipal.Identity.GetRosterId(), Item.StartDate, Item.EndDate, Item.StartTime, Item.EndTime, Item.Place, days.ToArray()).Length;
            return 0;
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var firstDayOfWeek = DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek;
            AllDays = DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).DayNames.ToList().Rotate((int)firstDayOfWeek);
        }

        protected override TrainingModel LoadItemCore(int id)
        {
            Days = new List<object>();

                return TrainingFactory.Create(GetParameter<DateTime>(ParametersConstants.StartDate), GetParameter<DateTime>(ParametersConstants.EndDate), GetParameter<TimeSpan>(ParametersConstants.StartTime), GetParameter<TimeSpan>(ParametersConstants.StartTime).Add(GetParameter<TimeSpan>(ParametersConstants.Duration)), string.Empty);
        }

        /// <summary>
        /// Calls when load data is completed.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            Mode = ScreenMode.Creation;
            Item.ResetModified();
        }

        /// <summary>
        /// After Save.
        /// </summary>
        protected override void OnSaveCompleted()
        {
            NotificationManager.ShowSuccess(string.Format(TrainingResources.AddedTrainingsSuccessMessage, CountTrainingsAdd));
            Close(DialogResult.Ok);
        }

        #endregion Data

        #endregion Methods
    }
}