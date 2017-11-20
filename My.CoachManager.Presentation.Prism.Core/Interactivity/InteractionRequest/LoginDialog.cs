namespace My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest
{
    public class LoginDialog : Dialog, ILoginDialog
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}