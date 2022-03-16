using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MonIO : Form{
        int mCol, mRow = 0;
        DataGridView dgv = null;
        int nNUM = 0;
        int tNUM = 0;
        string sMassege = string.Empty;

        public MonIO(){
            InitializeComponent();
        }

        void GET_COLROW(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void INI_(){
            Width = 450;
            Height = 170;
            tmrMonIO.Enabled = true;
            Show();
        }

        private void MonIO_Load(object sender, EventArgs e){
            gridIO.RowCount = 5;
            for (int i = 0; i < gridIO.RowCount; i++) gridIO[0, i].Value = "IN";
        }

        void REFRESH_(DataGridView g){
            for (int i = 0; i < g.RowCount; i++){
                if (g[1, i].Value == null) continue;
                try{
                    nNUM = Convert.ToInt16(g[1, i].Value);
                    if ((string)g[0, i].Value == "IN") g[2, i].Value = DATA_.InputName[nNUM];
                    else g[2, i].Value = DATA_.OutputName[nNUM];
                }
                catch (Exception ep) { System.Diagnostics.Trace.WriteLine(ep); }
            }
        }
        private void IO_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);

            if (mRow < 0 || mCol == 2) return;
            if (mCol == 0){
                if ((string)dgv.CurrentCell.Value == "IN") dgv.CurrentCell.Value = "OUT";
                else dgv.CurrentCell.Value = "IN";
            }
            if (mCol == 1){
                sMassege = "INPUT" + dgv[0, mRow].Value + " NUMBER";
                UTIL_.OPEN_KEYPAD_GRID(sMassege, ref dgv, false);
            }
            REFRESH_(dgv);
        }

        private void Close_Click(object sender, EventArgs e){
            tmrMonIO.Enabled = false;
            Hide();
        }

        private void TimerMonIO_Tick(object sender, EventArgs e){
            for (int i = 0; i < gridIO.RowCount; i++){
                if (gridIO[1, i].Value == null || gridIO[1, i].Value.ToString() == "") continue;
                tNUM = Convert.ToInt16(gridIO[1, i].Value);

                if ((string)gridIO[0, i].Value == "IN") gridIO[3, i].Value = Convert.ToString(DATA_.mIN[tNUM]);
                else gridIO[3, i].Value = Convert.ToSingle(DATA_.mOUT[tNUM]);
            }
        }
    }
}