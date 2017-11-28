using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for an entity.
    /// </summary>
    [MetadataType(typeof(EntityMetadata))]
    public abstract class Entity : EntityBase, IEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        public virtual string BusinessKey
        {
            get { return Id.ToString(); }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (ReferenceEquals(null, other)) { return false; }
            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("{0} | ID = {1} | Key = {2}", GetType().Name, Id, BusinessKey);
        }
    }
}