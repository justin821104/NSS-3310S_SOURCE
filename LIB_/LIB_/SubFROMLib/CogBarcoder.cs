using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Object;

namespace LIB_.SubFROMLib{
    /// <summary>
    /// COGNEX BARCODE 
    /// DM262-728A6E
    /// TCP/IP
    /// BARCODE IP : 192.168.200.51
    /// PC IP : 192.168.200.50
    /// </summary>
    /// 
    public struct CommunicationType{
        public const bool GigaE = true;
        public const bool Serial = false;
    }

    public partial class CogBarcoder : Form{
        
        public ResultCollector _results;

        public SynchronizationContext _syncContext = null;
        public EthSystemDiscoverer _ethSystemDiscoverer = null;
        public SerSystemDiscoverer _serSystemDiscoverer = null;
        public ISystemConnector _connector = null;
        public DataManSystem _system = null;
        public object _currentResultInfoSyncLock = new object();
        public bool _closing = false;
        public bool _autoconnect = false;
        public object _listAddItemLock = new object();

        public bool bCommunication = (bool)CommunicationType.GigaE;
        public bool bAutoReconect = false;
        public bool bRunKeepAlive = false;
        public EthSystemDiscoverer.SystemInfo _GigeEInfo;  //GigaE 정보
        public SerSystemDiscoverer.SystemInfo _SerialInfo; //Serial Port 정보
        public string ReadResult = "";

        public bool bOpen = false;
        public bool bLiveOff = false;

        public CogBarcoder(){
            InitializeComponent();

            // The SDK may fire events from arbitrary thread context. Therefore if you want to change
            // the state of controls or windows from any of the SDK' events, you have to use this
            // synchronization context to execute the event handler code on the main GUI thread.
            _syncContext = WindowsFormsSynchronizationContext.Current;

            _ethSystemDiscoverer = new EthSystemDiscoverer(); // Create discoverers to discover ethernet systems.
            _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);  // Subscribe to the system discoved event.
            _ethSystemDiscoverer.Discover(); // Ask the discoverers to start discovering systems.

            _serSystemDiscoverer = new SerSystemDiscoverer(); // Create discoverers to discover serial port systems.
            _serSystemDiscoverer.SystemDiscovered += new SerSystemDiscoverer.SystemDiscoveredHandler(OnSerSystemDiscovered);  // Subscribe to the system discoved event.
            _serSystemDiscoverer.Discover(); // Ask the discoverers to start discovering systems.

            lblRETUNE.Text = "";

            cbLiveDisplay.CheckedChanged += new System.EventHandler(this.cbLiveDisplay_CheckedChanged);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CogBarCoder_FormClosing);

