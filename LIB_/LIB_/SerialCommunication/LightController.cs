using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;

namespace LightControler{
    /// <summary>
    /// 모리
    /// LIGHT CONTROLLER
    /// SIRIUS-2R-304-12V
    /// REMOTE TYPE
    /// 2CH
    /// </summary>
    public partial class Sirius_2R : Component{
        public SerialPort sp = null;
        string Port = string.Empty;
        public bool CONNECTION { get; private set; } = false;
        public string VALUE = string.Empty;
        public bool ReturnCheck = false;

        public Sirius_2R(){
            InitializeComponent();
        }

        public Sirius_2R(IContainer container){
            container.Add(this);

            InitializeComponent();
        }

        public void OPEN(Int16 nPort){
            Port = "COM" + (nPort).ToString();

            sp = new SerialPort(Port);
            if (sp.IsOpen) sp.Close();
            try{
                sp.PortName = Port;
                sp.BaudRate = 57600;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.None;
                sp.DataReceived += RECEIVE; //new SerialDataReceivedEventHandler(RECEIVE);
                sp.Open();
                CONNECTION = true;
            }
            catch (Exception ex){
                Trace.WriteLine(ex.Message);
                return;
            }
        }
        public void CLOSE(){
            if (!sp.IsOpen) return;
            sp.DataReceived -= RECEIVE;
            sp.Close();
        }
        void Illuminator_DataReceived(object sender, SerialDataReceivedEventArgs e){
            SerialPort sp = (SerialPort)sender;

            int length = sp.BytesToRead;
            byte[] buf = new byte[length];
            sp.Read(buf, 0, length);

            string data = Encoding.ASCII.GetString(buf);
        }
        public void RECEIVE(object sender, SerialDataReceivedEventArgs args){
            try{
                if (sp.BytesToRead < 11) return;
                VALUE = sp.ReadLine();
                string[] sLine = VALUE.Split('\r');
                string[] Parsing = sLine[1].Split('=');
                VALUE = Parsing[1];
                ReturnCheck = true;
            }
            catch (Exception ex){
                Trace.WriteLine(ex.Message);
            }
        }

        public void SEND(string Message) { if (CONNECTION) sp.Write(Message); }

        public void SetLight(int nCh, double dValue){
            byte[] CR = new byte[1];
            string msg = string.Empty;

            if (dValue > 255) dValue = 255;
            CR[0] = ETC.CR;
            msg = "B" + (nCh + 1).ToString("0") + dValue.ToString("000") + "#"; //+ Encoding.ASCII.GetString(CR);
            //msg = "#CH" + (nCh + 1).ToString("00") + "BS0" + dValue.ToString("000") + "E";
            SEND(msg);
        }
    }
}