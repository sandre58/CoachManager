using System.Reflection;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.SplashScreen.Core;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.SplashScreen.ViewModels
{
    public class SplashScreenViewModel : ScreenViewModel, ISplashScreenViewModel
    {
        #region Fields

        private string _version;
        private string _copyright;
        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SplashScreenViewModel"/>.
        /// </summary>
        public SplashScreenViewModel()
        {
            Locator.GetInstance<IEventAggregator>().GetEvent<UpdateSplashScreenMessageRequestEvent>().Subscribe(OnUpdateMessage, ThreadOption.UIThread, true);

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