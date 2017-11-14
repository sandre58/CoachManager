namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Enums
{
    /// <summary>
    /// Enum des états possible d'un viewModel
    /// </summary>
    public enum ViewModelState
    {
        /// <summary>
        /// Le view modèle est prêt
        /// </summary>
        NotInitilized,

        /// <summary>
        /// Le view modèle est en cours de chargement
        /// </summary>
        Loading,

        /// <summary>
        /// Le view modèle en cours de sauvegarde
        /// </summary>
        Saving,

        /// <summary>
        /// Le view modèle est prêt
        /// </summary>
        Ready
    }
}