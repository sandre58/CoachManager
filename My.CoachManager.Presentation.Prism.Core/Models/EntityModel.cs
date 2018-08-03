using System;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public abstract class EntityModel : ModelBase, IEntityModel, IModifiable
    {

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

        /// <inheritdoc />
        /// <summary>
        /// Reset IsModified value.
        /// </summary>
        public void ResetModified()
        {
            IsModified = false;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as EntityModel;
            return !(other is null) && ReferenceEquals(this, other);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Methods

        #region IModifiable

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicates if the entity has been modified.
        /// </summary>
        public bool IsModified { get; private set; }

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

            if(propertyName != nameof(IsModified)) IsModified = true;
        }

        #endregion

    }
}