﻿using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class NavigationCompletedEventArgs : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets or set the workspace.
        /// </summary>
        public INavigatableWorkspaceViewModel Workspace { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public NavigationContext Context { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NavigationCompletedEventArgs"/>.
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="context"></param>
        public NavigationCompletedEventArgs(INavigatableWorkspaceViewModel workspace, NavigationContext context)
        {
            Workspace = workspace;
            Context = context;
        }

        #endregion Constructors
    }
}