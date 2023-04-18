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
		Me.TbAtProcessA.Text = ""
		Me.TbAtProcessB.Text = ""
		Me.TbAtProcessC.Text = ""
		Me.TbAtProcessD.Text = ""
		Me.TbAtProcessE.Text = ""

		Me.TbCbProcessA.Text = ""
		Me.TbCbProcessB.Text = ""
		Me.TbCbProcessC.Text = ""
		Me.TbCbProcessD.Text = ""
		Me.TbCbProcessE.Text = ""

		Me.DataGridView1.Columns.Clear()
		Me.DataGridView1.Rows.Clear()

		Me.RichTextBox2.Text = ""
	End Sub

	Private Sub TbAtProcessA_TextChanged(sender As Object, e As EventArgs) Handles TbAtProcessA.TextChanged

	End Sub

	Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

	End Sub
End Class
