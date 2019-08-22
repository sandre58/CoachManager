using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Training.Resources;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{
    public class TrainingsAddViewModel : TrainingEditViewModel
    {
        #region  Members

        /// <summary>
        /// Get or sets days.
        /// </summary>
        public IList<string> AllDays { get; set; }

        /// <summary>
        /// Get or sets days.
        /// </summary>
        public IList<object> Days { get; set; }
        
        #endregion

        #region Methods

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

        protected override int SaveItemCore()
        {
            var firstDayOfWeek = DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek;
            var allDays = AllDays.ToList();
            var days = Days.Select(x => (DayOfWeek)((allDays.IndexOf(x.ToString()) + (int)firstDayOfWeek) % AllDays.Count));
            var count = ApiHelper.PostData<TrainingParametersDto, int>(ApiConstants.ApiTrainingsAdd,
                new TrainingParametersDto
                {
                    Days = days.ToArray(),
                    EndDate = Item.EndDate,
                    EndTime = Item.EndTime,
                    Place = Item.Place,
                    RosterId = AppManager.Roster.Id,
                    StartDate = Item.StartDate, 
                    StartTime = Item.StartTime
                });

            SavingSuccessMessage = string.Format(TrainingResources.AddedTrainingsSuccessMessage, count);
            return 0;
        }

        /// <summary>
        /// Calls when load data is completed.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            Mode = ScreenMode.Creation;
            Item.ResetModified();
        }
        
        #endregion Methods
    }
}