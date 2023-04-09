Public Class Main
	Private Sub BtnExecute_Click(sender As Object, e As EventArgs) Handles BtnExecute.Click
		Dim SRTF As New SRTF
		Dim SPF As New SPF
		Dim FCFS As New FCFS

		If RdBtnFCFS.Checked = True Then
			FCFS.Execute()
		ElseIf RdBtnSPF.Checked = True Then
			SPF.Execute()
		ElseIf RdBtnSRTF.Checked = True Then
			SRTF.Execute()
		Else
			FCFS.Execute()
		End If

	End Sub

	Private Sub STRF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		RdBtnFCFS.Checked = True
	End Sub

	Private Sub BtnClearAll_Click(sender As Object, e As EventArgs) Handles BtnClearAll.Click
		For Each ctrl As Control In Me.Controls
			If TypeOf ctrl Is TextBox Then
				CType(ctrl, TextBox).Text = ""
			End If
		Next
	End Sub
End Class
