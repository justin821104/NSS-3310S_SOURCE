using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace LIB_.UERCTRL{
    public partial class UCL_MOVE : UserControl{
        public UCL_MOVE(){
            InitializeComponent();

            btnMOVE.Click += OnClick;
        }

        [NonSerialized]
        private EventHandler btnClick;
        public event EventHandler BtnClick{
            add { btnClick += value; }
            remove { btnClick -= value; }
        }
        protected void OnClick(object sender, EventArgs e){
            EventHandler handler = btnClick;
            if (btnClick != null) handler(sender, e);
        }


        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string TEXT{
            get { return btnMOVE.Text; }
            set { btnMOVE.Text = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NumMANUAL{
            get { return btnMOVE.TabIndex; }
            set { btnMOVE.TabIndex = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Color))]
        public Color ColorPOS{
            get { return led.BackColor; }
            set{
                led.BackColor = value;
                this.Invalidate();
            }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Image))]
        public Image ImgBtn{
            get { return btnMOVE.Image; }
            set{
                btnMOVE.Image = value;
                this.Invalidate();
            }
        }
    }
}