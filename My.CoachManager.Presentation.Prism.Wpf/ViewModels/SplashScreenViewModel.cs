using System.Reflection;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public class SplashScreenViewModel : ScreenViewModel, ISplashScreenViewModel
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private string _version;
        private string _copyright;
        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SplashScreenViewModel"/>.
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="dialogService"></param>
        /// <param name="logger"></param>
        public SplashScreenViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<UpdateSplashScreenMessageRequestEvent>().Subscribe(OnUpdateMessage, ThreadOption.UIThread, true);

            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();

            Version = string.Format(GlobalResources.Version, assembly.GetName().Version);
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            private set { SetProperty(ref _version, value); }
        }

        /// <summary>
        /// Get the message.
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set { SetProperty(ref _message, value); }
        }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            private set { SetProperty(ref _copyright, value); }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Call when the message is udpated.
        /// </summary>
        /// <param name="message"></param>
        protected void OnUpdateMessage(string message)
        {
            Message = message;
        }

        #endregion Methods
    }
}