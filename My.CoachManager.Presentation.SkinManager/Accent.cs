using System;

namespace My.CoachManager.Presentation.SkinManager
{
    /// <summary>
    /// Defines a accent as a Name &amp; a URI to the ResourceDictionary
    /// </summary>
    public sealed class Accent : Skin
    {
        /// <summary>
        /// Create a accent class
        /// </summary>
        /// <param name="name">The name of accent.</param>
        /// <param name="uri">The URI of the ResourceDictionary.</param>
        public Accent(string name, Uri uri) : base(name, uri)
        {
        }
    }
}