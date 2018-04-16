Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace MultiUserOutlookSync
	Partial Public Class LoginForm
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'dataSet1.Users' table. You can move, or remove it, as needed.
			Me.usersTableAdapter.Fill(Me.dataSet1.Users)

		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			Dim frm As New MainForm(Convert.ToInt32(comboBox1.SelectedValue), comboBox1.Text)
			frm.ShowDialog()
		End Sub
	End Class
End Namespace