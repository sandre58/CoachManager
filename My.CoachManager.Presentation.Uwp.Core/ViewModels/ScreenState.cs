namespace My.CoachManager.Presentation.Uwp.Core.ViewModels
{
    /// <summary>
    /// Enum des états possible d'un viewModel
    /// </summary>
    public enum ScreenState
    {
        /// <summary>
        /// Le view modèle est prêt
        /// </summary>
        NotLoaded = 0,

        /// <summary>
        /// Le view modèle est en cours de chargement
        /// </summary>
        Loading = 1,

        /// <summary>
        /// Le view modèle en cours de sauvegarde
        /// </summary>
        Saving = 2,

        /// <summary>
        /// Le view modèle est prêt
        /// </summary>
        Ready = 3
    }
}
