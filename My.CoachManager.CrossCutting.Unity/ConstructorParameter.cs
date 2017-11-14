namespace My.CoachManager.CrossCutting.Unity
{
    /// <summary>
    /// Used for inject constructor.
    /// </summary>
    public class ConstructorParameter
    {
        #region ----- Properties -----

        /// <summary>
        /// Gets or Sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the value of the parameter.
        /// </summary>
        public object Parameter { get; set; }

        #endregion ----- Properties -----
    }
}