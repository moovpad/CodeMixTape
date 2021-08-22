using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.CustomControls
{
    [DefaultEvent("cTextChanged")]
    public partial class CustomTXT : UserControl
    {
        //Fields - Added manually
        private Color bColor = Color.MediumSlateBlue; //border colour
        private int bSize = 2; //border size
        private bool underlined = false; //underlined style for the control
        private Color bFocusedColor = Color.HotPink; //a colour for when the user control has focus
        private bool isFocused = false; //Actually manipulated by the Enter and Leave methods below (optional declaration, since you can use the Focus property of the textbox)
        
        //Auto-generated
        public CustomTXT()
        {
            InitializeComponent();
        }


        //Events - Manually added
        public event EventHandler cTextChanged; //since this becomes our new default event, it also needs to be declared as such in the class header (see above class declaration)


        //Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public Color BColor 
        {
            get
            {
                return bColor;
            }
            set
            {
                bColor = value;
                this.Invalidate(); //this re-draws the control when the property is set
            }
        }

        //Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public int BSize
        {
            get
            {
                return bSize;
            }
            set
            {
                bSize = value;
                this.Invalidate(); //this re-draws the control when the property is set
            }
        }

        //Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public bool Underlined
        {
            get
            {
                return underlined;
            }
            set
            {
                underlined = value;
                this.Invalidate(); //this re-draws the control when the property is set
            }
        }

        //Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public bool PasswordChar
        {
            get
            {
                return textBox1.UseSystemPasswordChar;
            }
            set
            {
                textBox1.UseSystemPasswordChar = value;
            }
        }

        //Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public bool Multiline //notice this is different to 'MultiLine', the default property (different case L)
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
            }
        }

        //Overridden methods - Added manually
        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (this.DesignMode) //called to make sure the control resizes to fit the font size selected
                    UpdateControlHeight();
            }
        }

        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public String cText //this method was done different to an override because apparently doing an override has "drawbacks"... ???
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        [Category("Custom Properties")] //this line allows the grouping of all the special properties into their own category in the Properties pane
        public Color BFocusedColor
        {
            get
            {
                return bFocusedColor;
            }
            set
            {
                bFocusedColor = value;
            }
        }

        public bool IsFocused
        {
            get
            {
                return isFocused;
            }
            set
            {
                isFocused = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            //Draw border
            using (Pen penBorder = new Pen(bColor, bSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                if (!isFocused)
                {
                    if (underlined) //Line style if 'underlined' = true
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else //draw a normal border
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
                else
                {
                    penBorder.Color = bFocusedColor;

                    if (underlined) //Line style if 'underlined' = true
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else //draw a normal border
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight(); //also a manually created private method and put into IF to make sure it's only called during deisgn mode and not unnecessarily called at runtime

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        //Private Methods
        private void UpdateControlHeight()
        {
            if(textBox1.Multiline == false) //this specifies that the following only applies as long as the textbox is not multiline
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1; //gets the height of the text font and adds 1
                //since we can't directly change the height of the textbox, we do the following
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;

                //now we set the overall height of the control to be height of textbox plus top and bottom padding
                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }


        //EVENT METHODS
        private void textBox1_cTextChanged(object sender, EventArgs e)
        {
            if (cTextChanged != null)
            {
                cTextChanged.Invoke(sender, e);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
        }
    }
}
