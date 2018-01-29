using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.ViewModels;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = new StringFilter(typeof(PlayerDetailViewModel).GetProperty("FullName"), StringOperator.Contains, true)
            {
                Value = "TOTO"
            };

            XmlSerializer xs = new XmlSerializer(typeof(StringFilter));
            using (StreamWriter wr = new StreamWriter("test.xml"))
            {
                xs.Serialize(wr, test);
            }
        }
    }
}