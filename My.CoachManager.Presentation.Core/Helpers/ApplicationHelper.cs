using System.Diagnostics;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;

namespace My.CoachManager.Presentation.Core.Helpers
{
    public static class ApplicationHelper
    {

        /// <summary>
        /// Restart application.
        /// </summary>
        /// <param name="raiseInfoMessage"></param>
        public static void Restart(bool raiseInfoMessage = true)
        {
            if (raiseInfoMessage)
                DialogManager.ShowInformationDialog(MessageResources.Restart, MessageDialogButtons.Ok);

            Process.Start(System.Windows.Application.ResourceAssembly.Location);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
