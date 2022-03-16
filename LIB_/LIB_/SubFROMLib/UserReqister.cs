using Object;
using System.IO;
using System.Windows.Forms;
using LIB_.DateType;

namespace LIB_.SubFROMLib{
    public partial class UserReqister : Form{
        int mCol, mRow = 0;
        int nSelectUserID = -1;
        DataGridView dgv = null;

        public UserReqister(){
            InitializeComponent();

            btnClose.Click += (sender, e) => FrmClose();
            ADD_INFO_CLEAR.Click += (sender, e) => AddClear();
            ADD_INFO_USER.Click += (sender, e) => Register();
            DEL_INFO_USER.Click += (sender, e) => DellInfoUser();
            MODIFY_INFO_USER.Click += (sender, e) => Correction();
        }
        void FrmClose(){
            TMR_CHECK.Enabled = false;
            Hide();
        }

        bool CheckUserID(string idx){
            
            int id = CUSER.List.FindIndex(x => x.ID == idx);
            return (id == -1) ? true : false;
        }

        bool AddUserID(string sID, string sName, string sPassword){
            CUSER_INFO dummy = CUSER.List.Find(x => x.ID == sID /*&& x.Name == sName*/);

            if (dummy != null)
            {
                //동일 아이템이 있다
                return true;
            }
            CUSER.List.Add(new CUSER_INFO
            {
                ID = sID,
                Name = sName,
                Password = sPassword
            });
            return false;
        }
        void Register(){
            if (ADD_USER_ID.Text == ""){
                MessageBox.Show("USER ID 입력 하셔야 합니다!");
                return;
            }
            if (ADD_USER_NAME.Text == ""){
                MessageBox.Show("USER NAME 입력 하셔야 합니다!");
                return;
            }
            if (!CheckUserID(ADD_USER_ID.Text)){
                MessageBox.Show("동일한 USER ID가 존재 합니다!");
                return;
            }
            if (ADD_USER_PASSWORD.Text == ""){
                MessageBox.Show("PASSWORD 입력 하셔야 합니다!");
                return;
            }
            if (ADD_USER_COFIRM.Text == ""){
                MessageBox.Show("COFIRM 입력 하셔야 합니다!");
                return;
            }
            if (ADD_USER_PASSWORD.Text != ADD_USER_COFIRM.Text){
                MessageBox.Show("PASSWORD REGISTRATION FAILED !" + ETC.CrLf + "PASSWORD DO NOT MATCH !");
                return;
            }

            bool bRTN = AddUserID(ADD_USER_ID.Text, ADD_USER_NAME.Text, ADD_USER_PASSWORD.Text);
            if (bRTN){
                MessageBox.Show("동일한 사원 번호 입력되어 있습니다 !");
                return;
            }
            CUSER.WRITR();
            //CUSER.READ();
            ViewInfoUserList();
            AddClear();
            MessageBox.Show("등록 되었습니다!");
        }

        void DellInfoUser(){
            if (nSelectUserID < 0){
                MessageBox.Show("삭제 할 유닛 정보를 클릭하셔야 합니다!");
                return;
            }
            if (UTIL_.INPUT_MESSAGE("USER 수정", "USER 정보 삭제 !", "", true) != CUSER.List[nSelectUserID].Password){
                MessageBox.Show("패스워드가 틀립니다!");
                return;
            }
            if (CUSER.Current.ID == CUSER.List[nSelectUserID].ID){
                MessageBox.Show("현재 등록된 사용자 ID 입니다!" + ETC.NewLine + "USER ID 변경 후 삭제 하셔야 합니다.");
                return;
            }

            CUSER.List.RemoveAt(nSelectUserID); // 0127 JIK
            CUSER.WRITR();
            ViewInfoUserList();
            ModifyClear();
            MessageBox.Show("삭제 되었습니다!");
        }
        void Correction(){
            if (nSelectUserID < 0){
                MessageBox.Show("수정 할 유닛 정보를 클릭하셔야 합니다!");
                return;
            }
            if (UTIL_.INPUT_MESSAGE("USER 수정", "USER 정보 변경 !", "", true) != CUSER.List[nSelectUserID].Password){
                MessageBox.Show("패스워드가 틀립니다!");
                return;
            }
            if (CUSER.Current.ID == CUSER.List[nSelectUserID].ID){
                MessageBox.Show("현재 등록된 사용자 ID 입니다!" + ETC.NewLine + "USER ID 변경 후 수정 하셔야 합니다.");
                return;
            }

            string sOLD_ID = CUSER.List[nSelectUserID].ID;
            CUSER.List[nSelectUserID].ID = MODIFY_USER_ID.Text;
            CUSER.List[nSelectUserID].Name = MODIFY_USER_NAME.Text;
            CUSER.WRITR();
            ViewInfoUserList();
            ModifyClear();
            MessageBox.Show("수정 되었습니다!");
        }

        void ModifyClear(){
            MODIFY_USER_ID.Text = "";
            MODIFY_USER_NAME.Text = "";
            UTIL_.CLEAR_GRID_SELECTED(ref GRD_UESR_LIST);
            nSelectUserID = -1;
        }
        void AddClear(){
            ADD_USER_ID.Text = "";
            ADD_USER_NAME.Text = "";
            ADD_USER_PASSWORD.Text = "";
            ADD_USER_COFIRM.Text = "";
        }
        public void Initailize_View(){
            AddClear();
            ModifyClear();
            ViewInfoUserList();
            nSelectUserID = -1;
            TMR_CHECK.Enabled = true;

            Left = 0;
            ShowDialog();
            BringToFront();
        }

        void ViewInfoUserList(){
            GRD_UESR_LIST.DataSource = null;
            GRD_UESR_LIST.DataSource = CUSER.List;
            UTIL_.CLEAR_GRID_SELECTED(ref GRD_UESR_LIST);
        }

        private void GRD_UESR_LIST_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            UTIL_.GET_GRID_NUMBER(dgv, ref mCol, ref mRow);
            if (DATA_.eMCStatus == eMachineStatus.AUTO || mRow < 0 || mCol <= 0){
                UTIL_.CLEAR_GRID_SELECTED(ref dgv);
                return;
            }
            nSelectUserID = mRow;
            MODIFY_USER_ID.Text = CUSER.List[nSelectUserID].ID;
            MODIFY_USER_NAME.Text = CUSER.List[nSelectUserID].Name;
        }
    }
}