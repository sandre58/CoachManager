namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// State of DTO for Entity Updates
    /// </summary>
    public enum CrudStatus
    {
        /// <summary>
        /// No change on the DTO
        /// </summary>
        Unchanged = 0,

        /// <summary>
        /// New DTO for insert in EF
        /// </summary>
        Created = 1,

        /// <summary>
        /// Update an existing DTO
        /// </summary>
        Updated = 2,

        /// <summary>
        /// DTO must be deleted
        /// </summary>
        Deleted = 3
    }
}
