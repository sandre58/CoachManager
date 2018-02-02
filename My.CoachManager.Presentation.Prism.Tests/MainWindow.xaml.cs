using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Newtonsoft.Json;

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
            var test = new StringFilter("FullName", StringOperator.Contains, true)
            {
                Value = "TOTO"
            };

            JsonSerializer serializer = new JsonSerializer();

            var vm = new FilterViewModel(test, "coucou", LogicalOperator.Or);

            //IFormatter formatter = new BinaryFormatter();
            //XmlSerializer xs = new XmlSerializer(typeof(StringFilter));
            //using (FileStream wr = new FileStream("test.xml", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(wr, vm);
            //}

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"test.json"))
            {
                serializer.Serialize(file, vm);
            }

            FilterViewModel vm2 = null;

            using (StreamReader file = File.OpenText(@"test.json"))
            {
                vm2 = (FilterViewModel)serializer.Deserialize(file, typeof(FilterViewModel));
            }
        }
    }
}