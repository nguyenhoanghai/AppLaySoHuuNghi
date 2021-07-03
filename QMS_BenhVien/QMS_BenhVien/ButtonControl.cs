using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    [DefaultEvent("Click")]
    public partial class ButtonControl : UserControl
    {
        int radius = 20;
        Color backgroundColor = Color.Transparent,
            borderColor = Color.Silver;
        string _text = "Custom Button";
        int borderThickness = 5;
        public ButtonControl()
        {
        }

        public int BorderRadius
        {
            get { return radius; }
            set
            {
                radius = value;
                //call button update state
                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                this.Invalidate();
            }
        }


        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        public string ButtonText
        {
            get { return _text; }
            set { _text = value; this.Invalidate(); }
        }

        public int BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; this.Invalidate(); }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        //    GraphicsPath gp = new GraphicsPath(); 
        //    gp.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);         
        //    gp.AddArc(new Rectangle(Width - radius, 0, radius, radius), -90, 90);
        //    gp.AddArc(new Rectangle(Width - radius, Height - radius, radius, radius), 0, 90);
        //    gp.AddArc(new Rectangle(0, Height - radius, radius, radius), 90, 90);

        //    e.Graphics.FillPath(new SolidBrush(backgroundColor), gp);
        //    e.Graphics.DrawString(_text, Font, new SolidBrush(ForeColor), ClientRectangle, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });


        //    base.OnPaint(e);
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }


        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float m = 2.75F;
            float r2 = radius / 2f;
            GraphicsPath gp = new GraphicsPath();

            //top - left
            gp.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            //top - right
            gp.AddArc(new Rectangle(Width - radius, 0, radius, radius), -90, 90);
            //bottom - right
            gp.AddArc(new Rectangle(Width - radius, Height - radius, radius, radius), 0, 90);
            //bottom - left
            gp.AddArc(new Rectangle(0, Height - radius, radius, radius), 90, 90);
            gp.CloseFigure();
            return gp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, BorderRadius);

            this.Region = new Region(GraphPath);


            int _radius = 1;// borderThickness <=5 ?borderThickness:1;

            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(new Rectangle(BorderThickness, BorderThickness, 1, 1), 180, 90);
            gp.AddArc(new Rectangle((Width - _radius) - BorderThickness, BorderThickness, 1, 1), -90, 90);


            gp.AddArc(new Rectangle((Width - _radius) - BorderThickness, (Height - BorderThickness - _radius), 1, 1), 0, 90);
            gp.AddArc(new Rectangle(BorderThickness, (Height - BorderThickness - _radius), 1, 1), 90, 90);

            if (this.Enabled)
            {
                e.Graphics.FillPath(new SolidBrush(this.backgroundColor), gp);
                using (Pen pen = new Pen(this.borderColor, BorderThickness))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
                e.Graphics.DrawString(ButtonText, Font, new SolidBrush(this.ForeColor), ClientRectangle, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                this.Cursor = Cursors.Hand;
            }
            else
            {
                e.Graphics.FillPath(new SolidBrush(Color.LightGray), gp);
                using (Pen pen = new Pen(this.borderColor, BorderThickness))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
                e.Graphics.DrawString(ButtonText, Font, new SolidBrush(Color.Silver), ClientRectangle, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                this.Cursor = Cursors.No;
            }
        }
    }
}
