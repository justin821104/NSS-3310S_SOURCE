using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class ManualRepeat : Form{
        public int iRunRepeat = 0;
        public int iRunRepeatOff = 0;
        public string sMSG = string.Empty;

        public ManualRepeat(){
            InitializeComponent();
        }

        public void INI_(){
            Width = 395;
            Height = 170;
            tmrManualRepeat.Enabled = true;
            iRunRepeat = 0;
            iRunRepeatOff = 0;
            tmrManualRepeat.Enabled = true;
            BackColor = Color.Gray;
            lbManualName.Text = sMSG;
            ShowDialog();
        }

        void LAMP_BLICK(){
            if (DATA_.bMANUAL_REPEAT){
                if (iRunRepeat > 5){
                    if (iRunRepeatOff > 10){
                        iRunRepeatOff = 0;
                        iRunRepeat = 0;
                        if (BackColor != SystemColors.ButtonFace) BackColor = SystemColors.ButtonFace;
                    }
                    else{
                        iRunRepeatOff += 1;
                        if (BackColor != Color.Lime) BackColor = Color.Lime;
                    }
                }
                else{
                    iRunRepeat += 1;
                    if (BackColor != Color.Lime) BackColor = Color.Lime;
                }
            }
            else{
                iRunRepeat = 0;
                iRunRepeatOff = 0;
            }
        }
        private void ManualRepeat_Tick(object sender, EventArgs e) { LAMP_BLICK(); }

        private void RepeatStart_Click(object sender, EventArgs e){
            DATA_.iMANUAL.Label = lbManualName.Text;
            DATA_.bMANUAL_REPEAT = true;
        }

        private void RepeatStop_Click(object sender, EventArgs e){
            DATA_.bMANUAL_REPEAT = false;
            BackColor = Color.Gray;
        }

        private void Close_Click(object sender, EventArgs e){
            DATA_.bMANUAL_REPEAT = false;
            tmrManualRepeat.Enabled = false;
            BackColor = Color.Gray;
            Hide();
        }
    }
}