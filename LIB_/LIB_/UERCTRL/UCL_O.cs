using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace LIB_.UERCTRL{
    public partial class UCL_O : UserControl{
        public event EventHandler OnButtonClickOut;

        public UCL_O(){
            InitializeComponent();

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

        private void OUT_Click(object sender, EventArgs e){
            OnButtonClickOut?.Invoke(this, e);
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NumOUT{
            get { return led.TabIndex; }
            set { led.TabIndex = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Color))]
        public Color ColorOUT{
            get { return led.BackColor; }
            set{
                led.BackColor = value;
                this.Invalidate();
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string OutTEXT{
            get { return led.Text; }
            set { led.Text = value; }
        }


        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string TEXT{
            get { return btnOUT.Text; }
            set { btnOUT.Text = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NumMANUAL{
            get { return btnOUT.TabIndex; }
            set { btnOUT.TabIndex = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Image))]
        public Image ImgBtn{
            get { return btnOUT.Image; }
            set{
                btnOUT.Image = value;
                this.Invalidate();
            }
        }
    }
}