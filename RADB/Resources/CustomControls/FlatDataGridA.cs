using System.Drawing;

namespace RADB
{
    public partial class FlatDataGridA : GNX.Desktop.FlatDataGrid
    {
        public FlatDataGridA()
        {
            InitializeComponent();

            ColorBackground = ColorTranslator.FromHtml("#F4F4F4");
        }

        public void DarkMode()
        {
            ColorFontRow = ColorTranslator.FromHtml("#D2D2D2");
            ColorFontRowSelection = ColorFontRow;

            ColorRow = ColorTranslator.FromHtml("#212121");
            ColorRowAlternate = ColorTranslator.FromHtml("#242424");

            ColorGrid = ColorRow;
            ColorBackground = ColorRow;

            ColorRowSelection = ColorTranslator.FromHtml("#0F0F0F");
            ColorRowMouseHover = ColorTranslator.FromHtml("#353535");

            ColorColumnHeader = ColorTranslator.FromHtml("#3F3F3F");
            ColorColumnSelection = ColorColumnHeader;
        }
    }
}