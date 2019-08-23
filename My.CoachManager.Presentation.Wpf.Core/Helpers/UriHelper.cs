﻿using System;
using My.CoachManager.Presentation.Wpf.Core.Navigation;

namespace My.CoachManager.Presentation.Wpf.Core.Helpers
{
    /// <summary>
    /// Helper class for parsing <see cref="Uri"/> instances.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Gets the query part of <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The Uri.</param>
        public static string GetQuery(Uri uri)
        {
            return EnsureAbsolute(uri).Query;
        }

        /// <summary>
        /// Gets the AbsolutePath part of <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The Uri.</param>
        public static string GetAbsolutePath(Uri uri)
        {
            return EnsureAbsolute(uri).AbsolutePath;
        }

        /// <summary>
        /// Parses the query of <paramref name="uri"/> into a dictionary.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public static NavigationParameters ParseQuery(Uri uri)
        {
            var query = GetQuery(uri);

            return new NavigationParameters(query);
        }

        private static Uri EnsureAbsolute(Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return uri;
            }

            return !uri.OriginalString.StartsWith("/", StringComparison.Ordinal) ? new Uri("http://localhost/" + uri, UriKind.Absolute) : new Uri("http://localhost" + uri, UriKind.Absolute);
        }
    }
}
