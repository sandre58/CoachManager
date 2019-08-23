using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace My.CoachManager.Presentation.Wpf.SkinManager
{
    /// <summary>
    /// Theme manager for an WPF application.
    /// </summary>
    public static class SkinManager
    {
        #region Fields

        private static readonly IDictionary<DispatcherObject, Theme> CurrentThemes = new Dictionary<DispatcherObject, Theme>();
        private static readonly IDictionary<DispatcherObject, Accent> CurrentAccents = new Dictionary<DispatcherObject, Accent>();
        private static readonly IDictionary<DispatcherObject, Accent> CurrentSecondaryAccents = new Dictionary<DispatcherObject, Accent>();

        #endregion Fields

        #region Members

        /// <summary>
        /// The available themes to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Theme> Themes { get; } = RetrieveList<Theme>();

        /// <summary>
        /// The available accents to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Accent> Accents { get; } = RetrieveList<Accent>();

        /// <summary>
        /// The available accents to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Accent> SecondaryAccents { get; } = RetrieveList<Accent>("SecondaryAccent");

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public static Theme CurrentTheme
        {
            get
            {
                if (CurrentThemes.TryGetValue(Application.Current, out var current))
                {
                    return current;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public static Accent CurrentAccent
        {
            get
            {
                if (CurrentAccents.TryGetValue(Application.Current, out var current))
                {
                    return current;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public static Accent CurrentSecondaryAccent
        {
            get
            {
                if (CurrentSecondaryAccents.TryGetValue(Application.Current, out var current))
                {
                    return current;
                }
                return null;
            }
        }

        #endregion Members
        
        /// <summary>
        /// Retrieves the list of themes from the assembly.
        /// </summary>
        /// <returns>The list of theme names.</returns>
        private static List<T> RetrieveList<T>(string nameFolder  = "") where T : Skin
        {
            var list = new List<T>();

            // Get the assembly that we are currently in.
            Assembly assembly = Assembly.GetEntryAssembly();

            // Get the name of the resources file that we will load.
            if (assembly != null)
            {
                var resourceFileName = assembly.GetName().Name + ".g.resources";

                // Open the manifest stream.
                using (Stream stream = assembly.GetManifestResourceStream(resourceFileName))
                {
                    var typeName = string.IsNullOrEmpty(nameFolder) ? typeof(T).Name : nameFolder;
                    // Open a resource reader to get all the resource files.
                    if (stream == null) return list;
                    using (var reader = new ResourceReader(stream))
                    {
                        // Returns just the resources that start in the themes folder
                        list.AddRange(reader.Cast<DictionaryEntry>()
                            .Where(entry =>
                                entry.Key.ToString()
                                    .StartsWith("skins/" + typeName.ToLower() + "/")) // Get all files that are in the themes folder.
                            .Select(entry => GetSkin<T>(assembly, entry.Key.ToString(), typeName))
                            .OrderBy(x => x.Label)); // put it in alpha order.
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        private static T GetSkin<T>(Assembly assembly, string resourceName, string nameFolder = "") where T : Skin
        {
            var name = resourceName.Replace(".baml", "").Replace("skins/" + nameFolder.ToLower() + "/", "");
            var uri = new Uri(string.Format(@"{0};component/Skins/" + nameFolder + "/{1}.xaml", assembly.GetName().Name, name), UriKind.Relative);

            return (T)Activator.CreateInstance(typeof(T), name, uri);
        }

        /// <summary>
        /// Add a theme.
        /// </summary>
        /// <param name="theme"></param>
        public static void AddTheme(Theme theme)
        {
            if (Themes != null && Themes.All(x => x != theme))
            {
                Themes.Add(theme);
            }
        }

        /// <summary>
        /// Add a accent.
        /// </summary>
        /// <param name="accent"></param>
        public static void AddAccent(Accent accent)
        {
            if (Accents != null && Accents.All(x => x != accent))
            {
                Accents.Add(accent);
            }
        }

        /// <summary>
        /// Add a accent.
        /// </summary>
        /// <param name="accent"></param>
        public static void AddSecondaryAccent(Accent accent)
        {
            if (SecondaryAccents != null && SecondaryAccents.All(x => x != accent))
            {
                SecondaryAccents.Add(accent);
            }
        }

        /// <summary>
        /// Applies a theme to the application.
        /// </summary>
        /// <param name="theme">The theme to be applied.</param>
        public static void ApplyTheme(Theme theme)
        {
            var localThemes = Themes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(Application.Current, Application.Current.Resources, CurrentThemes, theme);
        }

        /// <summary>
        /// Applies a theme to a content control
        /// </summary>
        /// <param name="control">The control the theme will be applied to.</param>
        /// <param name="theme">The theme to be applied</param>
        public static void ApplyTheme(ContentControl control, Theme theme)
        {
            var localThemes = Themes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(control, control.Resources, CurrentThemes, theme);
        }

        /// <summary>
        /// Applies a accent to the application.
        /// </summary>
        /// <param name="themeName">The theme to be applied.</param>
        public static void ApplyTheme(string themeName)
        {
            if (Themes != null)
            {
                var enumerable = Themes ?? Themes.ToList();
                var theme = enumerable.FirstOrDefault(x => string.Equals(x.Name, themeName, StringComparison.CurrentCultureIgnoreCase));

                if (theme != null) ApplyTheme(theme);
            }
        }

        /// <summary>
        /// Applies a accent to the application.
        /// </summary>
        /// <param name="accent">The theme to be applied.</param>
        public static void ApplyAccent(Accent accent)
        {
            var localThemes = Accents ?? Enumerable.Empty<Accent>();
            if (localThemes.All(x => x != accent))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(Application.Current, Application.Current.Resources, CurrentAccents, accent);
        }

        /// <summary>
        /// Applies a accent to the application.
        /// </summary>
        /// <param name="accentName">The theme to be applied.</param>
        public static void ApplyAccent(string accentName)
        {
            if (Accents != null)
            {
                var enumerable = Accents ?? Accents.ToList();
                var accent = enumerable.FirstOrDefault(x => string.Equals(x.Name, accentName, StringComparison.CurrentCultureIgnoreCase));

                if (accent != null) ApplyAccent(accent);
            }
        }

        /// <summary>
        /// Applies a accent to a content control
        /// </summary>
        /// <param name="control">The control the theme will be applied to.</param>
        /// <param name="accent">The theme to be applied</param>
        public static void ApplyAccent(ContentControl control, Accent accent)
        {
            var localThemes = Accents ?? Enumerable.Empty<Accent>();
            if (localThemes.All(x => x != accent))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(control, control.Resources, CurrentAccents, accent);
        }

        /// <summary>
        /// Applies a accent to the application.
        /// </summary>
        /// <param name="accent">The theme to be applied.</param>
        public static void ApplySecondaryAccent(Accent accent)
        {
            var localThemes = SecondaryAccents ?? Enumerable.Empty<Accent>();
            if (localThemes.All(x => x != accent))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(Application.Current, Application.Current.Resources, CurrentSecondaryAccents, accent);
        }

        /// <summary>
        /// Applies a accent to the application.
        /// </summary>
        /// <param name="accentName">The theme to be applied.</param>
        public static void ApplySecondaryAccent(string accentName)
        {
            if (SecondaryAccents != null)
            {
                var enumerable = SecondaryAccents ?? SecondaryAccents.ToList();
                var accent = enumerable.FirstOrDefault(x => string.Equals(x.Name, accentName, StringComparison.CurrentCultureIgnoreCase));

                if (accent != null)
                    ApplySecondaryAccent(accent);
            }
        }

        /// <summary>
        /// Applies a accent to a content control
        /// </summary>
        /// <param name="control">The control the theme will be applied to.</param>
        /// <param name="accent">The theme to be applied</param>
        public static void ApplySecondaryAccent(ContentControl control, Accent accent)
        {
            var localThemes = SecondaryAccents ?? Enumerable.Empty<Accent>();
            if (localThemes.All(x => x != accent))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(control, control.Resources, CurrentSecondaryAccents, accent);
        }

        private static void ApplySkin<T>(DispatcherObject @object, ResourceDictionary resources, IDictionary<DispatcherObject, T> currents, T skin) where T : Skin
        {
            if (currents.TryGetValue(@object, out var current))
            {
                resources.MergedDictionaries.Remove(current.Resources);
            }

            if (skin != null && skin.Resources != null)
            {
                var resourceDictionary = skin.Resources;
                if (resourceDictionary != null)
                {
                    resources.MergedDictionaries.Add(resourceDictionary);
                }
            }

            currents[@object] = skin;
        }
    }
}