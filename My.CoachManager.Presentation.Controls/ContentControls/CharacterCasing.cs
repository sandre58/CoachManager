namespace My.CoachManager.Presentation.Controls.ContentControls
{
    // Résumé :
    //     Spécifie la casse de caractères tapé manuellement dans un contrôle System.Windows.Controls.TextBox.
    public enum CharacterCase
    {
        // Résumé :
        //     Les caractères tapés dans un System.Windows.Controls.TextBox ne sont pas
        //     convertis.
        Normal = 0,

        //
        // Résumé :
        //     Les caractères tapés dans un System.Windows.Controls.TextBox sont convertis
        //     en minuscules.
        Lower = 1,

        //
        // Résumé :
        //     Les caractères tapés dans un System.Windows.Controls.TextBox sont convertis
        //     en majuscules.
        Upper = 2,

        //
        // Résumé :
        //     Les 1er caractères tapés dans un System.Windows.Controls.TextBox sont convertis
        //     en majuscules.
        FirstLetterUpper = 3,
    }
}