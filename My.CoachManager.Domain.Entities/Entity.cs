using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    public abstract class Entity : EntityBase, IEntity
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual int Id { get; set; }

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
            return string.Format("{0} | ID = {1}", GetType().Name, Id);
        }
    }
}