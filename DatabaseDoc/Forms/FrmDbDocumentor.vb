Imports System.ComponentModel

Public Class FrmDbDocumentor
    Private isSearching As Boolean = False

    Private Sub FillComponentsList()
        Dim lastItemIndex As Integer = -1
        Cursor.Current = Cursors.AppStarting
        If LvComponents.SelectedItems.Count > 0 Then
            lastItemIndex = LvComponents.SelectedItems(0).Index
        End If
        LvComponents.Items.Clear()
        LvComponents.Columns.Clear()
        TxtComponentDescription.Text = ""
        TxtObjectDescription.Text = LvObjects.SelectedItems(0).SubItems(3).Text
        SQL = "EXEC [docum].[SpConDescriptions]"
        SQL &= " @ObjectName='" & LvObjects.SelectedItems(0).SubItems(0).Text & "'"
        Try
            rset = conx.Execute(SQL)
            If Not rset.EOF Then
                If LvComponents.Columns.Count = 0 Then
                    For idxFld As Integer = 0 To (rset.Fields.Count - 1)
                        LvComponents.Columns.Add(rset.Fields(idxFld).Name)
                    Next
                End If
            End If
            Dim aFields(rset.Fields.Count) As String
            Do While Not rset.EOF
                For idxFld As Integer = 0 To (rset.Fields.Count - 1)
                    aFields(idxFld) = rset(idxFld).Value.ToString
                Next
                LvComponents.Items.Add(New ListViewItem(aFields))
                rset.MoveNext()
            Loop
            rset.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        LvComponents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        'Here is a bug! but I don't know which...
        If lastItemIndex > -1 AndAlso LvComponents.Items.Count >= lastItemIndex Then
            Try
                LvComponents.SelectedIndices.Add(lastItemIndex)
                LvComponents.SelectedItems(0).EnsureVisible()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End If
        lblComponents.Text = "Components: (" & LvComponents.Items.Count() & ")"
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub FillObjectsList()
        Dim lastItemIndex As Integer = -1
        Cursor.Current = Cursors.AppStarting
        If LvObjects.SelectedItems.Count > 0 Then
            lastItemIndex = LvObjects.SelectedItems(0).Index
        End If
        LvObjects.Items.Clear()
        LvObjects.Columns.Clear()
        TxtObjectDescription.Text = ""
        LvComponents.Items.Clear()
        TxtComponentDescription.Text = ""
        SQL = "EXEC [docum].[SpConDescriptions] '" & cbObjectType.SelectedItem.ToString & "'"
        'Select Case cbObjectType.SelectedItem.ToString
        '    Case "FUNCTION"
        '        SQL = "select * from sys.objects sf inner join sys.schemas ss on ss.schema_id=sf.schema_id where ss.name='dbo' and sf.type in ('FN','IF','TF') and sf.name not like 'fn[_]%' order by sf.name"
        '    Case "PROCEDURE"
        '        SQL = "EXEC [docum].[SpConDescriptions] 'procedure'"
        '    Case "TABLE"
        '        SQL = "EXEC [docum].[SpConDescriptions] 'table'"
        '    Case "VIEW"
        '        SQL = "select sv.name, sv.create_date, sv.modify_date from sys.views sv inner join sys.schemas ss on ss.schema_id=sv.schema_id where ss.name='dbo' order by sv.name"
        'End Select
        Try
            rset = conx.Execute(SQL)
            If Not rset.EOF Then
                If LvObjects.Columns.Count = 0 Then
                    For idxFld As Integer = 0 To (rset.Fields.Count - 1)
                        LvObjects.Columns.Add(rset.Fields(idxFld).Name)
                    Next
                End If
            End If
            Dim aFields(rset.Fields.Count) As String
            Do While Not rset.EOF
                For idxFld As Integer = 0 To (rset.Fields.Count - 1)
                    aFields(idxFld) = rset(idxFld).Value.ToString
                Next
                LvObjects.Items.Add(New ListViewItem(aFields))
                rset.MoveNext()
            Loop
            rset.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        LvObjects.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        'Here is a bug! but I don't know which...
        If lastItemIndex > -1 AndAlso LvObjects.Items.Count >= lastItemIndex Then
            LvObjects.SelectedIndices.Add(lastItemIndex)
            LvObjects.SelectedItems(0).EnsureVisible()
        End If
        lblObjects.Text = "Objects: (" & LvObjects.Items.Count() & ")"
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub LoadDatabases()
        Cursor.Current = Cursors.AppStarting
        SQL = "select name from sys.databases where name like 'VP%' or name = 'MONITOR' or name = 'PRU'"
        Try
            rset = conx.Execute(SQL)
            Do While Not rset.EOF
                cbDatabases.Items.Add(rset("name").Value.ToString)
                rset.MoveNext()
            Loop
            rset.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub SearchView(toSearch As String)
        isSearching = True
        For Each item As ListViewItem In LvObjects.Items
            If item.Text.StartsWith(toSearch) Then
                LvObjects.SelectedIndices.Add(item.Index)
                Exit For
            End If
        Next
        LvObjects.SelectedItems(0).EnsureVisible()
        isSearching = False
    End Sub

    Private Sub FrmDbDocumentor_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtServer.Text = My.Settings.Server
        TxtInstance.Text = My.Settings.Instance
        TxtUser.Text = My.Settings.User
        TxtPassword.Text = My.Settings.Password
    End Sub

    Private Sub CbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDatabases.SelectedIndexChanged
        Dim SQL As String = "USE [" & cbDatabases.SelectedItem.ToString & "];"
        Try
            rset = conx.Execute(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbObjectType.SelectedIndexChanged
        If cbObjectType.SelectedIndex < 0 Then Exit Sub
        Call FillObjectsList()
    End Sub

    Private Sub FrmDbDocumentor_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        conx.Close()
    End Sub

    Private Sub LvObjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvObjects.SelectedIndexChanged
        If LvObjects.SelectedIndices.Count = 0 Then Exit Sub
        Call FillComponentsList()
    End Sub

    Private Function ColExists(ColName As String) As Boolean
        Dim idxCol As Short, retVal As Boolean = False
        For idxCol = 0 To CShort(LvComponents.Columns.Count - 1)
            If LvComponents.Columns(idxCol).Name = ColName Then
                retVal = True
                Exit For
            End If
        Next
        Return retVal
    End Function

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        If TxtSearch.Text = "" Then Exit Sub
        Call SearchView(TxtSearch.Text)
    End Sub

    Private Sub LvObjects_MouseClick(sender As Object, e As MouseEventArgs) Handles LvObjects.MouseClick
        TxtSearch.Text = LvObjects.SelectedItems(0).SubItems(0).Text
    End Sub

    Private Sub BtUpdateObjDoc_Click(sender As Object, e As EventArgs) Handles BtUpdObjDesc.Click
        If cbDatabases.SelectedIndex < 0 Then Exit Sub
        If cbObjectType.SelectedIndex < 0 Then Exit Sub
        If LvObjects.SelectedItems.Count = 0 Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        SQL = "EXEC [docum].[SpUpdDescriptions]"
        SQL &= " @ObjectName='" & LvObjects.SelectedItems(0).SubItems(0).Text & "'"
        SQL &= ",@Description='" & TxtObjectDescription.Text & "'"
        Try
            conx.Execute(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        Call FillObjectsList()
    End Sub

    Private Sub BtUpdateCompDoc_Click(sender As Object, e As EventArgs) Handles BtUpdCompDesc.Click
        If cbDatabases.SelectedIndex < 0 Then Exit Sub
        If cbObjectType.SelectedIndex < 0 Then Exit Sub
        If LvObjects.SelectedItems.Count = 0 Then Exit Sub
        If LvComponents.SelectedItems.Count = 0 Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        SQL = "EXEC [docum].[SpUpdDescriptions]"
        SQL &= " @ObjectName='" & LvObjects.SelectedItems(0).SubItems(0).Text & "'"
        SQL &= ",@SubObjectName='" & LvComponents.SelectedItems(0).SubItems(0).Text & "'"
        SQL &= ",@Description='" & TxtComponentDescription.Text & "'"
        Try
            conx.Execute(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        Call FillComponentsList()
    End Sub

    Private Sub LvComponents_MouseClick(sender As Object, e As MouseEventArgs) Handles LvComponents.MouseClick
        TxtComponentDescription.Text = LvComponents.SelectedItems(0).SubItems(LvComponents.SelectedItems(0).SubItems.Count - 2).Text
    End Sub

    Private Sub FrmDbDocumentor_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        TableLayoutPanel.Width = Me.Width - 23
        TableLayoutPanel.Height = Me.Height - 156
    End Sub

    Private Sub BtConnection_Click(sender As Object, e As EventArgs) Handles BtConnection.Click
        If BtConnection.Text = "Connect" Then
            If Not OpenDBConnection(TxtServer.Text, TxtInstance.Text, TxtUser.Text, TxtPassword.Text) Then End
            BtConnection.Image = My.Resources.DataConnection_Connected_1061
            BtConnection.Text = "Disconnect"
            Call LoadDatabases()
        Else
            BtConnection.Image = My.Resources.DataConnection_NotConnected_1059
            BtConnection.Text = "Connect"
            cbDatabases.Items.Clear()
            conx.Close()
        End If
    End Sub
End Class
