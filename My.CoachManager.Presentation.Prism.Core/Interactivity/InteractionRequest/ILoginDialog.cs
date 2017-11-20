namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public interface ILoginDialog : IDialog
    {
        string Login { get; set; }

        string Password { get; set; }
    }
}