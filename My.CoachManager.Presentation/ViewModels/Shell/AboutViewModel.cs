using System.Reflection;
using My.CoachManager.Presentation.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.ViewModels.Shell
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class AboutViewModel : DialogViewModel
    {
        #region Fields

        private string _version;
        private string _product;
        private string _copyright;
        private string _description;
        private string _company;

        #endregion Fields

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

        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.ShellResources.ApplicationTitle;
            }
        }

        /// <summary>
        /// Get the assembly version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            private set
            {
                _version = value;
                NotifyOfPropertyChange();
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
                _product = value;
                NotifyOfPropertyChange();
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
                _copyright = value;
                NotifyOfPropertyChange();
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
                _description = value;
                NotifyOfPropertyChange();
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
                _company = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Close window.
        /// </summary>
        public void Close()
        {
            TryClose();
        }

        #endregion Methods
    }
}