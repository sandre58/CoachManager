namespace My.CoachManager.Presentation.Prism.Core.ViewModels
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
    }
}