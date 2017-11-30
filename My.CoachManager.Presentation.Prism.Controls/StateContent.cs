using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class StateContent : ContentControl
    {
        #region Properties

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(object), typeof(StateContent), new PropertyMetadata());

        public object State
        {
            get { return GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register("Diameter", typeof(double), typeof(StateContent), new PropertyMetadata());

        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        #endregion Properties
    }
}