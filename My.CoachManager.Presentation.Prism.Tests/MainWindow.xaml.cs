using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            List = new ObservableCollection<Tuple<int, string>>();
            Values = new ObservableCollection<Tuple<int, string>>();

            for (int i = 0; i < 10; i++)
            {
                List.Add(new Tuple<int, string>(i, " Item " + i));
            }
        }

        public ObservableCollection<Tuple<int, string>> List { get; set; }

        public ObservableCollection<Tuple<int, string>> Values { get; set; }
    }
}