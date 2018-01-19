using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Login.Core;
using My.CoachManager.Presentation.Prism.Modules.Login.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Login.Views;
using Prism.Modularity;

namespace My.CoachManager.Presentation.Prism.Modules.Login
{
    public class LoginModule : IModule
    {
        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<ILoginViewModel, LoginViewModel>();

            // Register Views
            Locator.RegisterType<ILoginView, LoginView>();
        }
    }
}