using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.CrossCutting.Resources.Entities;
using My.CoachManager.Presentation.Core.Attributes.Validation;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class TrainingModel : EntityModel, IAppointment
    {

        /// <summary>
        /// Initialise a new instance of <see cref="PersonModel"/>.
        /// </summary>
        public TrainingModel()
        {
            Rules.Add(nameof(StartDate), ValidationMessageResources.StartDateLessOrEqualsThanEndDateMessage, o =>
            {
                var item = (TrainingModel)o;
                return item.StartDate.Date <= item.EndDate.Date;
            });

            Rules.Add(nameof(EndDate), ValidationMessageResources.StartDateLessOrEqualsThanEndDateMessage, o =>
            {
                var item = (TrainingModel)o;
                return item.StartDate.Date <= item.EndDate.Date;
            });

            Rules.Add(nameof(StartTime), ValidationMessageResources.StartTimeLessThanEndTimeMessage, o =>
            {
                var item = (TrainingModel)o;
                return item.StartDate.Date != item.EndDate.Date || item.StartTime < item.EndTime;
            });

            Rules.Add(nameof(EndTime), ValidationMessageResources.StartTimeLessThanEndTimeMessage, o =>
            {
                var item = (TrainingModel)o;
                return item.StartDate.Date != item.EndDate.Date || item.StartTime < item.EndTime;
            });
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int? RosterId { get; set; }


        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        [Display(Name = "Place", ResourceType = typeof(TrainingResources))]
        public virtual string Place { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "StartDate", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(StartTime))]
        [ValidateProperty(nameof(EndTime))]
        [ValidateProperty(nameof(EndDate))]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Display(Name = "EndDate", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(StartTime))]
        [ValidateProperty(nameof(EndTime))]
        [ValidateProperty(nameof(StartDate))]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cancelled value.
        /// </summary>
        [Display(Name = "IsCancelled", ResourceType = typeof(TrainingResources))]
        public virtual bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Attendances { get; set; }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Presents {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Present).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); } }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Absents
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Absent).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Unknown
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Unknown).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Injured
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Injured).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> InSelections
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.InSelection).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Apologized
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Apology).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public ObservableCollection<TrainingAttendanceModel> Resting
        {
            get { return Attendances != null ? Attendances.Where(x => x.Attendance == Attendance.Resting).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "Date", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(StartTime))]
        [ValidateProperty(nameof(EndTime))]
        public virtual DateTime Date {
            get => StartDate.Date;
            set
            {
                StartDate = new DateTime(value.Year, value.Month, value.Day, StartDate.Hour, StartDate.Minute, StartDate.Second);
               EndDate = new DateTime(value.Year, value.Month, value.Day, EndDate.Hour, EndDate.Minute, EndDate.Second);
            } }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "StartTime", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(EndTime))]
        public virtual TimeSpan StartTime
        {
            get => StartDate.TimeOfDay;
            set => StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, value.Hours, value.Minutes, value.Seconds);
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "EndTime", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(StartTime))]
        public virtual TimeSpan EndTime
        {
            get => EndDate.TimeOfDay;
            set => EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, value.Hours, value.Minutes, value.Seconds);
        }

        /// <summary>
        /// Gets or set Title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets IsPerformed value.
        /// </summary>
        public bool IsPerformed => !IsCancelled && EndDate < DateTime.Now;
    }
}
