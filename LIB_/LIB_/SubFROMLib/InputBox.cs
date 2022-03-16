using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class InputBox : Form{
        public string sRESULT = string.Empty;
        Button gBTN;

        public InputBox(){
            InitializeComponent();
        }

        private void EditInput_KeyPress(object sender, KeyPressEventArgs e){
            if (e.KeyChar != ETC.cspCr) return;
            sRESULT = editInput.Text;
            this.Hide();
        }

        private void InputBox_Click(object sender, EventArgs e){
            gBTN = sender as Button;
            if (gBTN.Name == "swYes"){
                sRESULT = editInput.Text;
                this.Hide();
            }
            else if (gBTN.Name == "swClose"){
                editInput.Text = "";
                this.Close();
            }
        }

        private void InputBox_Shown(object sender, EventArgs e) { editInput.Focus(); }
    }
}