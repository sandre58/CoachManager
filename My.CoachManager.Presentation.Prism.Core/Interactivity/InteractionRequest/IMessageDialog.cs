namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public interface IMessageDialog : IDialog
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        MessageDialogType Type { get; set; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        MessageDialogStyle Style { get; set; }
    }
}