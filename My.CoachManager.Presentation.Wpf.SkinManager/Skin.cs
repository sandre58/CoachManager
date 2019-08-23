using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows;

namespace My.CoachManager.Presentation.Wpf.SkinManager
{
    /// <summary>
    /// Defines a accent as a Name &amp; a URI to the ResourceDictionary
    /// </summary>
    public abstract class Skin : IEquatable<Skin>
    {
        /// <summary>
        /// The humna readable name of the accent
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The URI of the accent ResourceDictionary
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// The URI of the accent ResourceDictionary
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// The URI of the accent ResourceDictionary
        /// </summary>
        public ResourceDictionary Resources { get; private set; }

        /// <summary>
        /// Create a accent class
        /// </summary>
        /// <param name="name">The name of accent.</param>
        /// <param name="uri">The URI of the ResourceDictionary.</param>
        /// <param name="label"></param>
        public Skin(string name, Uri uri, string label = "")
        {
            Name = name;
            Uri = uri;

            if (Uri != null)
                Resources = Application.LoadComponent(Uri) as ResourceDictionary;

            if (label != "")
            {
                Label = label;
            }
            else
            {
                var resourceManager = new ResourceManager(Assembly.GetEntryAssembly()?.GetName().Name + ".Resources.Strings.SkinResources", Assembly.GetEntryAssembly() ?? throw new InvalidOperationException());
                var lb = resourceManager.GetString(name);

                if (lb != "")
                {
                    Label = lb;
                }
                else
                {
                    Label = name;
                }
            }
        }

        /// <summary>
        /// Determines whether two accents instances are equal.
        /// </summary>
        /// <param name="other">The accent to compare with the current accent.</param>
        /// <returns>true if the specified accent is equal to the current accent; otherwise, false.</returns>
        public bool Equals(Skin other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current accent.</param>
        /// <returns>true if the specified object is equal to the current accent; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Skin && Equals((Skin)obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current accent.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// The equality operator (==) returns true if its operands are equal, false otherwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Skin left, Skin right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// The inequality operator (!=) returns false if its operands are equal, true otherwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Skin left, Skin right)
        {
            return !Equals(left, right);
        }

        private sealed class NameEqualityComparer : IEqualityComparer<Skin>
        {
            public bool Equals(Skin x, Skin y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name);
            }

            public int GetHashCode(Skin obj)
            {
                return obj.Name?.GetHashCode() ?? 0;
            }
        }

        /// <summary>
        /// SecondaryAccent equality comparer
        /// </summary>
        public static IEqualityComparer<Skin> NameComparer { get; } = new NameEqualityComparer();
    }
}