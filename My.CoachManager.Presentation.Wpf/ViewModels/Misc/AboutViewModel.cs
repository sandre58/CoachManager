using System.Reflection;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Resources.Strings;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Misc
{
    public class AboutViewModel : DialogViewModel
    {
        #region Members

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Get the assembly product.
        /// </summary>
        public string Product { get; private set; }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright { get; private set; }

        /// <summary>
        /// Get the assembly description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Get the assembly company.
        /// </summary>
        public string Company { get; private set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly?.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var productAttr = assembly?.GetCustomAttribute<AssemblyProductAttribute>();
            var descriptionAttr = assembly?.GetCustomAttribute<AssemblyDescriptionAttribute>();
            var companyAttr = assembly?.GetCustomAttribute<AssemblyCompanyAttribute>();

            Version = assembly?.GetName().Version.ToString();
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : string.Empty;
            Product = (productAttr != null) ? productAttr.Product : string.Empty;
            Description = (descriptionAttr != null) ? descriptionAttr.Description : string.Empty;
            Company = (companyAttr != null) ? companyAttr.Company : string.Empty;
            Title = CommonResources.About;
        }

        #endregion Initialisation

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load data.
        /// </summary>
        protected override void LoadDataCore()
        {
        }

        #endregion Data
    }
}