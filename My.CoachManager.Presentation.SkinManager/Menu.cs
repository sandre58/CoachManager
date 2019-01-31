using System;

namespace My.CoachManager.Presentation.SkinManager
{
    /// <summary>
    /// Defines a menu as a Name &amp; a URI to the ResourceDictionary
    /// </summary>
    public sealed class Menu : Skin
    {
        /// <summary>
        /// Create a menu class
        /// </summary>
        /// <param name="name">The name of accent.</param>
        /// <param name="uri">The URI of the ResourceDictionary.</param>
        public Menu(string name, Uri uri) : base(name, uri)
        {
        }
    }
}