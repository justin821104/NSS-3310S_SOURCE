using Object;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace LIB_.UERCTRL{
    public partial class UCL_JOG : UserControl{
        public event EventHandler OnCcwMouseDown_Click;
        public event EventHandler OnCwMouseDown_Click;
        public event EventHandler OnMouseUp_Click;

        bool chkIncMove;     //= false;
        double dPitch = 0;

        public UCL_JOG(){
            InitializeComponent();
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(int))]
        public int NumMT{
            get { return lblMotor.TabIndex; }
            set { lblMotor.TabIndex = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string TEXT{
            get { return lblMotor.Text; }
            set { lblMotor.Text = value; }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public double CurPosValue{
            get { return double.Parse(CurPOS.DigitText); }
            set{
                CurPOS.DigitText = value.ToString();
                this.Invalidate();
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Image))]
        public Image ImgJogCcw{
            get { return JogCcw.Image; }
            set{
                JogCcw.Image = value;
                this.Invalidate();
            }
        }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(Image))]
        public Image ImgJogCw{
            get { return JogCw.Image; }
            set{
                JogCw.Image = value;
                this.Invalidate();
            }
        }

        public double Pitch{
            get { return dPitch; }
            set { dPitch = value; }
        }

        public eArrow SetBtnImage_Cw { set { JogCw.ImageIndex = (int)value; } }
        public eArrow SetBtnImage_Ccw { set { JogCcw.ImageIndex = (int)value; } }

        public Color StateMotor { set; get; }

        public double CurPosition{
            set { CurPOS.DigitText = value.ToString("0.000"); }
            get{
                return double.Parse(CurPOS.DigitText);
                //Invalidate();
            }
        }

        public double JogMovePitch{
            set { dPitch = Math.Abs(value); }
            get { return dPitch; }
        }
        public bool SetIncMove{
            set { chkIncMove = value; }
            get { return chkIncMove; }
        }

        private void JogMove_MouseUp(object sender, MouseEventArgs e){
            OnMouseUp_Click?.Invoke(this, e);
        }

        private void JogCcw_MouseDown(object sender, MouseEventArgs e){
            OnCcwMouseDown_Click?.Invoke(this, e);
        }

        private void JogCw_MouseDown(object sender, MouseEventArgs e){
            OnCwMouseDown_Click?.Invoke(this, e);
        }
    }
}