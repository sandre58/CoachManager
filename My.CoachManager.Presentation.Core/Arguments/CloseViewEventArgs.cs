namespace My.CoachManager.Presentation.Core.Arguments
{
    public class CloseViewEventArgs : System.EventArgs
    {
        #region DialogResult

        public bool DialogResult { get; set; }

        #endregion DialogResult

        #region Constructors

        public CloseViewEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }

        #endregion Constructors
    }
}