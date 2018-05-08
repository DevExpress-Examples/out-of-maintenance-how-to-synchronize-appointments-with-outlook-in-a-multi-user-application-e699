Namespace MultiUserOutlookSync
    Partial Public Class MainForm
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
            Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
            Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
            Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
            Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
            Me.scheduleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.dataSet1_Renamed = New MultiUserOutlookSync.DataSet1()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.btnExport = New System.Windows.Forms.Button()
            Me.btnImport = New System.Windows.Forms.Button()
            Me.scheduleTableAdapter = New MultiUserOutlookSync.DataSet1TableAdapters.ScheduleTableAdapter()
            CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.scheduleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dataSet1_Renamed, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' schedulerControl1
            ' 
            Me.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.WorkWeek
            Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.schedulerControl1.Location = New System.Drawing.Point(0, 41)
            Me.schedulerControl1.Name = "schedulerControl1"
            Me.schedulerControl1.Size = New System.Drawing.Size(949, 394)
            Me.schedulerControl1.Start = New Date(2007, 11, 19, 0, 0, 0, 0)
            Me.schedulerControl1.Storage = Me.schedulerStorage1
            Me.schedulerControl1.TabIndex = 0
            Me.schedulerControl1.Text = "schedulerControl1"
            Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
            Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2)
            ' 
            ' schedulerStorage1
            ' 
            Me.schedulerStorage1.Appointments.CustomFieldMappings.Add(New DevExpress.XtraScheduler.AppointmentCustomFieldMapping("UserID", "UserID"))
            Me.schedulerStorage1.Appointments.CustomFieldMappings.Add(New DevExpress.XtraScheduler.AppointmentCustomFieldMapping("EntryID", "OutlookEntryID"))
            Me.schedulerStorage1.Appointments.DataSource = Me.scheduleBindingSource
            Me.schedulerStorage1.Appointments.Mappings.Description = "Description"
            Me.schedulerStorage1.Appointments.Mappings.End = "EndTime"
            Me.schedulerStorage1.Appointments.Mappings.Label = "Label"
            Me.schedulerStorage1.Appointments.Mappings.Start = "StartTime"
            Me.schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            ' 
            ' scheduleBindingSource
            ' 
            Me.scheduleBindingSource.DataMember = "Schedule"
            Me.scheduleBindingSource.DataSource = Me.dataSet1_Renamed
            ' 
            ' dataSet1
            ' 
            Me.dataSet1_Renamed.DataSetName = "DataSet1"
            Me.dataSet1_Renamed.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            ' 
            ' panel1
            ' 
            Me.panel1.Controls.Add(Me.label1)
            Me.panel1.Controls.Add(Me.btnExport)
            Me.panel1.Controls.Add(Me.btnImport)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(949, 41)
            Me.panel1.TabIndex = 1
            ' 
            ' label1
            ' 
            Me.label1.AutoSize = True
            Me.label1.Font = New System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(204)))
            Me.label1.Location = New System.Drawing.Point(60, 16)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(162, 14)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Synchronization Method:"
            ' 
            ' btnExport
            ' 
            Me.btnExport.Font = New System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
            Me.btnExport.Location = New System.Drawing.Point(242, 12)
            Me.btnExport.Name = "btnExport"
            Me.btnExport.Size = New System.Drawing.Size(160, 23)
            Me.btnExport.TabIndex = 1
            Me.btnExport.Text = "Export (to Outlook)"
            Me.btnExport.UseVisualStyleBackColor = True
            ' 
            ' btnImport
            ' 
            Me.btnImport.Font = New System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(204)))
            Me.btnImport.Location = New System.Drawing.Point(408, 12)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(175, 23)
            Me.btnImport.TabIndex = 2
            Me.btnImport.Text = "Import (from Outlook)"
            Me.btnImport.UseVisualStyleBackColor = True
            ' 
            ' scheduleTableAdapter
            ' 
            Me.scheduleTableAdapter.ClearBeforeFill = True
            ' 
            ' MainForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(949, 435)
            Me.Controls.Add(Me.schedulerControl1)
            Me.Controls.Add(Me.panel1)
            Me.Name = "MainForm"
            Me.Text = "Outlook Synchronization - [{0}]"
            CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.scheduleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dataSet1_Renamed, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private WithEvents schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
        Private WithEvents schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
        Private panel1 As System.Windows.Forms.Panel
        Private WithEvents btnExport As System.Windows.Forms.Button
        Private WithEvents btnImport As System.Windows.Forms.Button

        Private dataSet1_Renamed As DataSet1
        Private scheduleBindingSource As System.Windows.Forms.BindingSource
        Private scheduleTableAdapter As MultiUserOutlookSync.DataSet1TableAdapters.ScheduleTableAdapter
        Private label1 As System.Windows.Forms.Label
    End Class
End Namespace

