﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.Prism.Wpf.Resources {
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
    public class MenuResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MenuResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("My.CoachManager.Presentation.Prism.Wpf.Resources.MenuResources", typeof(MenuResources).Assembly);
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
        ///   Looks up a localized string similar to Ajouter une équipe.
        /// </summary>
        public static string AddSquad {
            get {
                return ResourceManager.GetString("AddSquad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vous êtes sur le point de supprimer une équipe de l&apos;effectif. Vous perdrez tous les joueurs de cette équipe ainsi que les données qui leurs sont rattachées. Voulez-vous continuez ?.
        /// </summary>
        public static string ConfirmationRemoveSquadMessage {
            get {
                return ResourceManager.GetString("ConfirmationRemoveSquadMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Supprimer une équipe.
        /// </summary>
        public static string RemoveSquad {
            get {
                return ResourceManager.GetString("RemoveSquad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vous ne pouvez pas supprimer cette équipe. Il doit il y avoir au moins une équipe dans l&apos;effectif..
        /// </summary>
        public static string RemoveSquadCountMessage {
            get {
                return ResourceManager.GetString("RemoveSquadCountMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Saison {0}.
        /// </summary>
        public static string Season {
            get {
                return ResourceManager.GetString("Season", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Changer d&apos;effectif.
        /// </summary>
        public static string SetRoster {
            get {
                return ResourceManager.GetString("SetRoster", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} équipe(s).
        /// </summary>
        public static string SquadCount {
            get {
                return ResourceManager.GetString("SquadCount", resourceCulture);
            }
        }
    }
}