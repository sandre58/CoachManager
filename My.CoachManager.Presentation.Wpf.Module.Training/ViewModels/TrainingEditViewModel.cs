using System;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{
    public class TrainingEditViewModel : EditViewModel<TrainingModel>
    {
        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<TrainingDto, int>(ApiConstants.ApiTrainings, TrainingFactory.Get(Item, AppManager.Roster.Id, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override TrainingModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<TrainingDto>(ApiConstants.ApiTrainingsTraining, id);
            return TrainingFactory.Get(result);
        }

        /// <summary>
        /// Calls when load data is completed.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            
            if (Item != null && Item.Id == 0)
            {

                var date = GetParameter<DateTime>(ParametersConstants.Date);
                if (date != default) Item.Date = date;

                var startTime = GetParameter<TimeSpan>(ParametersConstants.StartTime);
                if (startTime != default)
                {
                    Item.StartTime = startTime;
                    Item.EndTime = startTime.Add(GetParameter<TimeSpan>(ParametersConstants.Duration));
                }

            }
            
            base.OnLoadDataCompleted();

        }

        #endregion Methods
    }
}