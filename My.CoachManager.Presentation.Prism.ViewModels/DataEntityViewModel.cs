using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for an item containing Code, Label, Description and Order.
    /// </summary>
    [MetadataType(typeof(DataEntityMetadata))]
    public class DataEntityViewModel : EntityViewModel, ILabelableViewModel, IOrderableViewModel
    {
        private string _label;

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get { return _label; } set { SetProperty(ref _label, value); } }

        private string _description;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get { return _description; } set { SetProperty(ref _description, value); } }

        private string _code;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }

        private int _order;

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int Order { get { return _order; } set { SetProperty(ref _order, value); } }
    }
}