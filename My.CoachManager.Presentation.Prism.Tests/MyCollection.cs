using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Tests
{
    internal class MyCollection : ListCollectionView
    {
        /// <summary>
        /// Instance of the registered filters
        /// </summary>
        private Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();

        public MyCollection(IList list) : base(list)
        {
        }
    }
}