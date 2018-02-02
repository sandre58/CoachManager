﻿using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView
    {
        public PlayerView(IPlayerViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}