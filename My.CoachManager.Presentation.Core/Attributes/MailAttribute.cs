using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Presentation.Core.Attributes
{
    public class EmailAttribute : DataTypeAttribute
    {
        private readonly EmailAddressAttribute _wrappedAttribute;

        public EmailAttribute()
            : base(DataType.EmailAddress)
        {
            _wrappedAttribute = new EmailAddressAttribute();
        }

        public override bool IsValid(object value)
        {
            if (value == null || value.Equals(string.Empty))
            {
                return true;
            }

            return _wrappedAttribute.IsValid(value);
        }
    }
}