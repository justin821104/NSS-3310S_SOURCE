using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    /// <summary>
    /// 201006 BES
    /// SYSTEM-K RF READER
    /// TCP/IP
    /// RF READER IP : 192.168.11.200 / PORT : 5000
    /// PC IP : 192.168.11.205
    /// </summary>
    /// 
    public partial class SystemK_RFReader : Form{
        public Socket socket;
        public Thread receiveThread;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        private string recvMsg = string.Empty;
        private char[] charValues = new char[20];
        private string recvStr = string.Empty;

        public bool bOpen = false;
        public string IP = "192.168.11.200";
        public string Port = "5000";
        public string ReadRFID = string.Empty;

        public SystemK_RFReader(){
            InitializeComponent();

            btnConnect.Click += (sender, e) => Conntect();
            btnDisconnect.Click += (sender, e) => DisConntect();
            BtnRead.Click += (sender, e) => Read();

            IP_ADD.Text = IP;
            PORT_NO.Text = Port;
        }

        public void LOG(string msg){
            this.Invoke(new Action(delegate (){
                tbResult.AppendText(string.Format("\r\n[{0}]{1}", DateTime.Now.ToString(), msg));
            }));
        }
        public void AppentText(string str){
            this.Invoke(new Action(delegate ()
            {
                tbResult.Clear();
                tbResult.AppendText(str);
            }));
        }

        public void ConnectCallback(IAsyncResult ar){
            try{
                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);

                //Receive();
            }
            catch (Exception e){
                LogWR_.SaveLogException("[RF READER] ConnectCallback Fail!", e);
            }
        }
        public void ReceiveCallback(IAsyncResult ar){
            try{
                Socket socket = (Socket)ar.AsyncState;
                int recvSize = socket.EndReceive(ar);
                byte[] receiveMsg = buffer;

                if (recvSize != 0){
                    if (recvStr.Length <= 20){
                        for (int i = 0; i < recvSize; i++){
                            recvStr += Convert.ToChar(receiveMsg[i]);
                        }

                        if (recvStr.Length == 20){
                            recvStr = recvStr.Substring(2, 12);
                            AppentText(recvStr);
                            LOG("SUCESS : " + recvStr);
                            ReadRFID = recvStr;
                            recvStr = string.Empty;
                        }
                    }
                }
                socket.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, socket);
            }
            catch (Exception e){
                LogWR_.SaveLogException("[RF READER] ReceiveCallback Fail!", e);
            }
        }

        public void Conntect(){
            IPAddress ipaddress = IPAddress.Parse(IP);
            IPEndPoint endPoint = new IPEndPoint(ipaddress, int.Parse(Port));

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            LOG("conntecting...");
            socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
            IsConntect(true);
            LOG("Conntect!");
        }
        public void DisConntect(){
            socket.Close();
            IsConntect(false);
            LOG("DisConntect!");
        }

        public void IsConntect(bool bFlog)
        {
            btnConnect.Enabled = !bFlog;
            btnDisconnect.Enabled = bFlog;
            BtnRead.Enabled = bFlog;
            bOpen = bFlog;
        }

        private void tbResult_DoubleClick(object sender, EventArgs e) { tbResult.Clear(); }
        private byte[] ConvertByteArray(string strHex){
            int length = strHex.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2){
                bytes[i / 2] = Convert.ToByte(strHex.Substring(i, 2), 16);
            }
            return bytes;
        }
        public void Read(){
            string strHex = "4430202020202020202020202020202020203145";
            byte[] sendBytes = ConvertByteArray(strHex);
            Send(sendBytes);
            Receive();
        }

        public void Send(byte[] msg){
            try{
                if (socket != null && socket.Connected){
                    socket.Send(msg, 20, 0);//, msg.Length, SocketFlags.None);//BeginSend(msg, 0, msg.Length, 0, SendCallback, rfSocket);
                }
            }
            catch (Exception e){
                LogWR_.SaveLogException("[RF READER] Send Fail!", e);
            }
        }

        public void Receive(){
            try{
                socket.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, socket);
            }
            catch (Exception e){
                LogWR_.SaveLogException("[RF READER] Receive Fail!", e);
            }
        }
    }
}