using System.Windows.Forms;
using LIB_.DateType;

namespace LIB_.SubFROMLib{
    public partial class UserID : Form{
        public UserID(){
            InitializeComponent();

            CLOSE.Click += (sender, e) => FormClose();
            OK.Click += (sender, e) => ChangeID();
        }
        void FormClose() { Hide(); }
        void ChangeID(){
            if (USER_ID.Text == ""){
                MessageBox.Show("USER ID 선택 하셔야 합니다!");
                return;
            }
            if (USER_ID.SelectedIndex < 0){
                MessageBox.Show("USER ID 다시 선택 하십시오!");
                return;
            }
            if (CUSER.List[USER_ID.SelectedIndex].Password != USER_PASSWORD.Text){
                MessageBox.Show("패스워드가 틀립니다!");
                return;
            }
            CUSER.Current = CUSER.List.Find(x=>x.ID == USER_ID.Text.Trim());
            CUSER.WRITE_CURRENT_USER();
            Hide();
        }

        public void Initailize_View(){
            USER_ID.Items.Clear();

            for (int i = 0; i < CUSER.List.Count; i++){
                USER_ID.Items.Add(CUSER.List[i].ID);
            }
            USER_PASSWORD.Text = "";

            ShowDialog();
            Left = 0;
            BringToFront();
        }
        public void Set_Language(){

        }
    }
}