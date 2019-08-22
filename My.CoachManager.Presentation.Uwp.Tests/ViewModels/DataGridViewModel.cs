using System.Collections.ObjectModel;

using My.CoachManager.Presentation.Uwp.Tests.Core.Models;
using My.CoachManager.Presentation.Uwp.Tests.Core.Services;

using Prism.Windows.Mvvm;

namespace My.CoachManager.Presentation.Uwp.Tests.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        private readonly ISampleDataService _sampleDataService;

        public DataGridViewModel(ISampleDataService sampleDataServiceInstance)
        {
            _sampleDataService = sampleDataServiceInstance;
        }

        public ObservableCollection<SampleOrder> Source => _sampleDataService.GetGridSampleData();
    }
}
