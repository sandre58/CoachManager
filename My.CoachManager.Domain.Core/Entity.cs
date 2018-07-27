using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity.
    /// </summary>
    [MetadataType(typeof(EntityMetadata))]
    public abstract class Entity : IEntity, IAuditable
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated user.
        /// </summary>
        public string ModifiedBy { get; set; }

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
            return $"{GetType().Name} | ID = {Id} | Key = {BusinessKey}";
        }
    }
}