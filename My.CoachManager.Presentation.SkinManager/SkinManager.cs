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

namespace My.CoachManager.Presentation.SkinManager
{
    /// <summary>
    /// Theme manager for an WPF application.
    /// </summary>
    public static class SkinManager
    {
        #region Fields

        private static readonly IDictionary<DispatcherObject, Theme> CurrentThemes;
        private static readonly IDictionary<DispatcherObject, Accent> CurrentAccents;
        private static readonly IDictionary<DispatcherObject, Menu> CurrentMenus;

        #endregion Fields

        #region Members

        /// <summary>
        /// The available themes to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Theme> Themes { get; set; }

        /// <summary>
        /// The available accents to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Accent> Accents { get; set; }

        /// <summary>
        /// The available menu to be applied to a content control or the whole application.
        /// </summary>
        public static IList<Menu> Menus { get; set; }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public static Theme CurrentTheme
        {
            get
            {
                Theme current;
                if (CurrentThemes.TryGetValue(Application.Current, out current))
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
                Accent current;
                if (CurrentAccents.TryGetValue(Application.Current, out current))
                {
                    return current;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public static Menu CurrentMenu
        {
            get
            {
                if (CurrentMenus.TryGetValue(Application.Current, out Menu current))
                {
                    return current;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets resource manager.
        /// </summary>
        public static ResourceManager ResourceManager { get; set; }

        #endregion Members

        #region Constructors

        static SkinManager()
        {
            // Get the assembly that we are currently in.
            Assembly assembly = Assembly.GetEntryAssembly();
            ResourceManager = new ResourceManager(assembly.GetName().Name + ".Resources.SkinResources", assembly);

            CurrentThemes = new Dictionary<DispatcherObject, Theme>();
            CurrentAccents = new Dictionary<DispatcherObject, Accent>();
            CurrentMenus = new Dictionary<DispatcherObject, Menu>();

            // Get the list of theme names from the method above.
            Themes = RetrieveList<Theme>();
            Accents = RetrieveList<Accent>();
            Menus = RetrieveList<Menu>();
        }

        #endregion Constructors

        /// <summary>
        /// Retrieves the list of themes from the assembly.
        /// </summary>
        /// <returns>The list of theme names.</returns>
        private static List<T> RetrieveList<T>() where T : Skin
        {
            var list = new List<T>();

            // Get the assembly that we are currently in.
            Assembly assembly = Assembly.GetEntryAssembly();

            // Get the name of the resources file that we will load.
            string resourceFileName = assembly.GetName().Name + ".g.resources";

            // Open the manifest stream.
            using (Stream stream = assembly.GetManifestResourceStream(resourceFileName))
            {
                var typeName = typeof(T).Name.ToLower();
                // Open a resource reader to get all the resource files.
                if (stream != null)
                    using (var reader = new ResourceReader(stream))
                    {
                        // Returns just the resources that start in the themes folder
                        list.AddRange(reader.Cast<DictionaryEntry>()
                            .Where(entry =>
                                entry.Key.ToString()
                                    .StartsWith("skins/" + typeName + "/")) // Get all files that are in the themes folder.
                            .Select(entry => GetSkin<T>(assembly, entry.Key.ToString()))
                            .OrderBy(x => x.Label)); // put it in alpha order.
                    }
            }

            return list;
        }

        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static T GetSkin<T>(Assembly assembly, string resourceName) where T : Skin
        {
            var name = resourceName.Replace(".baml", "").Replace("skins/" + typeof(T).Name.ToLower() + "/", "");
            var uri = new Uri(string.Format(@"{0};component/Skins/" + typeof(T).Name + "/{1}.xaml", assembly.GetName().Name, name), UriKind.Relative);

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
        /// <param name="menu"></param>
        public static void AddMenu(Menu menu)
        {
            if (Menus != null && Menus.All(x => x != menu))
            {
                Menus.Add(menu);
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
        /// Applies a menu to the application.
        /// </summary>
        /// <param name="menu">The theme to be applied.</param>
        public static void ApplyMenu(Menu menu)
        {
            var localThemes = Menus ?? Enumerable.Empty<Menu>();
            if (localThemes.All(x => x != menu))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(Application.Current, Application.Current.Resources, CurrentMenus, menu);
        }

        /// <summary>
        /// Applies a menu to the application.
        /// </summary>
        /// <param name="menuName">The theme to be applied.</param>
        public static void ApplyMenu(string menuName)
        {
            if (Menus != null)
            {
                var enumerable = Menus ?? Menus.ToList();
                var menu = enumerable.FirstOrDefault(x => string.Equals(x.Name, menuName, StringComparison.CurrentCultureIgnoreCase));

                if (menu != null) ApplyMenu(menu);
            }
        }

        /// <summary>
        /// Applies a menu to a content control
        /// </summary>
        /// <param name="control">The control the theme will be applied to.</param>
        /// <param name="menu">The theme to be applied</param>
        public static void ApplyMenu(ContentControl control, Menu menu)
        {
            var localThemes = Menus ?? Enumerable.Empty<Menu>();
            if (localThemes.All(x => x != menu))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplySkin(control, control.Resources, CurrentMenus, menu);
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