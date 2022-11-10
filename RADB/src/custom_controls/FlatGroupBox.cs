using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
//
using GNX;

namespace RADB
{
    public partial class FlatGroupBox : GroupBox
    {
        [Browsable(true)]
        [DefaultValue(typeof(Color), "0xA0A0A0")]
        public Color BorderColor { get; set; }

        [DefaultValue(1)]
        public int BorderSize { get; set; }

        [DefaultValue(true)]
        public bool BorderRound { get; set; }

        [Browsable(true)]
        [DefaultValue(typeof(Color), "0xF4F4F4")]
        public override Color BackColor { get; set; }

        public FlatGroupBox()
        {
            InitializeComponent();
            DoubleBuffered = true;

            BorderSize = 1;
            BorderColor = ColorTranslator.FromHtml("#A0A0A0");
            BorderRound = true;
            BackColor = ColorTranslator.FromHtml("#F4F4F4");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            SizeF strSize = e.Graphics.MeasureString(Text, Font);
            Rectangle RecLegend = new Rectangle(7, 6, (int)strSize.Width, 6);

            e.Graphics.DrawRoundBorder(this, BorderColor, BorderSize, BorderRound);

            ControlPaint.DrawBorder(e.Graphics, RecLegend, BackColor, ButtonBorderStyle.Solid);
            e.Graphics.DrawString(Text, Font, new Pen(ForeColor).Brush, 7, 0);
        }
    }
}
