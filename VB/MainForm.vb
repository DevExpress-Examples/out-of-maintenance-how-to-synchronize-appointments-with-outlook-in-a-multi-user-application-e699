Imports System
Imports System.Collections
Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Outlook
Imports DevExpress.XtraScheduler.Exchange
Imports DevExpress.XtraScheduler.Outlook.Interop
Imports OutlookInterop = Microsoft.Office.Interop.Outlook
Imports System.Data.OleDb

Namespace MultiUserOutlookSync
    Partial Public Class MainForm
        Inherits Form

        Private Const OutlookEntryIDFieldName As String = "OutlookEntryID"
        Public Const CustomFieldPropertyName As String = "UserID"
        Public Const OutlookUserPropertyName As String = "UserID"

        Private userID_Renamed As Integer = -1
        Private count As Integer = 0

        Public Sub New(ByVal userID As Integer, ByVal userName As String)
            InitializeComponent()
            AddHandler scheduleTableAdapter.Adapter.RowUpdated, AddressOf Adapter_RowUpdated

            Text = String.Format(Text, userName)
            Me.userID_Renamed = userID
            Me.scheduleTableAdapter.FillBy(Me.dataSet1_Renamed.Schedule, userID)
            Me.schedulerControl1.Start = Date.Today
        End Sub

        Private Sub Adapter_RowUpdated(ByVal sender As Object, ByVal e As System.Data.OleDb.OleDbRowUpdatedEventArgs)
            If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
                Dim id As Integer = 0
                Using cmd As New OleDbCommand("SELECT @@IDENTITY", Me.scheduleTableAdapter.Connection)
                    id = DirectCast(cmd.ExecuteScalar(), Integer)
                End Using
                e.Row("ID") = id
            End If
        End Sub

        Public ReadOnly Property UserID() As Integer
            Get
                Return userID_Renamed
            End Get
        End Property

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
            ImportSync()
        End Sub
        Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
            ExportSync()

        End Sub



        Private Sub ImportSync()
            count = 0

            Dim synchronizer As AppointmentImportSynchronizer = schedulerStorage1.CreateOutlookImportSynchronizer()
            AddHandler synchronizer.AppointmentSynchronizing, AddressOf synchronizer_AppointmentImportSynchronizing
            AddHandler synchronizer.AppointmentSynchronized, AddressOf synchronizer_AppointmentImportSynchronized
            synchronizer.ForeignIdFieldName = OutlookEntryIDFieldName
            synchronizer.Synchronize()
            MessageBox.Show(String.Format("Synchronized {0} appointment(s); source objects count: {1}", count, synchronizer.SourceObjectCount), "Import successful", MessageBoxButtons.OK)
        End Sub

        Private Sub ExportSync()
            count = 0

            Dim synchronizer As AppointmentExportSynchronizer = schedulerStorage1.CreateOutlookExportSynchronizer()
            AddHandler synchronizer.AppointmentSynchronizing, AddressOf synchronizer_AppointmentExportSynchronizing
            AddHandler synchronizer.AppointmentSynchronized, AddressOf synchronizer_AppointmentExportSynchronized
            synchronizer.ForeignIdFieldName = OutlookEntryIDFieldName
            synchronizer.Synchronize()
            MessageBox.Show(String.Format("Synchronized {0} appointment(s); source objects count: {1}", count, synchronizer.SourceObjectCount), "Export successful", MessageBoxButtons.OK)

        End Sub

        Private Sub synchronizer_AppointmentExportSynchronizing(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.AppointmentSynchronizingEventArgs)
            Dim args As OutlookAppointmentSynchronizingEventArgs = CType(e, OutlookAppointmentSynchronizingEventArgs)
            ' Prevent appointments with other UserID from being deleted
            If e.Operation = SynchronizeOperation.Delete Then
                If GetOutlookAppointmentUserId(args.OutlookAppointment) <> UserID Then
                    e.Cancel = True
                    Return
                End If
            End If
            ' Mark the new Outlook appoinment item with UserID
            If e.Operation = SynchronizeOperation.Create Then
                SetOutlookAppointmentUserId(args.OutlookAppointment)
            End If
        End Sub

        Private Sub synchronizer_AppointmentImportSynchronized(ByVal sender As Object, ByVal e As AppointmentSynchronizedEventArgs)
            count += 1
        End Sub

        Private Sub synchronizer_AppointmentExportSynchronized(ByVal sender As Object, ByVal e As AppointmentSynchronizedEventArgs)
            count += 1

            ' !!! Note: the following code isn't necessary when targeting XtraScheduler v2008 vol 1 and higher
            Dim args As OutlookAppointmentSynchronizedEventArgs = CType(e, OutlookAppointmentSynchronizedEventArgs)
            If args.Appointment IsNot Nothing AndAlso args.OutlookAppointment IsNot Nothing Then
                If e.Appointment.CustomFields(OutlookEntryIDFieldName) Is Nothing Then
                    e.Appointment.CustomFields(OutlookEntryIDFieldName) = args.OutlookAppointment.EntryID
                End If
            End If
        End Sub

        Private Sub synchronizer_AppointmentImportSynchronizing(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.AppointmentSynchronizingEventArgs)
            If e.Operation = SynchronizeOperation.Delete Then
                Return
            End If

            ' prevent appointments with other UserID from being created or updated
            Dim args As OutlookAppointmentSynchronizingEventArgs = CType(e, OutlookAppointmentSynchronizingEventArgs)
            e.Cancel = GetOutlookAppointmentUserId(args.OutlookAppointment) <> UserID
        End Sub

        Private Sub SetOutlookAppointmentUserId(ByVal aptItem As _AppointmentItem)
            Dim olApt As OutlookInterop.AppointmentItem = CType(aptItem, OutlookInterop.AppointmentItem)
            Dim prop As OutlookInterop.UserProperty = olApt.UserProperties.Add(OutlookUserPropertyName, OutlookInterop.OlUserPropertyType.olNumber, False, System.Reflection.Missing.Value)
            Try
                prop.Value = UserID
            Finally
                Marshal.ReleaseComObject(prop)
            End Try
        End Sub
        Private Function GetOutlookAppointmentUserId(ByVal aptItem As _AppointmentItem) As Integer
            Dim olApt As OutlookInterop.AppointmentItem = CType(aptItem, OutlookInterop.AppointmentItem)
            Dim en As IEnumerator = olApt.UserProperties.GetEnumerator()
            en.Reset()
            Do While en.MoveNext()
                Dim prop As OutlookInterop.UserProperty = DirectCast(en.Current, OutlookInterop.UserProperty)
                Try
                    If prop.Name = OutlookUserPropertyName Then
                        Return Convert.ToInt32(prop.Value)
                    End If
                Finally
                    Marshal.ReleaseComObject(prop)
                End Try
            Loop
            Return -1
        End Function


        ' Update the newly inserted appointment with the current UserID 
        Private Sub schedulerControl1_InitNewAppointment(ByVal sender As Object, ByVal e As AppointmentEventArgs) Handles schedulerControl1.InitNewAppointment
            e.Appointment.CustomFields(CustomFieldPropertyName) = UserID
        End Sub

        ' Save changes to the underlying data source
        Private Sub schedulerStorage1_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsDeleted, schedulerStorage1.AppointmentsInserted, schedulerStorage1.AppointmentsChanged
            scheduleTableAdapter.Update(dataSet1_Renamed.Schedule)
            dataSet1_Renamed.AcceptChanges()
        End Sub

        ' This event handler is necessary when creating appointments via code
        Private Sub schedulerStorage1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs) Handles schedulerStorage1.AppointmentInserting
            e.Object.CustomFields(CustomFieldPropertyName) = UserID
        End Sub
    End Class
End Namespace