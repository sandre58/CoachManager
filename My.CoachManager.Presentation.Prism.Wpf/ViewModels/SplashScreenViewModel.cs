using System.Reflection;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public class SplashScreenViewModel : ScreenViewModel
    {

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SplashScreenViewModel"/>.
        /// </summary>
        public SplashScreenViewModel()
        {
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
        public string Version { get; }

        /// <summary>
        /// Get the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright { get; }

        #endregion Members

    }
}
