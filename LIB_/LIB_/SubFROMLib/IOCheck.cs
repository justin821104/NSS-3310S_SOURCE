using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class IOCheck : Form{
        DataGridView dgv = null;
        RadioButton rbt = null;
        int mCol, mRow = 0;
        string sMassege = string.Empty;
        string sINPUT = string.Empty;
        string sOUTPUT = string.Empty;
        int nINPUT = 0;
        int nOUT = 0;
        int nNUM = 0;
        int tNUM = 0;
        public string PathLogIOCheck { get; set; }

        public IOCheck(){
            InitializeComponent();
        }

        public void GET_ColRow(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public string GET_TextIn(int col, int row){
            try{
                sMassege = gridIn.Rows[row].Cells[col].Value.ToString();
                if (sMassege == null) sMassege = "";
                return sMassege;
            }
            catch (Exception ex){
                LogWR_.SaveLogException("fIOCheck -> GET_TextIn Fail", ex);
                return "NOT NAME";
            }
        }
        public void SET_ColorIn(int col, int row, Color cColor){
            sMassege = GET_TextIn(1, row);
            if (sMassege.Length < 2){
                if (gridIn.Rows[row].Cells[col].Style.BackColor != Color.Silver) gridIn.Rows[row].Cells[col].Style.BackColor = Color.Silver;
                return;
            }
            gridIn.Rows[row].Cells[col].Style.BackColor = cColor;
        }

        public string GET_TextOut(int col, int row){
            try{
                sMassege = gridOut.Rows[row].Cells[col].Value.ToString();
                if (sMassege == null) sMassege = "";
                return sMassege;
            }
            catch (Exception ex){
                LogWR_.SaveLogException("fIOCheck -> GET_TextOut Fail", ex);
                return "NOT NAME";
            }
        }
        public void SET_ColorOut(int col, int row, Color cColor){
            sMassege = GET_TextOut(col, row);
            if (sMassege.Length < 2){
                if (gridOut.Rows[row].Cells[col].Style.BackColor != Color.Silver) gridOut.Rows[row].Cells[col].Style.BackColor = Color.Silver;
                return;
            }
            gridOut.Rows[row].Cells[col].Style.BackColor = cColor;
        }

        public void INI_(){
            TEACH_.Read_InfoIO();
            gridIn.RowCount = CNT_.numInLast;
            for (short i = 0; i < CNT_.numInLast; i++){
                gridIn.Rows[i].Cells[0].Value = i.ToString();
                gridIn.Rows[i].Cells[1].Value = DATA_.InputName[i];
                gridIn.Rows[i].Cells[2].Value = "0x" + string.Format("{0:X}", i);

                SET_ColorIn(0, i, Color.Gray);
                SET_ColorIn(2, i, Color.Gray);

                sINPUT = GET_TextIn(1, i);
                if (sINPUT.Length < 2){
                    gridIn.Rows[i].Cells[3].Value = "";
                    gridIn.Rows[i].Cells[4].Value = "";
                    continue;
                }

                if (DATA_.chkIN[i].ContactB) gridIn.Rows[i].Cells[3].Value = "B";
                else gridIn.Rows[i].Cells[3].Value = "A";

                if (DATA_.chkIN[i].Checked) gridIn.Rows[i].Cells[4].Value = "OK";
                else gridIn.Rows[i].Cells[4].Value = "NO";
            }

            gridOut.RowCount = CNT_.numOutLast;
            for (short i = 0; i < CNT_.numOutLast; i++){
                gridOut.Rows[i].Cells[0].Value = i.ToString();
                gridOut.Rows[i].Cells[1].Value = DATA_.OutputName[i];
                gridOut.Rows[i].Cells[2].Value = "0x" + string.Format("{0:X}", i);
                gridOut.Rows[i].Cells[4].Value = "ON/OFF";

                SET_ColorOut(0, i, Color.Gray);
                SET_ColorOut(2, i, Color.Gray);

                sOUTPUT = GET_TextOut(1, i);
                if (sOUTPUT.Length < 2)
{
                    gridOut.Rows[i].Cells[3].Value = "--";
                    continue;
                }

                if (DATA_.chkOUT[i].Checked) gridOut.Rows[i].Cells[3].Value = "OK";
                else gridOut.Rows[i].Cells[3].Value = "NO";
            }
            this.Show();
            tmrIOCheck.Enabled = true;
        }

        public int GET_IONum(DataGridView g, int col, int row){
            sMassege = g.Rows[row].Cells[col].Value.ToString();
            sMassege = sMassege.Replace("0x", "");
            sMassege = string.Format("{0:X}", sMassege);
            nNUM = Convert.ToInt32(sMassege, 16);
            return nNUM;
        }

        private void Numeral_Click(object sender, EventArgs e){
            rbt = (RadioButton)sender;
            if (rbt.Name == "rb10"){
                for (short i = 0; i < CNT_.numInLast; i++) gridIn.Rows[i].Cells[0].Value = i.ToString();
                for (short i = 0; i < CNT_.numOutLast; i++) gridOut.Rows[i].Cells[0].Value = i.ToString();
            }
            else if (rbt.Name == "rb16"){
                for (short i = 0; i < CNT_.numInLast; i++) gridIn.Rows[i].Cells[0].Value = string.Format("{0:X}", i);
                for (short i = 0; i < CNT_.numOutLast; i++) gridOut.Rows[i].Cells[0].Value = string.Format("{0:X}", i);
            }
        }

        private void Report_Click(object sender, EventArgs e){
            sMassege = " ================================= INPUT ================================= " + ETC.CrLf;
            for (short i = 0; i < gridIn.RowCount; i++){
                string iNAME = GET_TextIn(1, i);
                int iNUM = GET_IONum(gridIn, 2, i);
                if (DATA_.InputName[i] == null) DATA_.InputName[i] = "";
                if (DATA_.InputName[i].Length < 2 && iNAME.Length > 2) sMassege += "입력추가 : " + iNAME + " -> " + gridIn.Rows[i].Cells[2].Value + ETC.CrLf;
                if (DATA_.InputName[i].Length > 2 && iNAME.Length < 2) sMassege += "입력제거 : " + iNAME + " -> " + gridIn.Rows[i].Cells[2].Value + ETC.CrLf;
                if (iNUM != i) sMassege += "입력변경 : " + DATA_.InputName[i] + " -> 0x" + string.Format("{0:X}", i) + " -> 0x" + string.Format("{0:X}", iNUM) + ETC.CrLf;
                if (DATA_.InputName[i] != iNAME) sMassege += "명칭변경 : " + DATA_.InputName[i] + " -> 0x" + string.Format("{0:X}", i) + " -> " + iNAME.Trim() + ETC.CrLf;
            }
            sMassege += ETC.CrLf;

            for (short i = 0; i < gridIn.RowCount; i++){
                string iNAME = GET_TextIn(1, i);
                int iNUM = GET_IONum(gridIn, 2, i);
                if (gridIn.Rows[i].Cells[4].Value.ToString() == "NO" && iNAME.Length > 2) sMassege += "미 체크 : " + DATA_.InputName[i] + " -> 0x" + string.Format("{0:X}", iNUM) + ETC.CrLf;
            }
            sMassege += ETC.CrLf;

            for (short i = 0; i < gridIn.RowCount; i++){
                string iNAME = GET_TextIn(1, i);
                int iNUM = GET_IONum(gridIn, 2, i);
                if (gridIn.Rows[i].Cells[3].Value.ToString() == "B" && iNAME.Length > 2) sMassege += "B 접점 : " + DATA_.InputName[i] + " -> 0x" + string.Format("{0:X}", iNUM) + ETC.CrLf;
            }

            sMassege += ETC.CrLf + " ================================= OUTPUT ================================= " + ETC.CrLf;
            for (short i = 0; i < gridOut.RowCount; i++){
                string oNAME = GET_TextOut(1, i);
                int oNUM = GET_IONum(gridOut, 2, i);
                if (DATA_.OutputName[i] == null) DATA_.OutputName[i] = "";
                if (DATA_.OutputName[i].Length < 2 && oNAME.Length > 2) sMassege += "출력추가 : " + oNAME + " -> " + gridOut.Rows[i].Cells[2].Value + ETC.CrLf;
                if (DATA_.OutputName[i].Length > 2 && oNAME.Length < 2) sMassege += "출력제거 : " + oNAME + " -> " + gridOut.Rows[i].Cells[2].Value + ETC.CrLf;
                if (oNUM != i) sMassege += "출력변경 : " + DATA_.OutputName[i] + " -> 0x" + string.Format("{0:X}", i) + " -> 0x" + string.Format("{0:X}", oNUM) + ETC.CrLf;
                if (DATA_.OutputName[i] != oNAME) sMassege += "명칭변경 : " + DATA_.OutputName[i] + " -> 0x" + string.Format("{0:X}", i) + " -> " + oNAME.Trim() + ETC.CrLf;
            }
            sMassege += ETC.CrLf;

            for (short i = 0; i < gridOut.RowCount; i++)
            {
                string oNAME = GET_TextOut(1, i);
                int oNUM = GET_IONum(gridOut, 2, i);
                if (gridOut.Rows[i].Cells[3].Value.ToString() == "NO" && oNAME.Length > 2) sMassege += "미 체크 : " + DATA_.OutputName[i] + " -> 0x" + string.Format("{0:X}", oNUM) + ETC.CrLf;
            }
            editReport.Text = sMassege;
            tapIO.SelectedIndex = 1;

            FILE_.WR_File(PathLogIOCheck, sMassege, false);
        }
        private void Save_Click(object sender, EventArgs e){
            for (int i = 0; i < gridIn.RowCount; i++){
                if (gridIn.Rows[i].Cells[3].Value.ToString() == "A") DATA_.chkIN[i].ContactB = false;
                else DATA_.chkIN[i].ContactB = true;

                if (gridIn.Rows[i].Cells[4].Value.ToString() == "OK") DATA_.chkIN[i].Checked = true;
                else DATA_.chkIN[i].Checked = false;
            }
            for (int i = 0; i < gridOut.RowCount; i++){
                if (gridOut.Rows[i].Cells[3].Value.ToString() == "OK") DATA_.chkOUT[i].Checked = true;
                else DATA_.chkOUT[i].Checked = false;
            }
            TEACH_.WR_InfoIO();
        }

        private void Out_DoubleClick(object sender, EventArgs e){
            dgv = (DataGridView)sender;
            GET_ColRow(dgv);

            if (mRow < 0) return;
            nOUT = GET_IONum(dgv, 2, mRow);
            sMassege = GET_TextOut(1, mRow);

            if (mRow == 3){
                if (dgv.Rows[mRow].Cells[mCol].Value.ToString() == "OK"){
                    dgv.Rows[mRow].Cells[mCol].Value = "NO";
                    DATA_.chkOUT[nOUT].Checked = false;
                }
                else{
                    dgv.Rows[mRow].Cells[mCol].Value = "OK";
                    DATA_.chkOUT[nOUT].Checked = false;
                }
            }

            if (mCol == 4) DATA_.mOUT[nOUT] = !DATA_.mOUT[nOUT];
            UTIL_.CLEAR_GRID_SELECTED(ref dgv);
        }
        private void Out_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_ColRow(dgv);
            if (mCol == 4){
                nOUT = GET_IONum(dgv, 2, mRow);
                DATA_.mOUT[nOUT] = !DATA_.mOUT[nOUT];
            }
        }

        private void In_DoubleClick(object sender, EventArgs e){
            dgv = (DataGridView)sender;
            GET_ColRow(dgv);

            if (mRow < 0) return;
            nINPUT = GET_IONum(dgv, 2, mRow);
            sMassege = GET_TextIn(1, mRow);
            if (sMassege.Length < 2) return;

            if (mCol == 3){
                if (dgv.Rows[mRow].Cells[mCol].Value.ToString() == "A"){
                    dgv.Rows[mRow].Cells[mCol].Value = "B";
                    DATA_.chkIN[nINPUT].ContactB = true;
                }
                else{
                    dgv.Rows[mRow].Cells[mCol].Value = "A";
                    DATA_.chkIN[nINPUT].ContactB = false;
                }
            }

            if (mCol == 4){
                if (dgv.Rows[mRow].Cells[mCol].Value.ToString() == "OK"){
                    gridIn.Rows[mRow].Cells[mCol].Value = "NO";
                    DATA_.chkIN[nINPUT].Checked = false;
                }
                else{
                    dgv.Rows[mRow].Cells[mCol].Value = "OK";
                    DATA_.chkIN[nINPUT].Checked = true;
                }
            }
            UTIL_.CLEAR_GRID_SELECTED(ref dgv);
        }

        private void Close_Click(object sender, EventArgs e){
            tmrIOCheck.Enabled = false;
            this.Hide();
        }

        private void TimerIOCheck_Tick(object sender, EventArgs e){
            tmrIOCheck.Enabled = false;
            for (int i = 0; i < CNT_.numInLast; i++){
                if (DATA_.chkIN[i].ContactB) SET_ColorIn(3, i, Color.Red);
                else SET_ColorIn(3, i, Color.LightSkyBlue);

                if (DATA_.chkIN[i].Checked) SET_ColorIn(4, i, Color.LightSkyBlue);
                else SET_ColorIn(4, i, Color.Silver);

                tNUM = GET_IONum(gridIn, 2, i);
                if (DATA_.mIN[tNUM]) SET_ColorIn(1, i, Color.Lime);
                else SET_ColorIn(1, i, Color.Silver);
            }

            for (int i = 0; i < CNT_.numOutLast; i++){
                if (DATA_.chkOUT[i].Checked) SET_ColorOut(3, i, Color.LightSkyBlue);
                else SET_ColorOut(3, i, Color.Red);

                tNUM = GET_IONum(gridOut, 2, i);
                if (DATA_.mOUT[tNUM]) SET_ColorOut(1, i, Color.Yellow);
                else SET_ColorOut(1, i, Color.Silver);
            }
            tmrIOCheck.Enabled = true;
        }
    }
}