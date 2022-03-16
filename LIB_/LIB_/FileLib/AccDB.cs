using System;
using System.Data.OleDb;

public class AccDB_ : DATA_
{
    public delegate void DeleAddException(string strErr);
    public event DeleAddException DeleException = null;
    private OleDbConnection m_ObjCon;
    private bool m_bConn = false;

    public AccDB_(){
        m_ObjCon = new OleDbConnection{
            ConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=ACCDBTEST.accdb;"
        };
    }

    public AccDB_(string path){
        m_ObjCon = new OleDbConnection{
            ConnectionString = string.Format("Provider=Microsoft.Ace.OLEDB.12.0;Data Source={0};", path)
        };
    }

    public bool OpenDB(){
        bool bConn;
        try{
            m_ObjCon.Open();
            bConn = true;
        }
        catch (Exception ex){
            bConn = false;
            //if (DeleException != null)
            //    DeleException("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
            DeleException?.Invoke("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
        }
        m_bConn = bConn;
        return bConn;
    }

    public bool CloseDB(){
        bool bClose;
        try{
            m_ObjCon.Close();
            bClose = true;
            m_bConn = false;
        }
        catch{
            bClose = false;
        }
        return bClose;
    }

    public bool CreateDB(){
        bool isCreate = false;
        try{
            Type objClassType = Type.GetTypeFromProgID("ADOX.Catalog");
            if (objClassType != null){
                object obj = Activator.CreateInstance(objClassType);

                obj.GetType().InvokeMember("Create", System.Reflection.BindingFlags.InvokeMethod, null, obj,
                    new object[] { "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + m_ObjCon.ConnectionString + ";" });

                isCreate = true;

                // Clean Up
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
        }
        catch (Exception ex){
            //if (DeleException != null)
            //    DeleException("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
            DeleException?.Invoke("Could not create database file: " + m_ObjCon.ConnectionString + "\n\n" + ex.Message);
        }
        return isCreate;
    }

    public OleDbConnection GetConObj(){
        return m_ObjCon;
    }

    public bool IsConn(){
        return m_bConn;
    }
}