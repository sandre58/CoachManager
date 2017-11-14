using My.CoachManager.Presentation.Core.Services.Enums;
using System;

namespace My.CoachManager.Presentation.Core.Services.Settings
{
    public class PopupSettings
    {
        public TimeSpan Delay { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public PopupDirection Position { get; set; }

        public static PopupSettings Default
        {
            get
            {
                return new PopupSettings()
                {
                    Delay = TimeSpan.FromSeconds(3),
                    Width = 600,
                    Height = 45,
                    Position = PopupDirection.TopCenter
                };
            }
        }
    }
}