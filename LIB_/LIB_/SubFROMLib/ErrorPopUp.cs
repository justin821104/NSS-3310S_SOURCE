using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class ErrorPopUp : Form{
        public long sTime = 0, eTime = 0;
        int iERR = 0;

        public ErrorPopUp(){
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e){
            tmrError.Enabled = false;
            COM_.BZ_OFF();
            Hide();
        }

        private void ErrorPopUp_FormClosing(object sender, FormClosingEventArgs e) { tmrError.Enabled = false; }

        private void TimerError_Tick(object sender, EventArgs e){
            if (!DATA_.bOnERROR){
                tmrError.Enabled = false;
                Hide();
            }
            for (int i = 0; i < gridError.RowCount; i++){
                //gridError.Rows[i].Cells[0].Value = 
            }
        }

        public void VIEW_ERROR_POPUP(){
            gridError.RowCount = 5;
            tmrError.Enabled = true;
            Show();
            iERR = UTIL_.CHK_ERR();
            gridError.Rows[0].Cells[0].Value = iERR;
            gridError.Rows[0].Cells[1].Value = UTIL_.GET_ERROR_NAME(iERR);
        }
    }
}