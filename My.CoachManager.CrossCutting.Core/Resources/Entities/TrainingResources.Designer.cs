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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class TrainingResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TrainingResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("My.CoachManager.CrossCutting.Core.Resources.Entities.TrainingResources", typeof(TrainingResources).Assembly);
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
        ///   Looks up a localized string similar to Date.
        /// </summary>
        public static string Date {
            get {
                return ResourceManager.GetString("Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fin.
        /// </summary>
        public static string EndDate {
            get {
                return ResourceManager.GetString("EndDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Heure de Fin.
        /// </summary>
        public static string EndTime {
            get {
                return ResourceManager.GetString("EndTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Est annulé.
        /// </summary>
        public static string IsCancelled {
            get {
                return ResourceManager.GetString("IsCancelled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lieu.
        /// </summary>
        public static string Place {
            get {
                return ResourceManager.GetString("Place", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Effectif.
        /// </summary>
        public static string Roster {
            get {
                return ResourceManager.GetString("Roster", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Début.
        /// </summary>
        public static string StartDate {
            get {
                return ResourceManager.GetString("StartDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Heure de début.
        /// </summary>
        public static string StartTime {
            get {
                return ResourceManager.GetString("StartTime", resourceCulture);
            }
        }
    }
}
