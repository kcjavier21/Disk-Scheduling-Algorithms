Public Class Main
	Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

	End Sub

	Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

	End Sub

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

	Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

	End Sub

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

	End Sub

	Private Sub TbCbProcessA_TextChanged(sender As Object, e As EventArgs) Handles TbCbProcessA.TextChanged

	End Sub

	Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged

	End Sub

	Private Sub STRF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		RdBtnFCFS.Checked = True
	End Sub

	Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

	End Sub

	Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

	End Sub

	Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

	End Sub

	Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

	End Sub
End Class
