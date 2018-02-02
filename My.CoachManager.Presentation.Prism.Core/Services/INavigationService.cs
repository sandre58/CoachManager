using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Prism.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening page using the MVVM pattern in WPF.
    /// </summary>
    public interface INavigationService
    {
        void NavigateTo<TView>(IEnumerable<KeyValuePair<string, object>> parameters = null);

        void NavigateTo(Type typeView, IEnumerable<KeyValuePair<string, object>> parameters = null);

        void NavigateTo(string pagePath, IEnumerable<KeyValuePair<string, object>> parameters = null);

        void NavigateTo(string pagePath, object paramValue, string paramKey = "Id");

        void NavigateTo<TView>(object paramValue = null, string paramKey = "Id");

        void NavigateTo(Type typeView, object paramValue, string paramKey = "Id");
    }
}