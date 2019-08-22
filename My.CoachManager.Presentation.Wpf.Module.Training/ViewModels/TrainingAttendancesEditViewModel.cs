using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{
    public class TrainingAttendancesEditViewModel : EditViewModel<TrainingModel>
    {
        #region Members

        /// <summary>
        /// Gets or set toogle attendance command.
        /// </summary>
        public DelegateCommand<Attendance?> ToggleAttendanceToCommand { get; set; }

        #endregion

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            ToggleAttendanceToCommand = new DelegateCommand<Attendance?>(ToggleAttendanceTo);
        }

        #endregion Initialization

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<TrainingParametersDto, int>(ApiConstants.ApiTrainingsAttendances, new TrainingParametersDto
            {
                TrainingId = Item.Id,
                Attendances = Item.Attendances.Select(TrainingFactory.Get).ToArray()
            });
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
            if (!Item.Attendances.Any())
            {
                var result = ApiHelper.GetData<IList<RosterPlayerDto>>(ApiConstants.ApiTrainingsPlayers, Item.Id);
                var players = result.Select(RosterFactory.Get);
                Item.Attendances = players.Select(x => TrainingFactory.CreateAttendance(x, x.IsInjuredAtDate(Item.StartDate) ? Attendance.Injured : Attendance.Unknown)).ToObservableCollection();
            }

            base.OnLoadDataCompleted();

        }

        #region ToggleAttendance

        /// <summary>
        /// Toggle attendance.
        /// </summary>
        /// <param name="attendance"></param>
        protected void ToggleAttendanceTo(Attendance? attendance)
        {
            foreach (var item in Item.Attendances)
            {
                if (attendance != null) item.Attendance = attendance.Value;
            }
        }

        #endregion


        #endregion Methods
    }
}