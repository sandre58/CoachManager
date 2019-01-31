using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Controls.Datagrids
{
    public struct DataGridColumnInfo
    {
        public DataGridColumnInfo(DataGridColumn column)
            : this()
        {
            DisplayIndex = column.DisplayIndex;
            Name = AutomationProperties.GetName(column);
            SortDirection = column.SortDirection;
            Visibility = column.Visibility;
            WidthType = column.Width.UnitType;
            WidthValue = column.Width.DisplayValue;
        }

        public int DisplayIndex
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ListSortDirection? SortDirection
        {
            get;
            set;
        }

        public Visibility Visibility
        {
            get;
            set;
        }

        public DataGridLengthUnitType WidthType
        {
            get;
            set;
        }

        public double WidthValue
        {
            get;
            set;
        }

        public void Apply(DataGridColumn column, int gridColumnCount)
        {
            if (column.DisplayIndex != DisplayIndex)
            {
                var maxIndex = (gridColumnCount == 0) ? 0 : gridColumnCount - 1;
                column.DisplayIndex = (DisplayIndex <= maxIndex) ? DisplayIndex : maxIndex;
            }

            column.SortDirection = SortDirection;
            column.Visibility = Visibility;
            column.Width = new DataGridLength(WidthValue, WidthType);
        }
    }
}