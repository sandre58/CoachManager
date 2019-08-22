using System.Reflection;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Wpf.Core.Events;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Resources;
using Prism.Events;

namespace My.CoachManager.Presentation.Wpf.ViewModels
{
    public class StatusBarViewModel : ScreenViewModel
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
        protected override void Initialize()
        {
            base.Initialize();

            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly?.GetCustomAttribute<AssemblyCopyrightAttribute>();

            Version = string.Format(CoachManagerResources.Version, assembly?.GetName().Version);
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";

            Message = MessageResources.Ready;

            EventAggregator.GetEvent<UpdateStatusBarMessageRequestEvent>().Subscribe(OnMessageChanged, ThreadOption.UIThread, true);
        }
        #endregion Constructor

        #region Methods

        /// <summary>
        /// Call when the message changed.
        /// </summary>
        /// <param name="message"></param>
        protected void OnMessageChanged(string message)
        {
            Message = message;
        }

        #endregion Methods
    }
}