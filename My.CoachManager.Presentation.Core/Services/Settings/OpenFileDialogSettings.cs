namespace My.CoachManager.Presentation.Core.Services.Settings
{
    public class OpenFileDialogSettings
    {
        public bool MultiSelect { get; set; }

        public bool RestoreDirectory { get; set; }
        public string InitialDirectory { get; set; }
        public string Filter { get; set; }

        public static OpenFileDialogSettings Default
        {
            get
            {
                return new OpenFileDialogSettings()
                {
                    MultiSelect = false,
                    RestoreDirectory = true,
                    InitialDirectory = "",
                    Filter = ""
                };
            }
        }
    }
}