namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// List Dto interface.
    /// </summary>
    public interface IEntityDto : IEntityDtoBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        int Id { get; set; }
    }
}