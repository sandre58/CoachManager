﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.CrossCutting.Core.Resources.Entities {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RosterResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RosterResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("My.CoachManager.CrossCutting.Core.Resources.Entities.RosterResources", typeof(RosterResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Catégorie.
        /// </summary>
        public static string Category {
            get {
                return ResourceManager.GetString("Category", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Staff.
        /// </summary>
        public static string Coachs {
            get {
                return ResourceManager.GetString("Coachs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nom.
        /// </summary>
        public static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Joueurs.
        /// </summary>
        public static string Players {
            get {
                return ResourceManager.GetString("Players", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Saison.
        /// </summary>
        public static string Season {
            get {
                return ResourceManager.GetString("Season", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Equipes.
        /// </summary>
        public static string Squads {
            get {
                return ResourceManager.GetString("Squads", resourceCulture);
            }
        }
    }
}