
using System.ComponentModel;
using System.Threading;

namespace My.CoachManager.Presentation.Core.ComponentModel
{
    /// <summary>
    /// The abortable background worker.
    /// </summary>
    public class AbortableBackgroundWorker : BackgroundWorker
    {
        /// <summary>
        /// The worker thread.
        /// </summary>
        private Thread _workerThread;

        /// <summary>
        /// Aborts this instance.
        /// </summary>
        public void Abort()
        {
            if (_workerThread == null)
            {
                return;
            }

            _workerThread.Abort();
            _workerThread = null;
        }

        /// <summary>
        /// Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            _workerThread = Thread.CurrentThread;

            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                // We must set Cancel property to true !
                e.Cancel = true;

                // Prevents ThreadAbortException propagation
                Thread.ResetAbort();
            }
        }
    }
}
