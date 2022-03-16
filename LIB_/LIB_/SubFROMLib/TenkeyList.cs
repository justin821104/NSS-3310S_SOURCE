using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public delegate void EventHandler_();
    public partial class TenkeyList : Form{
        Control ctlSender;
        public static string[] m_KEY_SECTION = new string[20];
        public static string[,] m_KEYNAME = new string[20, 50];
        public event EventHandler_ SEND_TENKEY;
        public short mKEY_VAL = 0;
        string sMassege = string.Empty;

        public TenkeyList(){
            InitializeComponent();
        }

        public void INI(){
            lbl_Warning.Text = mKEY_VAL < 0 ? "NULL" : mKEY_VAL.ToString();
            READ_TENKEY(PATH_.Tenkey);
            COLOR_SELECT();
            Show();
            BringToFront();
        }

        private void LOAD_ITEM(short i){
            listBox1.Items.Clear();
            for (short k = 0; k < 35; k++) if (m_KEYNAME[i + 1, k] != null) listBox1.Items.Add(m_KEYNAME[i + 1, k]);
        }
        public void READ_TENKEY(string fileName){
            short I = 0;
            short K = 0;
            try{
                StreamReader ReadFile = new StreamReader(fileName);
                while (ReadFile.Peek() != -1){
                    sMassege = ReadFile.ReadLine();
                    if (!string.IsNullOrEmpty(sMassege)){
                        if (sMassege.Substring(0, 1) == "*"){
                            string[] strArray1 = sMassege.Split('*');
                            m_KEY_SECTION[I] = strArray1[1];
                            K = 0;
                            I++;
                        }
                        else{
                            string[] strArray2 = sMassege.Split('.');
                            if (strArray2.Length >= 1){
                                m_KEYNAME[I, K] = sMassege;
                                K++;
                            }
                        }
                    }
                }

                ReadFile.Close();
            }
            catch (Exception e) { MessageBox.Show("READ TENEKY FAIL " + e.Message); }
        }

        void COLOR_SELECT(){
            radioButton1.BackColor = radioButton1.Checked ? Color.Yellow : Color.White;
            radioButton2.BackColor = radioButton2.Checked ? Color.Yellow : Color.White;
            radioButton3.BackColor = radioButton3.Checked ? Color.Yellow : Color.White;
            radioButton4.BackColor = radioButton4.Checked ? Color.Yellow : Color.White;
            radioButton5.BackColor = radioButton5.Checked ? Color.Yellow : Color.White;
            radioButton6.BackColor = radioButton6.Checked ? Color.Yellow : Color.White;
            radioButton7.BackColor = radioButton7.Checked ? Color.Yellow : Color.White;
            radioButton8.BackColor = radioButton8.Checked ? Color.Yellow : Color.White;
            radioButton9.BackColor = radioButton9.Checked ? Color.Yellow : Color.White;
            radioButton10.BackColor = radioButton10.Checked ? Color.Yellow : Color.White;
            radioButton11.BackColor = radioButton11.Checked ? Color.Yellow : Color.White;
            radioButton12.BackColor = radioButton12.Checked ? Color.Yellow : Color.White;
            radioButton13.BackColor = radioButton13.Checked ? Color.Yellow : Color.White;
            radioButton14.BackColor = radioButton14.Checked ? Color.Yellow : Color.White;
            radioButton15.BackColor = radioButton15.Checked ? Color.Yellow : Color.White;
            radioButton16.BackColor = radioButton16.Checked ? Color.Yellow : Color.White;
        }

        private void TimerTenkey_Tick(object sender, EventArgs e){

        }

        private void EXCUTE_Click(object sender, EventArgs e){
            SEND_TENKEY();
        }

        private void TenkeyList_SelectedIndexChanged(object sender, EventArgs e){
            try{
                sMassege = listBox1.SelectedItems[0].ToString().Trim();
                string[] strArray1 = sMassege.Split('.');
                if (strArray1.Length != 2) return;
                mKEY_VAL = Convert.ToInt16(strArray1[0]);
                lbl_Warning.Text = strArray1[0];
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void SectionSelect_CheckedChanged(object sender, EventArgs e){
            ctlSender = (RadioButton)sender;
            LOAD_ITEM(Convert.ToInt16(ctlSender.TabIndex));
            COLOR_SELECT();
        }

        private void TenkeyList_Load(object sender, EventArgs e){
            radioButton1.Checked = true;
            foreach (Control mControl1 in groupBox1.Controls) mControl1.Text = m_KEY_SECTION[mControl1.TabIndex];
            LOAD_ITEM(0);
        }
    }
}