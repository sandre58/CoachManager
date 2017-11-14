using System.Reflection;
using My.CoachManager.Presentation.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class SplashScreenViewModel : DialogViewModel
    {
        #region Fields

        private static SplashScreenViewModel _instance;
        private string _version;
        private string _copyright;
        private string _message;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="SplashScreenViewModel"/>.
        /// </summary>
        private SplashScreenViewModel()
        {
            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();

            Version = assembly.GetName().Version.ToString();
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Get the instance.
        /// </summary>
        public static SplashScreenViewModel Instance
        {
            get
            {
                return _instance ?? (_instance = new SplashScreenViewModel());
            }
        }

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.ShellResources.ApplicationTitle;
            }
            set
            {
            }
        }

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            private set
            {
                _version = value;
                NotifyOfPropertyChange();
            }
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
            set
            {
                _message = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            private set
            {
                _copyright = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties
    }
}