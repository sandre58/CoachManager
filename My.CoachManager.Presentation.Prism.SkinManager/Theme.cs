using System;

namespace My.CoachManager.Presentation.Prism.SkinManager
{
    /// <summary>
    /// Defines a theme as a Name &amp; a URI to the ResourceDictionary
    /// </summary>
    public sealed class Theme : Skin
    {
        /// <summary>
        /// Create a theme class
        /// </summary>
        /// <param name="name">The name of theme.</param>
        /// <param name="uri">The URI of the ResourceDictionary.</param>
        public Theme(string name, Uri uri) : base(name, uri)
        {
        }
    }
}