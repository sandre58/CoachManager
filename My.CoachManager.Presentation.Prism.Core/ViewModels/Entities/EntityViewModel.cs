namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Entities
{
    public abstract class EntityViewModel : EntityViewModelBase, IEntityViewModel
    {
        #region Fields

        private int _id;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get the Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as EntityViewModel;
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

        #endregion Methods
    }
}