            btnConnect.Click        += (sender, e) => Conect();
            btnDisconnect.Click     += (sender, e) => Disconnect();
            btnTrigger.MouseDown    += (sender, e) => TriggerMouseDown();
            btnTrigger.MouseUp      += (sender, e) => TriggerMouseUp();
        }

        #region Device Discovery Events
        public void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo){
            _syncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        _GigeEInfo = systemInfo;
                    }),
                    null);
        }

        public void OnSerSystemDiscovered(SerSystemDiscoverer.SystemInfo systemInfo){
            _syncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        _SerialInfo = systemInfo;
                    }),
                    null);
        }
        #endregion

        private void CogBarcoder_Load(object sender, EventArgs e){
            _ethSystemDiscoverer = new EthSystemDiscoverer(); // Create discoverers to discover ethernet systems.
            _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);  // Subscribe to the system discoved event.
            _ethSystemDiscoverer.Discover(); // Ask the discoverers to start discovering systems.

            _serSystemDiscoverer = new SerSystemDiscoverer(); // Create discoverers to discover serial port systems.
            _serSystemDiscoverer.SystemDiscovered += new SerSystemDiscoverer.SystemDiscoveredHandler(OnSerSystemDiscovered);  // Subscribe to the system discoved event.
            _serSystemDiscoverer.Discover(); // Ask the discoverers to start discovering systems.
        }
        private void CogBarCoder_FormClosing(object sender, EventArgs e){
            _closing = true;
            _autoconnect = false;
            if (null != _system && _system.State == Cognex.DataMan.SDK.ConnectionState.Connected) _system.Disconnect();
        }

        #region Device Events
        private void OnSystemConnected(object sender, EventArgs args){
            _syncContext.Post(
                delegate{
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    btnTrigger.Enabled = true;
                    cbLiveDisplay.Enabled = true;
                    System.Diagnostics.Trace.WriteLine("System connected");
                },
                null);
        }
        private void OnSystemDisconnected(object sender, EventArgs args){
            _syncContext.Post(
                delegate{
                    System.Diagnostics.Trace.WriteLine("System disconnected");

                    if (!_closing && _autoconnect && bAutoReconect){
                        CogBarcoderReconnecting frm = new CogBarcoderReconnecting(this, _system);
                        if (frm.ShowDialog() == DialogResult.Cancel){
                            btnConnect.Enabled = true;
                            btnDisconnect.Enabled = false;
                            btnTrigger.Enabled = false;
                            cbLiveDisplay.Enabled = false;
                        }
                    }
                    else{
                        btnConnect.Enabled = true;
                        btnDisconnect.Enabled = false;
                        btnTrigger.Enabled = false;
                        cbLiveDisplay.Enabled = false;
                    }
                    bOpen = false;
                },
                null);
        }
        private void OnSystemWentOnline(object sender, EventArgs args){
            _syncContext.Post(
                delegate{
                    System.Diagnostics.Trace.WriteLine("System went online");
                },
                null);
        }
        private void OnSystemWentOffline(object sender, EventArgs args){
            _syncContext.Post(
                delegate{
                    System.Diagnostics.Trace.WriteLine("System went offline");
                },
                null);
        }
        private void OnKeepAliveResponseMissed(object sender, EventArgs args){
            _syncContext.Post(
                delegate{
                    System.Diagnostics.Trace.WriteLine("Keep-alive response missed");
                },
                null);
        }
        private void OnBinaryDataTransferProgress(object sender, BinaryDataTransferProgressEventArgs args){
            /*
			_syncContext.Post(
				delegate{
					toolStripProgressBar1.Value = (int)(100 * (args.BytesTransferred / (double)args.TotalDataSize));
				},
				null);
			*/
        }
        #endregion

        public void Conect(){
            if (_GigeEInfo == null && _SerialInfo == null) return;
            btnConnect.Enabled = false;

            try{
                if (bCommunication == (bool)CommunicationType.GigaE){
                    EthSystemDiscoverer.SystemInfo eth_system_info = _GigeEInfo as EthSystemDiscoverer.SystemInfo;
                    EthSystemConnector conn = new EthSystemConnector(eth_system_info.IPAddress, eth_system_info.Port);
                    conn.UserName = "admin";
                    conn.Password = "";
                    _connector = conn;
                } //GigaE
                else{
                    SerSystemDiscoverer.SystemInfo ser_system_info = _SerialInfo as SerSystemDiscoverer.SystemInfo;
                    SerSystemConnector conn = new SerSystemConnector(ser_system_info.PortName, ser_system_info.Baudrate);
                    _connector = conn;
                } //Serial Port

                _system = new DataManSystem(_connector);
                _system.DefaultTimeout = 5000;

                // Subscribe to events that are signalled when the system is connected / disconnected.
                _system.SystemConnected += new SystemConnectedHandler(OnSystemConnected);
                _system.SystemDisconnected += new SystemDisconnectedHandler(OnSystemDisconnected);
                _system.SystemWentOnline += new SystemWentOnlineHandler(OnSystemWentOnline);
                _system.SystemWentOffline += new SystemWentOfflineHandler(OnSystemWentOffline);
                _system.KeepAliveResponseMissed += new KeepAliveResponseMissedHandler(OnKeepAliveResponseMissed);
                _system.BinaryDataTransferProgress += new BinaryDataTransferProgressHandler(OnBinaryDataTransferProgress);

                // Subscribe to events that are signalled when the deveice sends auto-responses.
                ResultTypes requested_result_types = ResultTypes.ReadXml | ResultTypes.Image | ResultTypes.ImageGraphics;
                _results = new ResultCollector(_system, requested_result_types);
                _results.ComplexResultArrived += Results_ComplexResultArrived;
                _results.PartialResultDropped += Results_PartialResultDropped;

                _system.SetKeepAliveOptions(bRunKeepAlive, 3000, 1000);

                _system.Connect();

                _system.SetResultTypes(requested_result_types);

                bOpen = true;
            }
            catch (Exception ex){
                System.Diagnostics.Trace.WriteLine("Failed to connect : " + ex.ToString());
                btnConnect.Enabled = true;
            }
            _autoconnect = true;
        }
        void Results_ComplexResultArrived(object sender, ResultInfo e){
            _syncContext.Post(
                delegate{
                    ShowResult(e);
                },
                null);
        }
        void ReportDroppedResult(ResultInfo e){
            List<string> dropped = new List<string>();

            if (e.Image != null)            dropped.Add(String.Format("image (ResultId={0}, ImageId={1})", e.ResultId, e.ImageId));
            if (e.ImageGraphics != null)    dropped.Add(String.Format("graphics (ResultId={0}, ImageId={1})", e.ResultId, e.ImageId));
            if (e.ReadString != null)       dropped.Add(String.Format("read string (ResultId={0})", e.ResultId));
            if (e.XmlResult != null)        dropped.Add(String.Format("xml result (ResultId={0})", e.ResultId));

            System.Diagnostics.Trace.WriteLine("Partial results dropped: " + String.Join(", ", dropped.ToArray()));
        }
        void Results_PartialResultDropped(object sender, ResultInfo e){
            _syncContext.Post(
                delegate{
                    if (e.SubResults != null){
                        foreach (var sub_result in e.SubResults){
                            ReportDroppedResult(sub_result);
                        }
                    }
                    ReportDroppedResult(e);
                },
                null);
        }
        public void CleanupConnection(){
            if (null != _system){
                _system.SystemConnected             -= OnSystemConnected;
                _system.SystemDisconnected          -= OnSystemDisconnected;
                _system.SystemWentOnline            -= OnSystemWentOnline;
                _system.SystemWentOffline           -= OnSystemWentOffline;
                _system.KeepAliveResponseMissed     -= OnKeepAliveResponseMissed;
                _system.BinaryDataTransferProgress  -= OnBinaryDataTransferProgress;
            }
            _connector = null;
            _system = null;
        }
        public void OnLiveImageArrived(IAsyncResult result){
            try{
                Image image = _system.EndGetLiveImage(result);
                Size image_size = Gui.FitImageInControl(image.Size, picResultImage.Size);
                Image fitted_image = Gui.ResizeImageToBitmap(image, image_size);

                _syncContext.Post(
                    delegate{
                        picResultImage.Image = fitted_image;
                        picResultImage.Invalidate();
                    },
                null);
            }
            catch { }
            finally{
                if (cbLiveDisplay.Checked){
                    _system.BeginGetLiveImage(
                        ImageFormat.jpeg,
                        ImageSize.Sixteenth,
                        ImageQuality.Medium,
                        OnLiveImageArrived,
                        null);
                }
            }
        }
        public string GetReadStringFromResultXml(string resultXml){
            try{
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(resultXml);

                XmlNode full_string_node = doc.SelectSingleNode("result/general/full_string");

                if (full_string_node != null){
                    XmlAttribute encoding = full_string_node.Attributes["encoding"];
                    if (encoding != null && encoding.InnerText == "base64"){
                        byte[] code = Convert.FromBase64String(full_string_node.InnerText);
                        return _system.Encoding.GetString(code, 0, code.Length);
                    }
                    return full_string_node.InnerText;
                }
            }
            catch { }
            return "";
        }
        public void ShowResult(ResultInfo e){
            List<Image> images = new List<Image>();
            List<string> image_graphics = new List<string>();

            // Take a reference or copy values from the locked result info object. This is done
            // so that the lock is used only for a short period of time.
            lock (_currentResultInfoSyncLock){
                ReadResult = !String.IsNullOrEmpty(e.ReadString) ? e.ReadString : GetReadStringFromResultXml(e.XmlResult);

                if (e.Image != null) images.Add(e.Image);
                if (e.ImageGraphics != null) image_graphics.Add(e.ImageGraphics);
                if (e.SubResults != null){
                    foreach (var item in e.SubResults){
                        if (item.Image != null) images.Add(item.Image);
                        if (item.ImageGraphics != null) image_graphics.Add(item.ImageGraphics);
                    }
                }
            }
            System.Diagnostics.Trace.WriteLine("Complex result arrived : resultId = " + e.ResultId + ", read result = " + ReadResult);

            if (images.Count > 0){
                Image first_image = images[0];
                Size image_size = Gui.FitImageInControl(first_image.Size, picResultImage.Size);
                Image fitted_image = Gui.ResizeImageToBitmap(first_image, image_size);

                if (image_graphics.Count > 0){
                    using (Graphics g = Graphics.FromImage(fitted_image)){
                        foreach (var graphics in image_graphics){
                            ResultGraphics rg = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, image_size.Width, image_size.Height));
                            ResultGraphicsRenderer.PaintResults(g, rg);
                        }
                    }
                }

                if (picResultImage.Image != null) picResultImage.Image.Dispose();
                picResultImage.Image = fitted_image;
                picResultImage.Invalidate();
            }

            if (ReadResult != null){
                lblRETUNE.Text = ReadResult;
                DATA_.bWriteBarcode = true;
            }
        }

        public void Disconnect(){
            if (_system == null || _system.State != Cognex.DataMan.SDK.ConnectionState.Connected) return;

            cbLiveDisplay.Checked = false;
            btnDisconnect.Enabled = false;
            _autoconnect = false;
            _system.Disconnect();
            CleanupConnection();
            _results.ClearCachedResults();
            _results = null;

            bOpen = false;
        }

        private void chkDataMatrix_CheckedChanged(object sender, EventArgs e){
            //bool bOLD = !chkDataMatrix.Checked;
            try{
                if (chkDataMatrix.Checked) _system.SendCommand("SET SYMBOL.DATAMATRIX ON");
                else _system.SendCommand("SET SYMBOL.DATAMATRIX OFF");
            }
            catch (Exception ex){
                MessageBox.Show("Failed to send Data Matrix Setting command: " + ex.ToString());
                //chkDataMatrix.Checked = bOLD;
            }
        } //Data Matrix코드 Reading ON/OFF

        private void chkQR_CheckedChanged(object sender, EventArgs e){
            //bool bOLD = !chkQR.Checked;
            try{
                if (chkQR.Checked) _system.SendCommand("SET SYMBOL.QR ON");
                else _system.SendCommand("SET SYMBOL.QR OFF");
            }
            catch (Exception ex){
                MessageBox.Show("Failed to send QR Setting command: " + ex.ToString());
                //chkQR.Checked = bOLD;
            }
        } //QR코드 Reading ON/OFF

        private void lblRETUNE_DoubleClick(object sender, EventArgs e) { lblRETUNE.Text = ""; }

        public void cbLiveDisplay_CheckedChanged(object sender, EventArgs e){
            try{
                if (cbLiveDisplay.Checked){
                    btnTrigger.Enabled = false;
                    _system.SendCommand("SET LIVEIMG.MODE 2");
                    _system.BeginGetLiveImage(
                        ImageFormat.jpeg,
                        ImageSize.Sixteenth,
                        ImageQuality.Medium,
                        OnLiveImageArrived,
                        null);
                }
                else{
                    btnTrigger.Enabled = true;
                    _system.SendCommand("SET LIVEIMG.MODE 0");
                }
            }
            catch (Exception ex){
                MessageBox.Show("Failed to set live image mode: " + ex.ToString());
            }
        }

        public void Trigger(){
            if (cbLiveDisplay.Checked){
                cbLiveDisplay.Checked = false;
                cbLiveDisplay_CheckedChanged(cbLiveDisplay, EventArgs.Empty);
                UTIL_.DELAY(100);
            }

            try{
                ReadResult = "";
                DATA_.bWriteBarcode = false;
                _system.SendCommand("TRIGGER ON");
            }
            catch (Exception ex) { LogWR_.SaveLogException("Failed to send Trigger On command", ex); }
            UTIL_.DELAY(300);
            try{
                _system.SendCommand("TRIGGER OFF");
            }
            catch (Exception ex) { LogWR_.SaveLogException("Failed to send Trigger Off command", ex); }
            
        }

        public void TriggerMouseDown(){
            try{
                DATA_.bWriteBarcode = false;
                _system.SendCommand("TRIGGER ON");
            }
            catch (Exception ex){
                MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString());
            }
        }

        public void TriggerMouseUp(){
            try{
                _system.SendCommand("TRIGGER OFF");
            }
            catch (Exception ex){
                MessageBox.Show("Failed to send TRIGGER OFF command: " + ex.ToString());
            }
        }

        private void tmrBarcode_Tick(object sender, EventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || bLiveOff){
                cbLiveDisplay.Checked   = false;
                bLiveOff                = false;
            }
        }
    }
}