using System;
using System.Collections;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public abstract class EntityModel : SelectModel, IEntityModel, IModifiable, IComparable
    {

        #region Fields

        private bool _isModified;

        #endregion

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Get the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as EntityModel;
            return !(other is null) && Id == other.Id;// && ReferenceEquals(this, other);
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
        /// Compares this instance to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int CompareTo(object obj)
        {
            if (obj is EntityModel other)
            {
                return Id.CompareTo(other.Id);
            }
            return -1;
        }

        #endregion Methods

        #region IModifiable

        /// <summary>
        /// Gets a value indicates if the entity has been modified.
        /// </summary>
        public bool IsModified() {
                var type = GetType();
                var isModified = _isModified;
                var complexIsModified =
                    type.GetProperties().Select(x => x.GetValue(this)).OfType<IModifiable>().ToList()
                        .Any(x => x.IsModified());
                var collectionIsModied =
                    type.GetProperties().Select(x => x.GetValue(this)).OfType<ICollection>().ToList().SelectMany(x => x.OfType<IModifiable>()).Any(x => x.IsModified());
                return isModified || complexIsModified || collectionIsModied;
        }

        /// <inheritdoc />
        /// <summary>
        /// Reset IsModified value.
        /// </summary>
        public void ResetModified()
        {
            var type = GetType();
            _isModified = false;
            type.GetProperties().Select(x => x.GetValue(this)).OfType<IModifiable>().ToList()
                    .ForEach(x => x.ResetModified());
            type.GetProperties().Select(x => x.GetValue(this)).OfType<ICollection>().ToList().SelectMany(x => x.OfType<IModifiable>()).ForEach(x => x.ResetModified());
        }

        #endregion

        #region IPropertyChanged

        /// <inheritdoc />
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="before">The before value.</param>
        /// <param name="after">The after value.</param>
        protected override void OnPropertyChanged(string propertyName, object before, object after)
        {
            base.OnPropertyChanged(propertyName,before,after);

            _isModified = true;
        }

        #endregion

    }
}