using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RADB
{
    public class FlatArticle : FlowLayoutPanel
    {
        readonly Label lblTitle = new Label();
        readonly Label lblDesc = new Label();

        [DefaultValue(typeof(string), "LabelTitle")]
        public string TextTitle
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        [DefaultValue(typeof(string), "LabelDesc")]
        public string TextDesc
        {
            get { return lblDesc.Text; }
            set { lblDesc.Text = value; }
        }

        [DefaultValue(typeof(Font), "Segoe UI, 9pt, style=Bold")]
        public new Font Font
        {
            get { return lblTitle.Font; }
            set { lblTitle.Font = value; }
        }

        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public Font Font2
        {
            get { return lblDesc.Font; }
            set { lblDesc.Font = value; }
        }

        [DefaultValue(typeof(Padding), "1, 1, 1, 1")]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [DefaultValue(typeof(Size), "0, 0")]
        public new Size MaximumSize
        {
            get { return base.MaximumSize; }
            set
            {
                base.MaximumSize = value;

                //var nSize = new Size(MaximumSize.Width - (Padding.Left + Padding.Right), 0);
                //lblTitle.MaximumSize = nSize;
                //lblTitle.MaximumSize = nSize;
            }
        }

        [DefaultValue(true)]
        public new bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        [DefaultValue(typeof(AutoSizeMode), "GrowAndShrink")]
        public new AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set { base.AutoSizeMode = value; }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        public new Color BackColor
        {
            get { return lblTitle.BackColor; }
            set
            {
                lblTitle.BackColor = value;
                lblDesc.BackColor = value;
            }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        public Color BackColor2
        {
            get { return lblDesc.BackColor; }
            set { lblDesc.BackColor = value; }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [DefaultValue(typeof(Color), "ControlText")]
        public new Color ForeColor
        {
            get { return lblTitle.ForeColor; }
            set { lblTitle.ForeColor = value; }
        }

        [DefaultValue(typeof(Color), "ControlText")]
        public Color ForeColor2
        {
            get { return lblDesc.ForeColor; }
            set { lblDesc.ForeColor = value; }
        }

        [DefaultValue(typeof(FlowDirection), "TopDown")]
        public new FlowDirection FlowDirection
        {
            get { return base.FlowDirection; }
            set { base.FlowDirection = value; }
        }

        [DefaultValue(false)]
        public new bool WrapContents
        {
            get { return base.WrapContents; }
            set { base.WrapContents = value; }
        }

        public FlatArticle()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;

            Padding = new Padding(1);

            lblTitle.AutoSize = true;
            lblTitle.Text = "Title";
            lblTitle.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.UseMnemonic = false;
            lblTitle.Margin = new Padding(0);
            lblTitle.Padding = new Padding(1, 0, 0, 0);

            lblDesc.AutoSize = true;
            lblDesc.Text = "Desc";
            lblDesc.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point);
            lblDesc.TextAlign = ContentAlignment.MiddleLeft;
            lblDesc.UseMnemonic = false;
            lblDesc.Margin = new Padding(0);
            lblTitle.Padding = new Padding(0);

            BackColor = Color.Transparent;
            BorderColor = Color.Transparent;

            Controls.Add(lblTitle);
            Controls.Add(lblDesc);
        }
    }
}