﻿using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class MultiCheckboxes : SelectorControl
    {
        #region Constructors

        static MultiCheckboxes()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiCheckboxes), new FrameworkPropertyMetadata(typeof(MultiCheckboxes)));
        }

        #endregion Constructors

    }
}