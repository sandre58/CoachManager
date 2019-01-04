using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Core.Commands
{
    public interface IKeyCommand : ICommand
    {
        /// <summary>
        /// Gets or sets header.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Gets color.
        /// </summary>
        object Color { get; }

        /// <summary>
        /// Sets icon.
        /// </summary>
        object Icon { get; }
    }
}
