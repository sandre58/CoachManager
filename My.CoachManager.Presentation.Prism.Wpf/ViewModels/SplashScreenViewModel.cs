using System.Reflection;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public class SplashScreenViewModel : ScreenViewModel
    {

        #region Members

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Get the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright { get; private set; }

        #endregion Members

        #region Initialisation


        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();

            Version = string.Format(GlobalResources.Version, assembly.GetName().Version);
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";

            Message = StatusResources.DefaultStatusMessage;
        }
        #endregion Constructor

        #region Methods

        /// <summary>
        /// Updates message.
        /// </summary>
        /// <param name="message"></param>
        public void UpdateMessage(string message)
        {
            Message = message + "...";
        }

        #endregion
    }
}
