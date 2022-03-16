using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class TENKEY : Form{
        Control ctl = null;
        public bool bOPTION = false;
        public bool bCANCLE = false;
        private double OffsetVal = 0.001;
        private bool IsFIRST = false;
        private bool bFORCUS = true;
        private string OldVal = string.Empty;
        double dValue = 0;
        double dVAL = 0;

        string sVALUE = string.Empty;
        string sSELECT = string.Empty;
        int iSIZE = 0;

        public TENKEY(){
            InitializeComponent();

            editValue.Click += (sender, e) => { VALUE_CLICK(); };
            TXT_MINUS.Click += (sender, e) => { MINUS_CLICK(); };
        }

        void VIEW_(bool bFIRST){
            //390, 400 -> 305, 400 //305, 400
            if (bFIRST){
                Width = 305;
                rbn_0001.Checked = true;
            }
            else{
                if (Width == 390) Width = 305;
                else Width = 390;
            }
            Height = 400;
        }

        public void INI(){
            IsFIRST = false;
            bCANCLE = false;
            OldVal = editValue.Text;
            TXT_MINUS.Text = "";
            editValue.Focus();
            ShowDialog();
            DATA_.mTenkeyResult = editValue.Text;
        }

        private void Offset_Pitch_Click(object sender, EventArgs e){
            ctl = (RadioButton)sender;
            OffsetVal = double.Parse(ctl.Text);
        }

        public void VALUE_CLICK() { bFORCUS = true; }
        public void MINUS_CLICK() { bFORCUS = false; }

        private void KEYPAD_CLICK(object sender, EventArgs e){
            ctl = (Button)sender;
            dVAL = 0;

            if (ctl.Name == "swSign"){
                try{
                    if (bFORCUS){
                        dVAL = double.Parse(editValue.Text);
                        dVAL = -dVAL;
                        editValue.Text = dVAL.ToString();
                        DATA_.mTenkeyResult = editValue.Text;
                    }
                    else{
                        dVAL = double.Parse(TXT_MINUS.Text);
                        dVAL = -dVAL;
                        TXT_MINUS.Text = dVAL.ToString();
                    }
                }
                catch (Exception ex){
                    MessageBox.Show("fTENKEY -> swSign_Click Fail !" + ETC.NewLine + ex.ToString());
                    return;
                }
            }
            else if (ctl.Name == "swBack"){
                if (bFORCUS){
                    if (editValue.Text == "") return;
                    editValue.Text = Microsoft.VisualBasic.Strings.Left(editValue.Text, editValue.Text.Length - 1);
                    DATA_.mTenkeyResult = editValue.Text;
                }
                else{
                    if (TXT_MINUS.Text == "") return;
                    TXT_MINUS.Text = Microsoft.VisualBasic.Strings.Left(TXT_MINUS.Text, TXT_MINUS.Text.Length - 1);
                }
            }
            else if (ctl.Name == "swClear"){
                if (bFORCUS){
                    IsFIRST = false;
                    editValue.Text = "0";
                    editValue.Focus();
                    DATA_.mTenkeyResult = editValue.Text;
                }
                else TXT_MINUS.Text = "0";
            }
            else if (ctl.Name == "swCancel"){
                editValue.Text = OldVal;
                DATA_.mTenkeyResult = editValue.Text;
                TXT_MINUS.Text = "";
                TXT_MINUS.Visible = false;
                bCANCLE = true;
                DialogResult = DialogResult.Cancel;
            }
            else if (ctl.Name == "swEnter"){
                try{
                    if (bFORCUS){
                        if (editValue.Text == "") return;
                        dVAL = double.Parse(editValue.Text);
                        DATA_.mTenkeyResult = editValue.Text;
                    }
                    else{
                        if (TXT_MINUS.Text == "") return;
                        dVAL = double.Parse(TXT_MINUS.Text);
                        editValue.Text = (double.Parse(editValue.Text) + dVAL).ToString();
                        bFORCUS = true;
                        return;
                    }
                }
                catch (Exception ex){
                    MessageBox.Show("fTENKEY -> swEnter_Click Fail !" + ETC.NewLine + ex.ToString());
                    return;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void TENKEY_NUMBER_CLICK(object sender, EventArgs e){
            ctl = (Button)sender;
            sVALUE = editValue.Text;
            sSELECT = editValue.SelectedText;
            iSIZE = editValue.Text.Length;

            if (bFORCUS){
                if (!IsFIRST){
                    editValue.Text = ctl.Text;
                    IsFIRST = true;
                    DATA_.mKeyBoardResult = editValue.Text;
                    return;
                }
                if (ctl.Text == "."){
                    if (editValue.Text.IndexOf(".") >= 0) return;
                }
                if (editValue.Text == "0" && ctl.Text == "0") return;
                editValue.Text += ctl.Text;
                DATA_.mTenkeyResult = editValue.Text;
            }
            else{
                if (ctl.Text == "."){
                    if (TXT_MINUS.Text.IndexOf(".") >= 0) return;
                }
                if (TXT_MINUS.Text == "0" && ctl.Text == "0") return;
                TXT_MINUS.Text += ctl.Text;
            }
        }

        private void Value_KeyPress(object sender, KeyPressEventArgs e){
            if (e.KeyChar != ETC.cspCr) return;
            KEYPAD_CLICK(swEnter, EventArgs.Empty);
        }

        private void OPTION_Click(object sender, EventArgs e){
            ctl = (Button)sender;
            dValue = 0;

            if (ctl.Name == "swInfo") VIEW_(false);
            else if (ctl.Name == "swUp") dValue = double.Parse(editValue.Text) + OffsetVal;
            else if (ctl.Name == "swDown") dValue = double.Parse(editValue.Text) - OffsetVal;
            editValue.Text = dValue.ToString();
            DATA_.mTenkeyResult = editValue.Text;
        }

        private void TENKEY_Load(object sender, EventArgs e) { VIEW_(true); }
    }
}