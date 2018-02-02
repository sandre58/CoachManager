using System.Reflection;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.Prism.Modules.About.ViewModels
{
    public class AboutViewModel : DialogViewModel, IAboutViewModel
    {
        #region Fields

        private string _version;
        private string _product;
        private string _copyright;
        private string _description;
        private string _company;

        #endregion Fields

        #region Members

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            private set
            {
                SetProperty(ref _version, value);
            }
        }

        /// <summary>
        /// Get the assembly product.
        /// </summary>
        public string Product
        {
            get { return _product; }
            private set
            {
                SetProperty(ref _product, value);
            }
        }

        /// <summary>
        /// Get the assembly copyright.
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            private set
            {
                SetProperty(ref _copyright, value);
            }
        }

        /// <summary>
        /// Get the assembly description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            private set
            {
                SetProperty(ref _description, value);
            }
        }

        /// <summary>
        /// Get the assembly company.
        /// </summary>
        public string Company
        {
            get { return _company; }
            private set
            {
                SetProperty(ref _company, value);
            }
        }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="AboutViewModel"/>.
        /// </summary>
        public AboutViewModel()
        {
            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var productAttr = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            var descriptionAttr = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            var companyAttr = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            Version = assembly.GetName().Version.ToString();
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";
            Product = (productAttr != null) ? productAttr.Product : "";
            Description = (descriptionAttr != null) ? descriptionAttr.Description : "";
            Company = (companyAttr != null) ? companyAttr.Company : "";
        }

        #endregion Constructor
    }
}