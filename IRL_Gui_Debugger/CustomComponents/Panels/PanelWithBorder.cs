using System.ComponentModel;

namespace IRL_Image_Creator.CustomComponents.Panels
{
    public partial class PanelWithBorder : Panel
    {
        [Browsable(true)]
        public Color BorderColor { get; set; }
        
        public PanelWithBorder()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            using (SolidBrush brush = new SolidBrush(BackColor))
                pe.Graphics.FillRectangle(brush, ClientRectangle);
            pe.Graphics.DrawRectangle(new Pen(BorderColor), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }
    }
}
