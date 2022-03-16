using System.Windows.Forms;
using System;
using System.Drawing;

namespace ezGEM
{
    public partial class SecsGEM_TERMINAL : Form{
        SecsGEM gemFrm;

        public SecsGEM_TERMINAL(SecsGEM _gemForm){
            gemFrm = _gemForm;
            //Gem Form 연결.

            InitializeComponent();
        }

        private void btn_OK_Click(object sender, System.EventArgs e){
            label_message.Text = "";
            this.Hide();
        }

        public void AddString(string sMessage){
            if (this.InvokeRequired){
                Invoke((MethodInvoker)delegate ()
                {
                    AddString(sMessage);
                });
            }
            else{
                this.Text = "Form Teminal Message " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                label_message.Text = sMessage;
                label_message.BackColor = Color.White;
                label_message.ForeColor = Color.Black;

                this.Show();
            }
        }
    }
}
