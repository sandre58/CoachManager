using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    public abstract class DataEntity : Entity, ILabelable, IOrderable
    {
        public string Label { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int Order { get; set; }
    }
}