Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace MultiUserOutlookSync
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New LoginForm())
        End Sub
    End Class
End Namespace