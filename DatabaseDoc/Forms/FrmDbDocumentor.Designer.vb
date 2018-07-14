<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDbDocumentor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbDatabases = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbObjectType = New System.Windows.Forms.ComboBox()
        Me.LvObjects = New System.Windows.Forms.ListView()
        Me.colObjName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colObjCreateDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colObjModifyDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colObjDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TxtObjectDescription = New System.Windows.Forms.TextBox()
        Me.LvComponents = New System.Windows.Forms.ListView()
        Me.colCompName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCompDataType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCompDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TxtComponentDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.BtUpdObjDesc = New System.Windows.Forms.Button()
        Me.BtUpdCompDesc = New System.Windows.Forms.Button()
        Me.lblObjects = New System.Windows.Forms.Label()
        Me.lblComponents = New System.Windows.Forms.Label()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtServer = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtInstance = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.gbSelection = New System.Windows.Forms.GroupBox()
        Me.gbConnection = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtUser = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.BtConnection = New System.Windows.Forms.Button()
        Me.TableLayoutPanel.SuspendLayout()
        Me.gbSelection.SuspendLayout()
        Me.gbConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Database:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbDatabases
        '
        Me.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDatabases.FormattingEnabled = True
        Me.cbDatabases.Location = New System.Drawing.Point(84, 14)
        Me.cbDatabases.Name = "cbDatabases"
        Me.cbDatabases.Size = New System.Drawing.Size(185, 21)
        Me.cbDatabases.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Objects type:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbObjectType
        '
        Me.cbObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbObjectType.FormattingEnabled = True
        Me.cbObjectType.Items.AddRange(New Object() {"FUNCTION", "PROCEDURE", "TABLE", "VIEW"})
        Me.cbObjectType.Location = New System.Drawing.Point(350, 14)
        Me.cbObjectType.Name = "cbObjectType"
        Me.cbObjectType.Size = New System.Drawing.Size(185, 21)
        Me.cbObjectType.TabIndex = 3
        '
        'LvObjects
        '
        Me.LvObjects.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colObjName, Me.colObjCreateDate, Me.colObjModifyDate, Me.colObjDescription})
        Me.LvObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvObjects.FullRowSelect = True
        Me.LvObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.LvObjects.HideSelection = False
        Me.LvObjects.Location = New System.Drawing.Point(3, 23)
        Me.LvObjects.MultiSelect = False
        Me.LvObjects.Name = "LvObjects"
        Me.TableLayoutPanel.SetRowSpan(Me.LvObjects, 2)
        Me.LvObjects.Size = New System.Drawing.Size(586, 220)
        Me.LvObjects.TabIndex = 1
        Me.LvObjects.UseCompatibleStateImageBehavior = False
        Me.LvObjects.View = System.Windows.Forms.View.Details
        '
        'colObjName
        '
        Me.colObjName.Text = "Name"
        Me.colObjName.Width = 122
        '
        'colObjCreateDate
        '
        Me.colObjCreateDate.Text = "Create date"
        Me.colObjCreateDate.Width = 137
        '
        'colObjModifyDate
        '
        Me.colObjModifyDate.Text = "Modify date"
        Me.colObjModifyDate.Width = 154
        '
        'colObjDescription
        '
        Me.colObjDescription.Text = "Description"
        Me.colObjDescription.Width = 200
        '
        'TxtObjectDescription
        '
        Me.TxtObjectDescription.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtObjectDescription.Location = New System.Drawing.Point(595, 23)
        Me.TxtObjectDescription.MaxLength = 7500
        Me.TxtObjectDescription.Multiline = True
        Me.TxtObjectDescription.Name = "TxtObjectDescription"
        Me.TxtObjectDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtObjectDescription.Size = New System.Drawing.Size(316, 100)
        Me.TxtObjectDescription.TabIndex = 2
        '
        'LvComponents
        '
        Me.LvComponents.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCompName, Me.colCompDataType, Me.colCompDescription})
        Me.LvComponents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvComponents.FullRowSelect = True
        Me.LvComponents.HideSelection = False
        Me.LvComponents.Location = New System.Drawing.Point(3, 269)
        Me.LvComponents.MultiSelect = False
        Me.LvComponents.Name = "LvComponents"
        Me.TableLayoutPanel.SetRowSpan(Me.LvComponents, 2)
        Me.LvComponents.Size = New System.Drawing.Size(586, 220)
        Me.LvComponents.TabIndex = 5
        Me.LvComponents.UseCompatibleStateImageBehavior = False
        Me.LvComponents.View = System.Windows.Forms.View.Details
        '
        'colCompName
        '
        Me.colCompName.Text = "Name"
        Me.colCompName.Width = 122
        '
        'colCompDataType
        '
        Me.colCompDataType.Text = "Datatype"
        Me.colCompDataType.Width = 157
        '
        'colCompDescription
        '
        Me.colCompDescription.Text = "Description"
        Me.colCompDescription.Width = 154
        '
        'TxtComponentDescription
        '
        Me.TxtComponentDescription.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtComponentDescription.Location = New System.Drawing.Point(595, 269)
        Me.TxtComponentDescription.MaxLength = 7500
        Me.TxtComponentDescription.Multiline = True
        Me.TxtComponentDescription.Name = "TxtComponentDescription"
        Me.TxtComponentDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtComponentDescription.Size = New System.Drawing.Size(316, 100)
        Me.TxtComponentDescription.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Object search:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSearch
        '
        Me.TxtSearch.Location = New System.Drawing.Point(84, 41)
        Me.TxtSearch.MaxLength = 25
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(185, 20)
        Me.TxtSearch.TabIndex = 5
        '
        'BtUpdObjDesc
        '
        Me.BtUpdObjDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtUpdObjDesc.Location = New System.Drawing.Point(595, 219)
        Me.BtUpdObjDesc.Name = "BtUpdObjDesc"
        Me.BtUpdObjDesc.Size = New System.Drawing.Size(316, 24)
        Me.BtUpdObjDesc.TabIndex = 3
        Me.BtUpdObjDesc.Text = "Update Object Description"
        Me.BtUpdObjDesc.UseVisualStyleBackColor = True
        '
        'BtUpdCompDesc
        '
        Me.BtUpdCompDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtUpdCompDesc.Location = New System.Drawing.Point(595, 465)
        Me.BtUpdCompDesc.Name = "BtUpdCompDesc"
        Me.BtUpdCompDesc.Size = New System.Drawing.Size(316, 24)
        Me.BtUpdCompDesc.TabIndex = 7
        Me.BtUpdCompDesc.Text = "Update Component Description"
        Me.BtUpdCompDesc.UseVisualStyleBackColor = True
        '
        'lblObjects
        '
        Me.lblObjects.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblObjects.AutoSize = True
        Me.lblObjects.Location = New System.Drawing.Point(3, 3)
        Me.lblObjects.Name = "lblObjects"
        Me.lblObjects.Size = New System.Drawing.Size(46, 13)
        Me.lblObjects.TabIndex = 0
        Me.lblObjects.Text = "Objects:"
        Me.lblObjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComponents
        '
        Me.lblComponents.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblComponents.AutoSize = True
        Me.lblComponents.Location = New System.Drawing.Point(3, 249)
        Me.lblComponents.Name = "lblComponents"
        Me.lblComponents.Size = New System.Drawing.Size(69, 13)
        Me.lblComponents.TabIndex = 4
        Me.lblComponents.Text = "Components:"
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.85149!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.14851!))
        Me.TableLayoutPanel.Controls.Add(Me.lblObjects, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblComponents, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.LvObjects, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LvComponents, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.TxtObjectDescription, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.TxtComponentDescription, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.BtUpdObjDesc, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.BtUpdCompDesc, 1, 5)
        Me.TableLayoutPanel.Location = New System.Drawing.Point(4, 116)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 6
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(914, 492)
        Me.TableLayoutPanel.TabIndex = 6
        '
        'TxtServer
        '
        Me.TxtServer.Location = New System.Drawing.Point(52, 14)
        Me.TxtServer.MaxLength = 30
        Me.TxtServer.Name = "TxtServer"
        Me.TxtServer.Size = New System.Drawing.Size(96, 20)
        Me.TxtServer.TabIndex = 8
        Me.TxtServer.Text = "255.255.255.255"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Server:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInstance
        '
        Me.TxtInstance.Location = New System.Drawing.Point(211, 14)
        Me.TxtInstance.MaxLength = 30
        Me.TxtInstance.Name = "TxtInstance"
        Me.TxtInstance.Size = New System.Drawing.Size(96, 20)
        Me.TxtInstance.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(154, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Instance:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(313, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "(Blank for default)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbSelection
        '
        Me.gbSelection.Controls.Add(Me.cbDatabases)
        Me.gbSelection.Controls.Add(Me.Label3)
        Me.gbSelection.Controls.Add(Me.TxtSearch)
        Me.gbSelection.Controls.Add(Me.Label1)
        Me.gbSelection.Controls.Add(Me.Label2)
        Me.gbSelection.Controls.Add(Me.cbObjectType)
        Me.gbSelection.Location = New System.Drawing.Point(4, 45)
        Me.gbSelection.Name = "gbSelection"
        Me.gbSelection.Size = New System.Drawing.Size(914, 65)
        Me.gbSelection.TabIndex = 12
        Me.gbSelection.TabStop = False
        Me.gbSelection.Text = "Select:"
        '
        'gbConnection
        '
        Me.gbConnection.Controls.Add(Me.BtConnection)
        Me.gbConnection.Controls.Add(Me.Label8)
        Me.gbConnection.Controls.Add(Me.TxtPassword)
        Me.gbConnection.Controls.Add(Me.Label7)
        Me.gbConnection.Controls.Add(Me.TxtUser)
        Me.gbConnection.Controls.Add(Me.TxtInstance)
        Me.gbConnection.Controls.Add(Me.Label4)
        Me.gbConnection.Controls.Add(Me.Label6)
        Me.gbConnection.Controls.Add(Me.TxtServer)
        Me.gbConnection.Controls.Add(Me.Label5)
        Me.gbConnection.Location = New System.Drawing.Point(4, 1)
        Me.gbConnection.Name = "gbConnection"
        Me.gbConnection.Size = New System.Drawing.Size(914, 38)
        Me.gbConnection.TabIndex = 13
        Me.gbConnection.TabStop = False
        Me.gbConnection.Text = "Connect to..."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(420, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "User:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtUser
        '
        Me.TxtUser.Location = New System.Drawing.Point(458, 14)
        Me.TxtUser.MaxLength = 30
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(96, 20)
        Me.TxtUser.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(560, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Password:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPassword
        '
        Me.TxtPassword.Location = New System.Drawing.Point(622, 14)
        Me.TxtPassword.MaxLength = 30
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(96, 20)
        Me.TxtPassword.TabIndex = 15
        '
        'BtConnection
        '
        Me.BtConnection.AutoSize = True
        Me.BtConnection.Image = Global.SqlServerDBDoc.My.Resources.Resources.DataConnection_NotConnected_1059
        Me.BtConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtConnection.Location = New System.Drawing.Point(724, 13)
        Me.BtConnection.Name = "BtConnection"
        Me.BtConnection.Size = New System.Drawing.Size(78, 23)
        Me.BtConnection.TabIndex = 16
        Me.BtConnection.Text = "Connect"
        Me.BtConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtConnection.UseVisualStyleBackColor = True
        '
        'FrmDbDocumentor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 609)
        Me.Controls.Add(Me.gbConnection)
        Me.Controls.Add(Me.gbSelection)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Name = "FrmDbDocumentor"
        Me.Text = "SQLServer database documentor"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        Me.gbSelection.ResumeLayout(False)
        Me.gbSelection.PerformLayout()
        Me.gbConnection.ResumeLayout(False)
        Me.gbConnection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbDatabases As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbObjectType As ComboBox
    Friend WithEvents LvObjects As ListView
    Friend WithEvents colObjName As ColumnHeader
    Friend WithEvents colObjCreateDate As ColumnHeader
    Friend WithEvents colObjModifyDate As ColumnHeader
    Friend WithEvents TxtObjectDescription As TextBox
    Friend WithEvents LvComponents As ListView
    Friend WithEvents colCompName As ColumnHeader
    Friend WithEvents colCompDataType As ColumnHeader
    Friend WithEvents colCompDescription As ColumnHeader
    Friend WithEvents TxtComponentDescription As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents BtUpdObjDesc As Button
    Friend WithEvents BtUpdCompDesc As Button
    Friend WithEvents lblObjects As Label
    Friend WithEvents lblComponents As Label
    Friend WithEvents colObjDescription As ColumnHeader
    Friend WithEvents TableLayoutPanel As TableLayoutPanel
    Friend WithEvents TxtServer As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtInstance As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents gbSelection As GroupBox
    Friend WithEvents gbConnection As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtUser As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents BtConnection As Button
End Class
