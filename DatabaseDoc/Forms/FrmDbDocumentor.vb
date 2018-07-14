Imports System.ComponentModel

Public Class FrmDbDocumentor
    Private isSearching As Boolean = False
    Private sortColumn As Integer = -1

#Region "ListViewComparer for sorting"
    'From https://msdn.microsoft.com/en-us/library/ms996467.aspx
    'changing Parse() with TryParse() by Warayra
    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer
        Private order As SortOrder

        Public Sub New()
            col = 0
            order = SortOrder.Ascending
        End Sub

        Public Sub New(column As Integer, order As SortOrder)
            col = column
            Me.order = order
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer _
                        Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            ' Determine whether the type being compared is a date type.
            Try
                ' Parse the two objects passed as a parameter as a DateTime.
                Dim firstDate, secondDate As System.DateTime
                If System.DateTime.TryParse(CType(x, ListViewItem).SubItems(col).Text, firstDate) Then
                    System.DateTime.TryParse(CType(y, ListViewItem).SubItems(col).Text, secondDate)
                    ' Compare the two dates.
                    returnVal = DateTime.Compare(firstDate, secondDate)
                Else
                    ' If neither compared object has a valid date format, 
                    ' compare as a string.
                    ' Compare the two items as a string.
                    returnVal = [String].Compare(CType(x,
                              ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Compare error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
            ' Determine whether the sort order is descending.
            If order = SortOrder.Descending Then
                ' Invert the value returned by String.Compare.
                returnVal *= -1
            End If
            Return returnVal
        End Function
    End Class
#End Region

    Friend Sub DisableAll()
        gbSelection.Enabled = False
        LvComponents.Items.Clear()
        LvObjects.Items.Clear()
        TxtComponentDescription.Text = ""
        TxtObjectDescription.Text = ""
        For Each ctrl As Control In TableLayoutPanel.Controls
            ctrl.Enabled = False
        Next
    End Sub

    Friend Sub EnableAll()
        gbSelection.Enabled = True
        For Each ctrl As Control In TableLayoutPanel.Controls
            ctrl.Enabled = True
        Next
    End Sub

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
            Rset = Conx.Execute(SQL)
            If Not Rset.EOF Then
                If LvComponents.Columns.Count = 0 Then
                    For idxFld As Integer = 0 To (Rset.Fields.Count - 1)
                        LvComponents.Columns.Add(Rset.Fields(idxFld).Name)
                    Next
                End If
            End If
            Dim aFields(Rset.Fields.Count) As String
            Do While Not Rset.EOF
                For idxFld As Integer = 0 To (Rset.Fields.Count - 1)
                    aFields(idxFld) = Rset(idxFld).Value.ToString
                Next
                LvComponents.Items.Add(New ListViewItem(aFields))
                Rset.MoveNext()
            Loop
            Rset.Close()
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
            Rset = Conx.Execute(SQL)
            If Not Rset.EOF Then
                If LvObjects.Columns.Count = 0 Then
                    For idxFld As Integer = 0 To (Rset.Fields.Count - 1)
                        LvObjects.Columns.Add(Rset.Fields(idxFld).Name)
                    Next
                End If
            End If
            Dim aFields(Rset.Fields.Count) As String
            Do While Not Rset.EOF
                For idxFld As Integer = 0 To (Rset.Fields.Count - 1)
                    aFields(idxFld) = Rset(idxFld).Value.ToString
                Next
                LvObjects.Items.Add(New ListViewItem(aFields))
                Rset.MoveNext()
            Loop
            Rset.Close()
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
            Rset = Conx.Execute(SQL)
            Do While Not Rset.EOF
                cbDatabases.Items.Add(Rset("name").Value.ToString)
                Rset.MoveNext()
            Loop
            Rset.Close()
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
            Rset = Conx.Execute(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Execute error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbObjectType.SelectedIndexChanged
        If cbObjectType.SelectedIndex < 0 Then Exit Sub
        Call FillObjectsList()
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
            Conx.Execute(SQL)
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

        Cursor.Current = Cursors.AppStarting
        SQL = "EXEC [docum].[SpUpdDescriptions]"
        SQL &= " @ObjectName='" & LvObjects.SelectedItems(0).SubItems(0).Text & "'"
        SQL &= ",@SubObjectName='" & LvComponents.SelectedItems(0).SubItems(0).Text & "'"
        SQL &= ",@Description='" & TxtComponentDescription.Text & "'"
        Try
            Conx.Execute(SQL)
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
        Cursor.Current = Cursors.AppStarting
        If BtConnection.Text = "Connect" Then
            If Not OpenDBConnection(TxtServer.Text, TxtInstance.Text, "", TxtUser.Text, TxtPassword.Text) Then End
            BtConnection.Image = My.Resources.Disconnect_9957
            BtConnection.Text = "Disconnect"
            Call LoadDatabases()
        Else
            BtConnection.Image = My.Resources.AddConnection_477
            BtConnection.Text = "Connect"
            cbDatabases.Items.Clear()
            Conx.Close()
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub LvObjects_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LvObjects.ColumnClick
        ' Determine whether the column is the same as the last column clicked.
        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            LvObjects.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If LvObjects.Sorting = SortOrder.Ascending Then
                LvObjects.Sorting = SortOrder.Descending
            Else
                LvObjects.Sorting = SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        LvObjects.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        LvObjects.ListViewItemSorter = New ListViewItemComparer(e.Column, LvObjects.Sorting)
    End Sub
End Class
