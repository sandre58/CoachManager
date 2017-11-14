using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(DataEntityMetadata))]
    public class DataEntityViewModel : EntityViewModel, ILabelableViewModel, IOrderableViewModel
    {
        private string _label;
        public string Label { get { return _label; } set { SetProperty(ref _label, value); } }

        private string _description;
        public string Description { get { return _description; } set { SetProperty(ref _description, value); } }

        private string _code;
        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }

        private int _order;
        public int Order { get { return _order; } set { SetProperty(ref _order, value); } }
    }
}