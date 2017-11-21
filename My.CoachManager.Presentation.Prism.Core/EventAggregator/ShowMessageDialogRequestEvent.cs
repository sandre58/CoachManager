﻿using My.CoachManager.Presentation.Prism.Core.Interactivity;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.EventAggregator
{
    public class ShowMessageDialogRequestEvent : PubSubEvent<DialogEventArgs>
    {
    }
}