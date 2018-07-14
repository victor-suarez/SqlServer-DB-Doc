Imports ADODB

Module ModConex
    Friend WithEvents Conx As ADODB.Connection
    Friend Rset As ADODB.Recordset
    Friend SQL As String
    Friend isConnected As Boolean = False

    Friend Function OpenDBConnection(Optional Server As String = "localhost", Optional Instance As String = "", Optional Database As String = "", Optional User As String = "sa", Optional Password As String = "password") As Boolean
        Dim strConn As String = ""
        strConn = "Provider=SQLOLEDB"
        strConn = strConn & ";Server=" & Server & If(Instance = "", Instance, "\" & Instance)
        strConn = strConn & ";Database=" & Database
        strConn = strConn & ";Uid=" & User
        strConn = strConn & ";Pwd=" & Password
        strConn = strConn & ";Encrypt=yes"
        strConn = strConn & ";TrustServerCertificate=true;"
        Conx = New ADODB.Connection With {
            .ConnectionString = strConn
        }
        Try
            Conx.Open()
            Call SaveLastConnectionStr(Server, Instance, Database, User, Password)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Conexion error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    Private Sub SaveLastConnectionStr(Server As String, Instance As String, Database As String, User As String, Password As String)
        My.Settings.Server = Server
        My.Settings.Instance = Instance
        My.Settings.Database = Database
        My.Settings.User = User
        My.Settings.Password = Password
        My.Settings.Save()
    End Sub

    Private Sub Conx_ConnectComplete(pError As [Error], ByRef adStatus As EventStatusEnum, pConnection As Connection) Handles Conx.ConnectComplete
        Call FrmDbDocumentor.EnableAll()
    End Sub

    Private Sub Conx_Disconnect(ByRef adStatus As EventStatusEnum, pConnection As Connection) Handles Conx.Disconnect
        Call FrmDbDocumentor.DisableAll()
    End Sub
End Module
