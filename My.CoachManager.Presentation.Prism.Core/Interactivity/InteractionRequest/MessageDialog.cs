namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public class MessageDialog : Dialog, IMessageDialog
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public MessageDialogType Type { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public MessageDialogStyle Style { get; set; }
    }
}