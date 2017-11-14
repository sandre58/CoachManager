using System;
using Caliburn.Micro;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation
{
    public static class ScreenLocator
    {
        /// <summary>
        /// Create an instance of <see cref="TScreenViewModel"/>.
        /// </summary>
        /// <typeparam name="TScreenViewModel"></typeparam>
        /// <returns></returns>
        public static TScreenViewModel Create<TScreenViewModel>() where TScreenViewModel : IScreenViewModel
        {
            return Activator.CreateInstance<TScreenViewModel>();
        }
    }
}