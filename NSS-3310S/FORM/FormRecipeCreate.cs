using System;
using System.Windows.Forms;
using LIB_.DateType;

namespace NSS_3310S{
    public partial class FormRecipeCreate : Form{
        public bool bClose = false;
        public int nSelect = 0;
        enum eMode{
            Append = 0,
            Modify = 1
        }

        public FormRecipeCreate(){
            InitializeComponent();

            Cancel.Click += (sender, e) => Hide();
            OK.Click += (sender, e) => OK_Click(OK);
        }
        void OK_Click(object sender){
            if (DEVICE.Text == ""){
                MessageBox.Show("DEVICE 선택 하셔야 합니다!");
                return;
            }
            if (HANDLER_PPID.Text == ""){
                MessageBox.Show("HANDLER PPID 입력 하셔야 합니다!");
                return;
            }
            if (SAW_RECIPE.Text == ""){
                MessageBox.Show("SAW RECIPE 입력 하셔야 합니다!");
                return;
            }

            string sRecipeCreate = GROUP.Text + ETC.NewLine;
            sRecipeCreate += DEVICE.Text + ETC.NewLine;
            sRecipeCreate += GROUP.Text + "_" + (DEVICE.Text).Replace(".job", "") + ETC.NewLine;
            sRecipeCreate += SAW_RECIPE.Text;

            CMES.PPID = HANDLER_PPID.Text;
            CMES.PPID_SAW_GROUP = GROUP.Text;
            CMES.PPID_SAW_DEVICE = SAW_RECIPE.Text;
            CMES.PPID_SORTER_GROUP = GROUP.Text;
            CMES.PPID_SORTER_DEVICE = DEVICE.Text.Replace(".job", "");
            CMES.PPID_VISION_DEVICE = GROUP.Text + "_" + (DEVICE.Text).Replace(".job", "");

            DATA_.iMANUAL.Label = sRecipeCreate;

            if (nSelect == (int)eMode.Append){
                TEACH_.WR_PPID(CMES.PPID, DATA_.iMANUAL.Label);
                DATA_.IsSTRING[S.DeviceMessage] = "RECIPE 생성 완료 !";
                F.Device.bRecipeCreate = true;
            }
            else{
                TEACH_.WR_PPID(CMES.PPID, DATA_.iMANUAL.Label);
                DATA_.IsSTRING[S.DeviceMessage] = "RECIPE 변경 완료 !";
                F.Device.bRecipeCreate = true;
            }
        }

        public void Initailize_View(){
            GROUP.Text = F.Device.mSubItem;

            DEVICE.Text = nSelect == (int)eMode.Append ? "" : CMES.CUR_DEVICE;
            HANDLER_PPID.Text = nSelect == (int)eMode.Append ? "" : CMES.CurPPID;
            SAW_RECIPE.Text = nSelect == (int)eMode.Append ? "" : CMES.CUR_SAW;
            HANDLER_PPID.Enabled = nSelect == (int)eMode.Append ? true : false;

            DEVICE_INDEX.SelectedIndex = -1;
            HandlerRecipeList();
            SawRecipeList();
            pbxLoading.Visible = false;

            CHECK_DELAY_TIME.Enabled = true;
            ShowDialog();
            Left = 0;
            BringToFront();
        }

        void HandlerRecipeList(){
            string sList;
            DEVICE_INDEX.Items.Clear();
            if (DATA_.RecipeList == null) return;
            for (int i = 0; i < DATA_.RecipeList.Length; i++){
                sList = DATA_.RecipeList[i];
                DEVICE_INDEX.Items.Add(sList.Replace(".jog", ""));
            }
        }
        void SawRecipeList(){
            SAW_GROUP.Items.Clear();
            SAW_DEVICE.Items.Clear();
            int idx = -1;
            string[] sList;
            sList = DATA_.IsSTRING[S.SawRecipeList].Split('\n');
            for (int i = 0; i < sList.Length - 1; i++){
                string[] recipe = sList[i].Split(';');
                SAW_GROUP.Items.Add(recipe[0]);
                if (GROUP.Text == recipe[0]) idx = i;
            }
            if (idx < 0) SAW_GROUP.SelectedIndex = idx;
        }

        private void DEVICE_INDEX_SelectedIndexChanged(object sender, EventArgs e){
            if (DEVICE_INDEX.SelectedIndex < 0) return;
            DEVICE.Text = DATA_.RecipeList[DEVICE_INDEX.SelectedIndex];
        }
        private void SAW_GROUP_SelectedIndexChanged(object sender, EventArgs e){
            SAW_DEVICE.Items.Clear();
            if (SAW_GROUP.SelectedIndex < 0) return;

            string[] list;
            list = DATA_.IsSTRING[S.SawRecipeList].Split('\n');
            for (int i = 0; i < list.Length - 1; i++){
                if (SAW_GROUP.SelectedIndex != i) continue;
                string[] Recipe = list[i].Split(';');
                for (int j = 0; j < Recipe.Length; j++){
                    SAW_DEVICE.Items.Add(Recipe[j]);
                }
            }
        }

        private void SET_SAW_RECIPE_Click(object sender, EventArgs e){
            if (SAW_GROUP.SelectedIndex < 0 || SAW_GROUP.Text == ""){
                MessageBox.Show("SAW 디바이스 GROUP를(을) 선택 하셔야 합니다!");
                return;
            }
            if (SAW_DEVICE.SelectedIndex < 0 || SAW_DEVICE.Text == ""){
                MessageBox.Show("SAW 디바이스 RECIPE를(을) 선택 하셔야 합니다!");
                return;
            }
            SAW_RECIPE.Text = SAW_GROUP.Text + "_" + SAW_DEVICE.Text;
        }

        private void CHECK_DELAY_TIME_Tick(object sender, EventArgs e){
            CHECK_DELAY_TIME.Enabled = false;
            Invoke();
            CHECK_DELAY_TIME.Enabled = true;
        }
        void Invoke(){
            if (bClose){
                bClose = false;
                CHECK_DELAY_TIME.Enabled = false;
                Hide();
            }
        }
    }
}