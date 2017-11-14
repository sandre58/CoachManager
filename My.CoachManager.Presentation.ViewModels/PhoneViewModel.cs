using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.ViewModels.Metadatas;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(PhoneMetadata))]
    public class PhoneViewModel : ContactViewModel
    {
        private string _value;
        public override string Value { get { return _value; } set { SetEntityProperty(() => _value = value, value, Value); } }
    }
}