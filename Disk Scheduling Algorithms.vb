Public Class Main
	Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

	End Sub

	Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

	End Sub

	Private Sub BtnExecute_Click(sender As Object, e As EventArgs) Handles BtnExecute.Click
		'DataGridViewColumn column = DataGridView.Columns[0];
		'column.Width = 60;

		'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
		Dim SRTF As New SRTF
		Dim SPF As New SPF

		SPF.Execute()

		'SRTF.Execute()
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

	End Sub
End Class
