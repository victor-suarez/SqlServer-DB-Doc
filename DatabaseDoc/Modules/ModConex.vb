Module ModConex
    Friend conx As ADODB.Connection
    Friend rset As ADODB.Recordset
    Friend SQL As String

    Friend Function OpenDBConnection(Optional Server As String = "localhost", Optional Instance As String = "", Optional User As String = "sa", Optional Password As String = "password") As Boolean
        Dim strConn As String = ""
        strConn = "Provider=SQLOLEDB"
        strConn = strConn & ";Server=" & Server & If(Instance = "", Instance, "\" & Instance)
        strConn = strConn & ";Database=master"
        strConn = strConn & ";Uid=" & User
        strConn = strConn & ";Pwd=" & Password
        strConn = strConn & ";Encrypt=yes"
        strConn = strConn & ";TrustServerCertificate=true;"
        conx = New ADODB.Connection With {
            .ConnectionString = strConn
        }
        Try
            conx.Open()
            Call SetLastConnectionStr(Server, Instance, User, Password)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Conexion error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    Private Sub SetLastConnectionStr(Server As String, Instance As String, User As String, Password As String)
        My.Settings.Server = Server
        My.Settings.Instance = Instance
        My.Settings.User = User
        My.Settings.Password = Password
        My.Settings.Save()
    End Sub
    Sub Main()
        If Not OpenDBConnection() Then End
        FrmDbDocumentor.Show()
    End Sub
End Module
