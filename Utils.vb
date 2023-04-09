Public Class Utils
	Public Sub AddProcessIdColumn(value, index, columnWidth)
		Main.DataGridView1.Columns.Add(value, value)
		Main.DataGridView1.Columns(index).Width = columnWidth
		Main.DataGridView1.Columns(index).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopCenter
		Main.DataGridView1.Columns(index).ReadOnly = True
	End Sub

	Public Sub AddColoredRows(value, index)
		If value = "A" Then
			Main.DataGridView1.Columns(index).DefaultCellStyle.BackColor = Color.Blue
		ElseIf value = "B" Then
			Main.DataGridView1.Columns(index).DefaultCellStyle.BackColor = Color.Green
		ElseIf value = "C" Then
			Main.DataGridView1.Columns(index).DefaultCellStyle.BackColor = Color.Orange
		ElseIf value = "D" Then
			Main.DataGridView1.Columns(index).DefaultCellStyle.BackColor = Color.Yellow
		ElseIf value = "E" Then
			Main.DataGridView1.Columns(index).DefaultCellStyle.BackColor = Color.Red
		End If
	End Sub

	Public Function CheckIfTextBoxIsNotEmpty(ByVal txtBox As TextBox) As Boolean
		If String.IsNullOrEmpty(txtBox.Text) Then
			MsgBox("Put a damn value on all text boxes!", MsgBoxStyle.Exclamation)
			txtBox.Focus()

			Return False
		End If

		Return True
	End Function

	Public Function CheckIfNumber(ByVal txtBox As TextBox) As Boolean
		If IsNumeric(txtBox.Text) Then
			Return True
		End If

		MsgBox("Input numbers only!", MsgBoxStyle.Exclamation)
		txtBox.Focus()
		Return False
	End Function

	Public Function CheckIfPositiveNumber(ByVal txtBox As TextBox) As Boolean
		If Val(txtBox.Text) >= 0 Then
			Return True
		End If

		MsgBox("Positive numbers only!", MsgBoxStyle.Exclamation)
		txtBox.Focus()

		Return False
	End Function

	Public Sub VisualizeProcessWalkthrough(ByVal processWalkthrough As ArrayList)
		Dim columnWidth As Integer = 0
		Dim count As Integer = 0
		Dim prevProcessId As String = ""

		Main.DataGridView1.Columns.Clear()
		Main.DataGridView1.Rows.Clear()

		For i = 0 To (processWalkthrough.Count - 1)
			If processWalkthrough(i) = prevProcessId Then
				columnWidth += 25

				If i = (processWalkthrough.Count - 1) Then
					AddProcessIdColumn(processWalkthrough(i), count, columnWidth)
					AddColoredRows(processWalkthrough(i), count)
				End If

			ElseIf processWalkthrough(i) <> prevProcessId Then
				AddProcessIdColumn(prevProcessId, count, columnWidth)
				AddColoredRows(prevProcessId, count)

				columnWidth = 0
				count += 1
			End If
			prevProcessId = processWalkthrough(i)
		Next
	End Sub
End Class
