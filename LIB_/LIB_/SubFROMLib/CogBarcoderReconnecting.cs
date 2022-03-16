using Cognex.DataMan.SDK;
using System;
using System.Threading;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class CogBarcoderReconnecting : Form{
        private Form _parent = null;
        private DataManSystem _system = null;
        private SynchronizationContext _syncContext = null;
        private Thread _thread = null;
        private bool _cancel = false;

        public CogBarcoderReconnecting(Form parent, DataManSystem system){
            _parent = parent;
            _system = system;
            _syncContext = WindowsFormsSynchronizationContext.Current;

            InitializeComponent();
        }

        private void CogBarcoderReconnecting_Load(object sender, EventArgs e){
            _thread = new Thread(ReconnectThread);
            _thread.Name = "frmReconnecting.ReconnectThread";
            _thread.Start();
        }
        private void ReconnectThread(){
            while (!_cancel){
                try{
                    _system.Connect();
                }
                catch{
                    Thread.Sleep(500);
                    continue;
                }

                _syncContext.Post(
                    delegate{
                        DialogResult = DialogResult.OK;
                        Close();
                    },
                    null);

                break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e){
            DialogResult = DialogResult.Cancel;
            _cancel = true;
        }
    }
}