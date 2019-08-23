using System.Diagnostics;

namespace My.CoachManager.Presentation.Wpf.Core.Helpers
{
    public static class ApplicationHelper
    {

        /// <summary>
        /// Restart application.
        /// </summary>
        /// <param name="raiseInfoMessage"></param>
        public static void Restart(bool raiseInfoMessage = true)
        {
            Process.Start(System.Windows.Application.ResourceAssembly.Location);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
