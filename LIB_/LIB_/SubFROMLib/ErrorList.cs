using Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class ErrorList : Form{
        public string[] sErrKind = { "M/C", "MAN", "PRODUCT", "METHOD" };
        public string[] sResetKind = { "OP", "ENGNEER", "MASTER", "SOFT" };
        int mCol, mRow = 0;
        DataGridView dgv = null;
        string ErrNAME = string.Empty;

        public ErrorList(){
            InitializeComponent();

            #region "CONTROL EVENT"
            swClose.Click += (sender, e) => { CLOSE(); };
            swSave.Click += (sender, e) => { SAVE(); };
            #endregion "CONTROL EVENT"
        }

        public void RD_ERRORLIST(){
            INITIAL_GRID();
            ShowDialog();
        }

        public void CLOSE() { Hide(); }

        public void GET_ColRow(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void INITIAL_GRID(){
            gridError.RowCount = CNT_.ERR;
            for (int i = 0; i < CNT_.ERR; i++){
                gridError.Rows[i].Cells[0].Value = i.ToString();
                gridError.Rows[i].Cells[1].Value = DATA_.ErrName[i];
                gridError.Rows[i].Cells[2].Value = sErrKind[DATA_.ErrINFO[i].kind];
                gridError.Rows[i].Cells[3].Value = DATA_.ErrINFO[i].rstLevel.ToString();
                gridError.Rows[i].Cells[4].Value = DATA_.ErrINFO[i].enRec.ToString();
            }
        }

        public void SAVE(){
            if (!UTIL_.PRINT_MASSAGE("DO YOU WANT TO SAVE THE ERROR NAME ?" + ETC.CrLf + "에러명칭을 저장하시겠습니까 ?", false, false, false)) return;
            for (int i = 0; i < CNT_.ERR; i++){
                if (gridError.Rows[i].Cells[1].Value == null) continue;
                ErrNAME = gridError.Rows[i].Cells[1].Value.ToString();
                if (ErrNAME == null || ErrNAME == "" || ErrNAME == "NO DEFINE") continue;

                if (gridError.Rows[i].Cells[2].Value.ToString() == sErrKind[0]) DATA_.ErrINFO[i].kind = 0;
                if (gridError.Rows[i].Cells[2].Value.ToString() == sErrKind[1]) DATA_.ErrINFO[i].kind = 1;
                if (gridError.Rows[i].Cells[2].Value.ToString() == sErrKind[2]) DATA_.ErrINFO[i].kind = 2;
                if (gridError.Rows[i].Cells[2].Value.ToString() == sErrKind[3]) DATA_.ErrINFO[i].kind = 3;

                if (gridError.Rows[i].Cells[3].Value.ToString() == sResetKind[0]) DATA_.ErrINFO[i].rstLevel = eLogLevel.OP;
                if (gridError.Rows[i].Cells[3].Value.ToString() == sResetKind[1]) DATA_.ErrINFO[i].rstLevel = eLogLevel.ENG;
                if (gridError.Rows[i].Cells[3].Value.ToString() == sResetKind[2]) DATA_.ErrINFO[i].rstLevel = eLogLevel.ADMIN;
                if (gridError.Rows[i].Cells[3].Value.ToString() == sResetKind[3]) DATA_.ErrINFO[i].rstLevel = eLogLevel.SOFT;

                DATA_.ErrINFO[i].enRec = Convert.ToBoolean(gridError.Rows[i].Cells[4].Value.ToString());

                // 파일저장
            }
        }

        public string INDEX_KIND(string name){
            if (name == sErrKind[0]) return sErrKind[1];
            if (name == sErrKind[1]) return sErrKind[2];
            if (name == sErrKind[2]) return sErrKind[3];
            if (name == sErrKind[3]) return sErrKind[0];
            return "NULL";
        }
        public string INDEX_LEVEL(string name){
            if (name == sResetKind[0]) return sResetKind[1];
            if (name == sResetKind[1]) return sResetKind[2];
            if (name == sResetKind[2]) return sResetKind[3];
            if (name == sResetKind[3]) return sResetKind[0];
            return "NULL";
        }

        private void CsvFileSave_Click(object sender, EventArgs e){
            if (SD.ShowDialog() == DialogResult.Cancel) return;
            string sPath = SD.FileName;
            dgv = gridError;
            if (dgv == null || dgv.RowCount == 0) return;
            using (StreamWriter writer = new StreamWriter(sPath, false, /*Encoding.GetEncoding("euc - kr""shift_jis")) */ Encoding.UTF8)){
                int rowCount = dgv.Rows.Count;
                if (dgv.AllowUserToAddRows == true) rowCount = rowCount - 1;

                for (int i = 0; i < rowCount; i++){
                    List<string> strList = new List<string>();
                    for (int j = 0; j < dgv.Columns.Count; j++){
                        if (dgv[j, i].Value == null) continue;
                        strList.Add(dgv[j, i].Value.ToString());
                    }
                    string[] strArray = strList.ToArray();
                    string strCsvData = string.Join(",", strArray);
                    writer.WriteLine(strCsvData);
                }
                writer.Close();
            }
        }

        private void Error_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_ColRow(dgv);

            if (mRow < 0) return;
            if (mCol == 2){
                string s = dgv.Rows[mRow].Cells[mCol].Value.ToString();
                dgv.Rows[mRow].Cells[mCol].Value = INDEX_KIND(s);
            }
            if (mCol == 3){
                string s = dgv.Rows[mRow].Cells[mCol].Value.ToString();
                dgv.Rows[mRow].Cells[mCol].Value = INDEX_LEVEL(s);
            }
            if (mCol == 4){
                if (dgv.Rows[mRow].Cells[mCol].Value.ToString() == "False") dgv.Rows[mRow].Cells[mCol].Value = "True";
                else dgv.Rows[mRow].Cells[mCol].Value = "False";
            }
        }
    }
}