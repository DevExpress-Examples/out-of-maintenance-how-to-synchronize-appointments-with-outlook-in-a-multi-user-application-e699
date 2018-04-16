using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Outlook;
using DevExpress.XtraScheduler.Exchange;
using DevExpress.XtraScheduler.Outlook.Interop;
using OutlookInterop = Microsoft.Office.Interop.Outlook;
using System.Data.OleDb;

namespace MultiUserOutlookSync {
	public partial class MainForm : Form {
		
		private const string OutlookEntryIDFieldName = "OutlookEntryID";
		public const string CustomFieldPropertyName = "UserID";
		public const string OutlookUserPropertyName = "UserID";
		int userID = -1;
        int count = 0;

		public MainForm(int userID, string userName) {
			InitializeComponent();
            this.scheduleTableAdapter.Adapter.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(Adapter_RowUpdated);

			Text = String.Format(Text, userName);
            this.userID = userID;
            this.scheduleTableAdapter.FillBy(this.dataSet1.Schedule, userID);
			this.schedulerControl1.Start = DateTime.Today;
		}

        void Adapter_RowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                int id = 0;
                using (OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY", this.scheduleTableAdapter.Connection))
                {
                    id = (int)cmd.ExecuteScalar();
                }
                e.Row["ID"] = id;
            }
        }

		public int UserID { get { return userID; } }

		private void button1_Click(object sender, EventArgs e) {
			ImportSync();
		}
		private void button2_Click(object sender, EventArgs e) {
			ExportSync();

		}



		private void ImportSync() {
            count = 0;

			AppointmentImportSynchronizer synchronizer = schedulerStorage1.CreateOutlookImportSynchronizer();
			synchronizer.AppointmentSynchronizing += new DevExpress.XtraScheduler.AppointmentSynchronizingEventHandler(synchronizer_AppointmentImportSynchronizing);
			synchronizer.AppointmentSynchronized +=new AppointmentSynchronizedEventHandler(synchronizer_AppointmentImportSynchronized);
            synchronizer.ForeignIdFieldName = OutlookEntryIDFieldName;
			synchronizer.Synchronize();
            MessageBox.Show(string.Format("Synchronized {0} appointment(s); source objects count: {1}", count, synchronizer.SourceObjectCount), "Import successful", MessageBoxButtons.OK);
		}

		private void ExportSync() {
            count = 0;

			AppointmentExportSynchronizer synchronizer = schedulerStorage1.CreateOutlookExportSynchronizer();
			synchronizer.AppointmentSynchronizing += new DevExpress.XtraScheduler.AppointmentSynchronizingEventHandler(synchronizer_AppointmentExportSynchronizing);
			synchronizer.AppointmentSynchronized += new AppointmentSynchronizedEventHandler(synchronizer_AppointmentExportSynchronized);
			synchronizer.ForeignIdFieldName = OutlookEntryIDFieldName;
			synchronizer.Synchronize();
            MessageBox.Show(string.Format("Synchronized {0} appointment(s); source objects count: {1}", count, synchronizer.SourceObjectCount), "Export successful", MessageBoxButtons.OK);

		}

		void synchronizer_AppointmentExportSynchronizing(object sender, DevExpress.XtraScheduler.AppointmentSynchronizingEventArgs e) {
			OutlookAppointmentSynchronizingEventArgs args = (OutlookAppointmentSynchronizingEventArgs)e;
			// Prevent appointments with other UserID from being deleted
			if (e.Operation == SynchronizeOperation.Delete) {
				if (GetOutlookAppointmentUserId(args.OutlookAppointment) != UserID) {
					e.Cancel = true;
					return;
				}
			}
			// Mark the new Outlook appoinment item with UserID
			if (e.Operation == SynchronizeOperation.Create) {
				SetOutlookAppointmentUserId(args.OutlookAppointment);
			}
		}

        void synchronizer_AppointmentImportSynchronized(object sender, AppointmentSynchronizedEventArgs e) {
            count++;
        }

		void synchronizer_AppointmentExportSynchronized(object sender, AppointmentSynchronizedEventArgs e) {
            count++;

            // !!! Note: the following code isn't necessary when targeting XtraScheduler v2008 vol 1 and higher
			OutlookAppointmentSynchronizedEventArgs args = (OutlookAppointmentSynchronizedEventArgs)e;
			if (args.Appointment != null && args.OutlookAppointment != null) {
			    if (e.Appointment.CustomFields[OutlookEntryIDFieldName] == null)
			        e.Appointment.CustomFields[OutlookEntryIDFieldName] = args.OutlookAppointment.EntryID;
			}
		}

		void synchronizer_AppointmentImportSynchronizing(object sender, DevExpress.XtraScheduler.AppointmentSynchronizingEventArgs e) {
			if (e.Operation == SynchronizeOperation.Delete)
				return;

			// prevent appointments with other UserID from being created or updated
            OutlookAppointmentSynchronizingEventArgs args = (OutlookAppointmentSynchronizingEventArgs)e;
            e.Cancel = GetOutlookAppointmentUserId(args.OutlookAppointment) != UserID;
		}

		private void SetOutlookAppointmentUserId(_AppointmentItem aptItem) {
			OutlookInterop.AppointmentItem olApt = (OutlookInterop.AppointmentItem)aptItem;
			OutlookInterop.UserProperty prop = olApt.UserProperties.Add(OutlookUserPropertyName, OutlookInterop.OlUserPropertyType.olNumber, false, System.Reflection.Missing.Value);
			try {
				prop.Value = UserID;
			} finally {
				Marshal.ReleaseComObject(prop);
			}
		}
		private int GetOutlookAppointmentUserId(_AppointmentItem aptItem) {
			OutlookInterop.AppointmentItem olApt = (OutlookInterop.AppointmentItem)aptItem;
			IEnumerator en = olApt.UserProperties.GetEnumerator();
			en.Reset();
			while (en.MoveNext()) {
				OutlookInterop.UserProperty prop = (OutlookInterop.UserProperty)en.Current;
				try {
					if (prop.Name == OutlookUserPropertyName)
						return Convert.ToInt32(prop.Value);
				} finally {
					Marshal.ReleaseComObject(prop);
				}
			}
			return -1;
		}

        
        // Update the newly inserted appointment with the current UserID 
		private void schedulerControl1_InitNewAppointment(object sender, AppointmentEventArgs e) {
			e.Appointment.CustomFields[CustomFieldPropertyName] = UserID;
		}

        // Save changes to the underlying data source
        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            scheduleTableAdapter.Update(dataSet1.Schedule);
            dataSet1.AcceptChanges();
        }

        // This event handler is necessary when creating appointments via code
        private void schedulerStorage1_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e) {
            e.Object.CustomFields[CustomFieldPropertyName] = UserID;
        }
	}
}