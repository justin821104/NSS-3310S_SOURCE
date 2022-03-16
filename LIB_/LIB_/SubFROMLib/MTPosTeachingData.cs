using Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MTPosTeachingData : Form{
        Label lb = null;
        public int numAxis = 0, numPos = 0;
        public double pos = 0, spd = 0, acl = 0, dcl = 0, dVelue = 0;
        public int movTime = 0, Delay = 0;
        public bool bFIRST = false;
        double dOffset = 0;
        double newvalue = 0;
        string sMassege = string.Empty;
        double dCMDPOS = 0;
        double dPitch = 0;

        public MTPosTeachingData(){
            InitializeComponent();
        }

        public void INI_(){
            lbMotorName.Text = "[ MOTOR ] " + DATA_.MtName[numAxis];
            lbPosName.Text = " [POSITION ] " + DATA_.PosName[numAxis, numPos];

            pos = DATA_.mtDATA[numAxis, numPos].Pos;
            spd = DATA_.mtDATA[numAxis, numPos].Spd;
            acl = DATA_.mtDATA[numAxis, numPos].Acc;
            dcl = DATA_.mtDATA[numAxis, numPos].Dec;
            movTime = DATA_.mtDATA[numAxis, numPos].MoveTime;
            Delay = DATA_.mtDATA[numAxis, numPos].Delay;

            lbPosition.Text = pos.ToString();
            lbSpeed.Text = spd.ToString();
            lbAccel.Text = Convert.ToString(acl);
            lbDeccel.Text = Convert.ToString(dcl);
            lbTime.Text = string.Format("{0:0}", movTime);
            lbDelay.Text = string.Format("{0:0}", Delay);

            editMinus.Text = string.Format("{0:0.000}", DATA_.mtTeachingLimit[numAxis, numPos].PosNLimit);
            editPlus.Text = string.Format("{0:0.000}", DATA_.mtTeachingLimit[numAxis, numPos].PosPLimit);
            cbEnable.Checked = DATA_.mtTeachingLimit[numAxis, numPos].Enable;

            if (!bFIRST){
                bFIRST = true;
                tmrMTPositionTeachingData.Enabled = true;
                Show();
            }
        }

        private void Apply_Click(object sender, EventArgs e){
            if (UTIL_.PRINT_MASSAGE("Do you want to change the teaching range setting value ?" + ETC.NewLine + "티칭 범위 설정값을 변경 하시겠습니까 ?", false, false, false)) return;

            DATA_.mtTeachingLimit[numAxis, numPos].PosNLimit = double.Parse(editMinus.Text);
            DATA_.mtTeachingLimit[numAxis, numPos].PosPLimit = double.Parse(editPlus.Text);
            DATA_.mtTeachingLimit[numAxis, numPos].Enable = cbEnable.Checked;
            TEACH_.WR_TeachingLimit(numAxis, numPos);
        }

        private void RMove_Click(object sender, EventArgs e){
            dPitch = double.Parse(edit_RMove.Text);
            if (Math.Abs(dPitch) > 100){
                UTIL_.PRINT_MASSAGE("NOT MORE THAN ± 100mm !" + ETC.CrLf + "거리 값이 너무 큽니다. (± 100mm 이상 설정 불가)", false, false, true);
                return;
            }
            if (LAB_.MTBUSY(numAxis)) return;

            dCMDPOS = LAB_.GET_CMDPOS(numAxis);
            dPitch = dCMDPOS + dPitch;
            uint uRTM = LAB_.MT_STARTMOVE(numAxis, dPitch, 50);
            if (uRTM != 0){
                LAB_.MTSSTOP(numAxis, "fMTPosTeachingData -> swRMove = FAIL !");
                UTIL_.PRINT_MASSAGE("PITCH MOVING FAIL !" + ETC.NewLine + "정상 이동 하지 못하였습니다.", false, true, true);
            }
        }

        private void Info_Click(object sender, EventArgs e){
            //545, 220 -> 545, 157 // 545, 200 -> 545, 147
            if (Height == 157){
                if (DATA_.eLoginLevel < eLogLevel.ADMIN) return;
                Height = 200;
            }
            else Height = 147;
            Width = 545;
        }

        private void Close_Click(object sender, EventArgs e){
            bFIRST = false;
            tmrMTPositionTeachingData.Enabled = false;
            this.Hide();
        }

        private void MTPosTeachingData_Load(object sender, EventArgs e){
            //545, 220 -> 545, 157 // 545, 200 -> 545, 147
            Width = 545;
            Height = 147;
            bFIRST = false;
        }

        private void TimerMTPositionTeachingData_Tick(object sender, EventArgs e){
            Top = SUBFRM_.gMTPosTeching.Top + SUBFRM_.gMTPosTeching.Height;
            Left = SUBFRM_.gMTPosTeching.Left + 7;
            if (DATA_.eLoginLevel <= eLogLevel.ADMIN) panRange.Enabled = true;
            else panRange.Enabled = false;
        }

        private void Save_Click(object sender, EventArgs e){
            if (!UTIL_.PRINT_MASSAGE("CHANGE SETTING MOTION DATA?" + ETC.NewLine + "모터 설정값을 변경 하시겠습니까 ?", false, false, false)) return;
            SAVE_MTDATA();
            //SetForegroundWindow(cDEF.fMain.Handle);
        }

        private void CurrentSet_Click(object sender, EventArgs e){
            if (!DATA_.mtSTS[numAxis].bHomeComplete){
                UTIL_.PRINT_MASSAGE("MOTOR IS NOT HOME" + ETC.NewLine + "원점 작업이 안되어 있습니다 !", false, false, false);
                return;
            }
            if (!UTIL_.PRINT_MASSAGE("SAVE CURRENT POSITION ?" + ETC.NewLine + "현 위치로 티칭 하시겠습니까 ?", false, false, false)) return;
            pos = LAB_.GET_ACTPOS(numAxis);
            if ((pos > DATA_.mtTeachingLimit[numAxis, numPos].PosPLimit || pos < DATA_.mtTeachingLimit[numAxis, numPos].PosNLimit) && DATA_.mtTeachingLimit[numAxis, numPos].Enable){
                UTIL_.PRINT_MASSAGE("Position setting is beyond the setting range." + ETC.NewLine + "설정범위를 초과한 티칭입니다.", true, true, true);
                return;
            }
            lbPosition.Text = pos.ToString();
            SAVE_MTDATA();
        }

        private void OffsetTeach_Click(object sender, EventArgs e){
            if (!UTIL_.PRINT_MASSAGE("CHANGE SETTING POSITION ?" + ETC.NewLine + "현 위치값을 변경 하시겠습니까 ?", false, false, false)) return;
            dOffset = double.Parse(editOffset.Text);
            pos = DATA_.mtDATA[numAxis, numPos].Pos + dOffset;
            lbPosition.Text = pos.ToString();
            SAVE_MTDATA();
        }

        private void MT_Data_Click(object sender, EventArgs e){
            lb = (Label)sender;
            lb.BackColor = Color.YellowGreen;
            newvalue = 0;
            sMassege = string.Empty;
            try{
                switch (lb.Name){
                    case "lbPosition":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].Pos, true);
                        if ((newvalue > DATA_.mtTeachingLimit[numAxis, numPos].PosPLimit || newvalue < DATA_.mtTeachingLimit[numAxis, numPos].PosNLimit) && DATA_.mtTeachingLimit[numAxis, numPos].Enable){
                            UTIL_.PRINT_MASSAGE("Position setting is beyond the setting range." + ETC.NewLine + "설정범위를 초과한 티칭입니다.", true, true, true);
                            return;
                        }
                        pos = newvalue;
                        sMassege = pos.ToString();
                        break;
                    case "lbSpeed":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].Spd, false);
                        spd = newvalue;
                        sMassege = spd.ToString();
                        break;
                    case "lbAccel":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].Acc, false);
                        acl = newvalue;
                        sMassege = acl.ToString();
                        break;
                    case "lbDeccel":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].Dec, false);
                        dcl = newvalue;
                        sMassege = dcl.ToString();
                        break;
                    case "lbTime":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].MoveTime, false);
                        movTime = (int)newvalue;
                        sMassege = movTime.ToString();
                        break;
                    case "lbDelay":
                        newvalue = UTIL_.OPEN_KEYPAD(DATA_.PosName[numAxis, numPos], DATA_.mtDATA[numAxis, numPos].Delay, false);
                        Delay = (int)newvalue;
                        sMassege = Delay.ToString();
                        break;
                }
                lb.BackColor = Color.White;
                lb.Text = sMassege;
            }
            catch (Exception ex){
                MessageBox.Show("fMTPosTeachingData -> " + lb.Name + ETC.NewLine + ex.ToString());
                lb.BackColor = Color.White;
            }
        }

        void SAVE_MTDATA(){
            DATA_.mtOLD = DATA_.mtDATA[numAxis, numPos]; //변경 전 데이터
            DATA_.mtDATA[numAxis, numPos].Pos = pos;
            DATA_.mtDATA[numAxis, numPos].Spd = spd;
            DATA_.mtDATA[numAxis, numPos].Acc = acl;
            DATA_.mtDATA[numAxis, numPos].Dec = dcl;
            DATA_.mtDATA[numAxis, numPos].MoveTime = movTime;
            DATA_.mtDATA[numAxis, numPos].Delay = Delay;

            DATA_.mtSAVE = DATA_.mtDATA[numAxis, numPos];   // 변경 후 데이터
            TEACH_.WR_MTDATA(numAxis, numPos);
        }
    }
}