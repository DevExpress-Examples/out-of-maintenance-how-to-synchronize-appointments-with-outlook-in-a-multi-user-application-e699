Namespace MultiUserOutlookSync
    Partial Public Class LoginForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.comboBox1 = New System.Windows.Forms.ComboBox()
            Me.usersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.dataSet1_Renamed = New MultiUserOutlookSync.DataSet1()
            Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
            Me.usersTableAdapter = New MultiUserOutlookSync.DataSet1TableAdapters.UsersTableAdapter()
            CType(Me.usersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dataSet1_Renamed, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' comboBox1
            ' 
            Me.comboBox1.DataSource = Me.usersBindingSource
            Me.comboBox1.DisplayMember = "FirstName"
            Me.comboBox1.FormattingEnabled = True
            Me.comboBox1.Location = New System.Drawing.Point(12, 32)
            Me.comboBox1.Name = "comboBox1"
            Me.comboBox1.Size = New System.Drawing.Size(296, 21)
            Me.comboBox1.TabIndex = 0
            Me.comboBox1.ValueMember = "ID"
            ' 
            ' usersBindingSource
            ' 
            Me.usersBindingSource.DataMember = "Users"
            Me.usersBindingSource.DataSource = Me.dataSet1_Renamed
            ' 
            ' dataSet1
            ' 
            Me.dataSet1_Renamed.DataSetName = "DataSet1"
            Me.dataSet1_Renamed.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            ' 
            ' simpleButton1
            ' 
            Me.simpleButton1.Location = New System.Drawing.Point(233, 83)
            Me.simpleButton1.Name = "simpleButton1"
            Me.simpleButton1.Size = New System.Drawing.Size(75, 23)
            Me.simpleButton1.TabIndex = 1
            Me.simpleButton1.Text = "Login"
            ' 
            ' usersTableAdapter
            ' 
            Me.usersTableAdapter.ClearBeforeFill = True
            ' 
            ' LoginForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(320, 122)
            Me.Controls.Add(Me.simpleButton1)
            Me.Controls.Add(Me.comboBox1)
            Me.Name = "LoginForm"
            Me.Text = "Please select user name to log in"
            CType(Me.usersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dataSet1_Renamed, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private comboBox1 As System.Windows.Forms.ComboBox
        Private WithEvents simpleButton1 As DevExpress.XtraEditors.SimpleButton

        Private dataSet1_Renamed As DataSet1
        Private usersBindingSource As System.Windows.Forms.BindingSource
        Private usersTableAdapter As MultiUserOutlookSync.DataSet1TableAdapters.UsersTableAdapter
    End Class
End Namespace