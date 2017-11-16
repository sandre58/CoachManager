using My.CoachManager.CrossCutting.Core.Metadatas;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(ContactMetadata))]
    public abstract class Contact : Entity
    {
        public string Label { get; set; }

        public bool Default { get; set; }

        public string Value { get; set; }

        public int PersonId { get; set; }
        
        public Person Person { get; set; }
    }
}