/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:My.CoachManager.Presentation.Wpf"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.Ioc
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// The AutoWireViewModel attached property.
        /// </summary>
        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            new PropertyMetadata(false, AutoWireViewModelChanged));

        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                ViewModelProvider.AutoWireViewModelChanged(d, Bind);
        }

        /// <summary>
        /// Sets the DataContext of a View
        /// </summary>
        /// <param name="view">The View to set the DataContext on</param>
        /// <param name="viewModel">The object to use as the DataContext for the View</param>
        private static void Bind(object view, object viewModel)
        {
            if (view is FrameworkElement element)
                element.DataContext = viewModel;
        }

    }
}