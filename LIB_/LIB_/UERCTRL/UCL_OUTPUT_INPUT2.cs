using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace LIB_.UERCTRL{
    public partial class UCL_OUTPUT_INPUT2 : UserControl{
        public event EventHandler OnButtonClickOut;

        public UCL_OUTPUT_INPUT2(){
            InitializeComponent();

            btnOUT.Click += OnClick;
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
        public int NUM_INPUT1{
            get { return inLED1.TabIndex; }
            set { inLED1.TabIndex = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Color))]
        public Color ColorINPUT1{
            get { return inLED1.BackColor; }
            set { inLED1.BackColor = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NUM_INPUT2{
            get { return inLED2.TabIndex; }
            set { inLED1.TabIndex = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Color))]
        public Color ColorINPUT2{
            get { return inLED2.BackColor; }
            set{
                inLED2.BackColor = value;
                this.Invalidate();
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string InputTEXT_1{
            get { return inLED1.Text; }
            set { inLED1.Text = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string InputTEXT_2{
            get { return inLED2.Text; }
            set { inLED2.Text = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NUM_OUTPUT{
            get { return outLED.TabIndex; }
            set { outLED.TabIndex = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Color))]
        public Color ColorOUTPUT{
            get { return outLED.BackColor; }
            set{
                outLED.BackColor = value;
                this.Invalidate();
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string OutTEXT{
            get { return outLED.Text; }
            set { outLED.Text = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string TEXT{
            get { return btnOUT.Text; }
            set { btnOUT.Text = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NUM_MANUAL{
            get { return btnOUT.TabIndex; }
            set { btnOUT.TabIndex = value; }
        }
    }
}