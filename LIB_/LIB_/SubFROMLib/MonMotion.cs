using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MonMotion : Form{
        DataGridView dgv = null;
        int mCol, mRow = 0;
        int tNum = 0;
        public MonMotion(){
            InitializeComponent();
        }

        void GET_COLROW(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void INI_(){
            gridMotor.RowCount = CNT_.MT;
            this.Width = 650;
            this.Height = 210;
            this.Show();
            tmrMonMotion.Enabled = true;
        }

        private void Motor_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            if (mCol != 0 || mRow < 0) return;
            UTIL_.OPEN_KEYPAD_GRID("INPUT MOTOR NUMBER", ref dgv, false);
        }

        private void Close_Click(object sender, EventArgs e){
            tmrMonMotion.Enabled = false;
            this.Hide();
        }

        private void TimerMonMotion_Tick(object sender, EventArgs e){
            for (int i = 0; i < gridMotor.RowCount; i++){
                if (gridMotor[0, i].Value == null || gridMotor[0, i].Value.ToString() == "") continue;
                tNum = Convert.ToInt16(gridMotor[0, i].Value);
                if (tNum >= CNT_.MT || tNum < 0) continue;

                gridMotor[1, i].Value = DATA_.MtName[tNum];
                gridMotor[2, i].Value = DATA_.mtSTS[tNum].CurrentPosition.ToString();
                gridMotor[3, i].Value = DATA_.mtSTS[tNum].bSensorHome.ToString();
                gridMotor[4, i].Value = DATA_.mtSTS[tNum].bBusy.ToString();
                gridMotor[5, i].Value = DATA_.mtSTS[tNum].CurrentPosition.ToString();
                gridMotor[6, i].Value = DATA_.mtSTS[tNum].bAlram.ToString();

                if (DATA_.mtSTS[tNum].bSensorCW && DATA_.mtSTS[tNum].bSensorCCW) gridMotor[7, i].Value = "+/-";
                else if (DATA_.mtSTS[tNum].bSensorCW && !DATA_.mtSTS[tNum].bSensorCCW) gridMotor[7, i].Value = "+";
                else if (!DATA_.mtSTS[tNum].bSensorCW && DATA_.mtSTS[tNum].bSensorCCW) gridMotor[7, i].Value = "-";
                else gridMotor[7, i].Value = "OK";
            }
        }
    }
}