﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Resources {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SquadResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SquadResources() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("My.CoachManager.Presentation.Prism.Modules.Roster.Resources.SquadResources", typeof(SquadResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
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
        ///   Recherche une chaîne localisée semblable à Vous êtes sur le point de supprimer ce joueur de l&apos;effectif. Vous perdrez toutes les données et statistiques qui lui sont rattachées. Voulez-vous continuez ?.
        /// </summary>
        public static string ConfirmationRemovingItemMessage {
            get {
                return ResourceManager.GetString("ConfirmationRemovingItemMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Vous êtes sur le point de supprimer les joueurs sélectionnés de l&apos;effectif. Vous perdrez toutes les données et statistiques qui leurs sont rattachées. Voulez-vous continuez ?.
        /// </summary>
        public static string ConfirmationRemovingItemsMessage {
            get {
                return ResourceManager.GetString("ConfirmationRemovingItemsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Editer l&apos;équipe.
        /// </summary>
        public static string EditSquad {
            get {
                return ResourceManager.GetString("EditSquad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Des Joueurs Existants.
        /// </summary>
        public static string ExistingPlayers {
            get {
                return ResourceManager.GetString("ExistingPlayers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Un Nouveau Joueur.
        /// </summary>
        public static string NewPlayer {
            get {
                return ResourceManager.GetString("NewPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Nouvelle équipe.
        /// </summary>
        public static string NewSquad {
            get {
                return ResourceManager.GetString("NewSquad", resourceCulture);
            }
        }
    }
}
