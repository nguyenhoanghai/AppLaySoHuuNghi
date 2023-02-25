using System;
using System.Drawing;
using System.Windows.Forms;

namespace QMS_BenhVien.CustomControls
{
    class CustomCheckbox : Control
    {
        #region checkbox props
        private Label chklabel;
        Rectangle chkRectangle;

        /// <summary>
        /// Check if mouse is over the control
        /// </summary>
        bool MouseOver = false; 

        #region FrameStrength 
        private int _CheckboxFrameStrength = 1;
        public int CheckboxFrameStrength
        {
            get { return _CheckboxFrameStrength; }
            set
            {
                if (CheckboxFrameStrength != value)
                {
                    _CheckboxFrameStrength = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region FrameColor 
        private Color _CheckboxFrameColor = Color.FromArgb(0, 0, 0);
        public Color CheckboxFrameColor
        {
            get { return _CheckboxFrameColor; }
            set
            {
                if (CheckboxFrameColor != value)
                {
                    _CheckboxFrameColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region FrameHightLightColor 
        private Color _CheckboxFrameHightLightColor = Color.FromArgb(255, 0, 0);
        public Color CheckboxFrameHightLightColor
        {
            get { return _CheckboxFrameHightLightColor; }
            set
            {
                if (CheckboxFrameHightLightColor != value)
                {
                    _CheckboxFrameHightLightColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region BackColor 
        private Color _CheckboxBackColor = Color.FromArgb(255, 255, 255);
        public Color CheckboxBackColor
        {
            get { return _CheckboxBackColor; }
            set
            {
                if (CheckboxBackColor != value)
                {
                    _CheckboxBackColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Size 
        private int _CheckboxSize = 10;
        public int CheckboxSize
        {
            get { return _CheckboxSize; }
            set
            {
                if (CheckboxSize != value)
                {
                    _CheckboxSize = value;
                    RefreshLabel();
                    Invalidate();
                }
            }
        }
        #endregion

        #region Label 
        private Font _LabelFont;
        public Font LabelFont
        {
            get { return _LabelFont; }
            set
            {
                if (LabelFont != value)
                {
                    _LabelFont = value;
                    RefreshLabel();
                }
            }
        }
         
        private Color _LabelForeColor = Color.FromArgb(0, 0, 0);
        public Color LabelForeColor
        {
            get { return _LabelForeColor; }
            set
            {
                if (LabelForeColor != value)
                {
                    _LabelForeColor = value;
                    RefreshLabel();
                }
            }
        }
         
        private int _LabelOffsetX = 0;
        public int LabelOffsetX
        {
            get { return _LabelOffsetX; }
            set
            {
                if (LabelOffsetX != value)
                {
                    _LabelOffsetX = value;
                    RefreshLabel();
                }
            }
        }
         
        private int _LabelOffsetY = 0;
        public int LabelOffsetY
        {
            get { return _LabelOffsetY; }
            set
            {
                if (LabelOffsetY != value)
                {
                    _LabelOffsetY = value;
                    RefreshLabel();
                }
            }
        }
        
        private string _LabelText = "My Checkbox";
        public string LabelText
        {
            get { return _LabelText; }
            set
            {
                if (LabelText != value)
                {
                    _LabelText = value;
                    RefreshLabel();
                }
            }
        }
        #endregion

        #region CheckboxChar 
        private string _CheckboxChar =   "\u2713";
        public string CheckboxChar
        {
            get { return _CheckboxChar; }
            set
            {
                if (CheckboxChar != value)
                {
                    _CheckboxChar = value;
                    Invalidate();
                }
            }
        }
      
        private Font _CheckboxCharFont;
        public Font CheckboxCharFont
        {
            get { return _CheckboxCharFont; }
            set
            {
                if (CheckboxCharFont != value)
                {
                    _CheckboxCharFont = value;
                    Invalidate();
                }
            }
        }
             
        private Color _CheckboxCharForeColor = Color.FromArgb(0, 0, 0);
        public Color CheckboxCharForeColor
        {
            get { return _CheckboxCharForeColor; }
            set
            {
                if (CheckboxCharForeColor != value)
                {
                    _CheckboxCharForeColor = value;
                    Invalidate();
                }
            }
        }
  
        private int _CheckboxCharOffsetX = 0;
        public int CheckboxCharOffsetX
        {
            get { return _CheckboxCharOffsetX; }
            set
            {
                if (CheckboxCharOffsetX != value)
                {
                    _CheckboxCharOffsetX = value;
                    Invalidate();
                }
            }
        }
   
        private int _CheckboxCharOffsetY = 0;
        public int CheckboxCharOffsetY
        {
            get { return _CheckboxCharOffsetY; }
            set
            {
                if (CheckboxCharOffsetY != value)
                {
                    _CheckboxCharOffsetY = value;
                    Invalidate();
                }
            }
        }

        #endregion

        private bool _Checked = false;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                if (Checked != value)
                {
                    _Checked = value;  
                    Invalidate();
                }
            }
        }
        #endregion

        #region constructor
        public CustomCheckbox()
        {
            DoubleBuffered = true;

            //Default size
            Size = new Size(112, 17);

            LabelFont = this.Font;
            CheckboxCharFont = this.Font;

            var midHeight = 2 - (Height / 2 - Font.Height / 2);
            var offsetX = CheckboxSize + 2;
            chklabel = new Label()
            {
                Size = new Size(this.Width, this.Height),
                Location = new Point(offsetX, midHeight),
                Text = _LabelText,
                Font = LabelFont
            };
            Controls.Add(chklabel);

            InitEvents();
        }
        #endregion

        #region Events 
        private void InitEvents()
        {
            MouseEnter += (sender, e) => { OnEnter(e); };
            MouseLeave += (sender, e) => { OnLeave(e); };
            MouseDown += (sender, e) => { _OnMouseDown(e); };

            chklabel.MouseEnter += (sender, e) => { OnEnter(e); };
            chklabel.MouseLeave += (sender, e) => { OnLeave(e); };
            chklabel.MouseDown += (sender, e) => { _OnMouseDown(e); }; 

        }         

        private void OnEnter(EventArgs e)
        {
            if (!MouseOver)
            {
                //switch
                MouseOver = true;
                //repaint
                Invalidate();
                //fore refresh
                RefreshLabel();
            }
        }
        private void OnLeave(EventArgs e)
        {
            if (MouseOver)
            {
                MouseOver = false;
                Invalidate();
                RefreshLabel();
            }
        }

        private void _OnMouseDown(EventArgs e)
        {
            Checked = !Checked;
            Invalidate();
        }
        #endregion

        #region Paint 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
            //paint checkbox rectangle
            PaintRectangle(e);
          
            if (Checked)
            {
                //paint arrow char
                PaintArrow(e);
            }
            RefreshLabel();
        }

        void PaintRectangle(PaintEventArgs e)
        {
            var midHeight = Height / 2 - CheckboxSize / 2;
            chkRectangle = new Rectangle(0, midHeight, CheckboxSize, CheckboxSize);

            var fillColor = CheckboxBackColor;// MouseOver ? CheckboxFrameHightLightColor : CheckboxBackColor;
            var frameColor = MouseOver ? CheckboxFrameHightLightColor : _CheckboxFrameColor;

            using (var pen = new Pen(frameColor, _CheckboxFrameStrength))
            {
                var brush = new SolidBrush(fillColor);
                e.Graphics.FillRectangle(brush, chkRectangle);
                e.Graphics.DrawRectangle(pen, chkRectangle);
            }
        }

        private void PaintArrow(PaintEventArgs e)
        {
            var charColor = CheckboxCharForeColor;
            var _x = CheckboxSize / 2 - CheckboxCharOffsetX;
            var _y = Height / 2 - CheckboxCharFont.Height / 2 - CheckboxCharOffsetY;
            using (var brush = new SolidBrush(charColor))
            {
                e.Graphics.DrawString(CheckboxChar, CheckboxCharFont, brush, new Point(_x, _y));
            }
        }
        #endregion
       
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshLabel();
        }

        void RefreshLabel()
        {
            //catch & break
            if (chklabel == null) return;

            //set new location
            var offsetX = CheckboxSize + 6 + LabelOffsetX;
            var midHeight = Height / 2 - LabelFont.Height / 2 + LabelOffsetY;
            chklabel.Location = new Point(offsetX, midHeight);

            // set text
            chklabel.Text = _LabelText;

            //set font
            chklabel.Font = LabelFont;

            //set size
            var _width = Width - CheckboxSize;
            chklabel.Size = new Size(_width, Height);

            //set fore color
            chklabel.ForeColor = LabelForeColor;

            //refresh paint
            //Invalidate();
        }

        //load control
        public void OnLoad()
        {
            Invalidate();
            RefreshLabel();
        }
    }
}
