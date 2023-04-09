Public Class SRTF
	Public Sub Execute()
		Dim result As String = ""
		Dim timePassed As String = ""

		Dim Processes As New List(Of Process)
		Dim Utils As New Utils

		Dim avgTurnAroundTime As Double = 0
		Dim avgWaitingTime As Double = 0
		Dim avgResponseTime As Double = 0
		Dim throughput As Double = 0
		Dim cpuUtilization As Double = 0

		Dim totalTurnAroundTime As Integer = 0
		Dim totalWaitingTime As Integer = 0
		Dim totalResponseTime As Integer = 0
		Dim totalIdleTime As Integer = 0

		Dim burstRemaining(99)
		Dim isCompleted(99) As Boolean
		Dim processWalkthrough = New ArrayList

		Dim processIds() As String = {"A", "B", "C", "D", "E"}
		Dim TbAtProcesses() As TextBox = {Main.TbAtProcessA, Main.TbAtProcessB, Main.TbAtProcessC, Main.TbAtProcessD, Main.TbAtProcessE}
		Dim TbCbProcesses() As TextBox = {Main.TbCbProcessA, Main.TbCbProcessB, Main.TbCbProcessC, Main.TbCbProcessD, Main.TbCbProcessE}

		Dim numOfProcesses As Integer = processIds.Length

		For i = 0 To (numOfProcesses - 1)
			Dim atIsNotEmpty = Utils.CheckIfTextBoxIsNotEmpty(TbAtProcesses(i))
			If atIsNotEmpty = False Then Return

			Dim atIsNumber = Utils.CheckIfNumber(TbAtProcesses(i))
			If atIsNumber = False Then Return

			Dim atIsPositiveNumber = Utils.CheckIfPositiveNumber(TbAtProcesses(i))
			If atIsPositiveNumber = False Then Return

			Dim cbIsNotEmpty = Utils.CheckIfTextBoxIsNotEmpty(TbCbProcesses(i))
			If cbIsNotEmpty = False Then Return

			Dim cbIsNumber = Utils.CheckIfNumber(TbCbProcesses(i))
			If cbIsNumber = False Then Return

			Dim cbIsPositiveNumber = Utils.CheckIfPositiveNumber(TbCbProcesses(i))
			If cbIsNumber = False Then Return

			Processes.Add(New Process())
			Processes(i).Id = processIds(i)
			Processes(i).ArrivalTime = Val(TbAtProcesses(i).Text)
			Processes(i).BurstTime = Val(TbCbProcesses(i).Text)
			burstRemaining(i) = Processes(i).BurstTime
		Next


		'InitializeComponent value of isCompleted Array to all False
		For i = 0 To 99
			isCompleted(i) = False
		Next

		Dim currentTime As Integer = 0
		Dim completedProcesses As Integer = 0
		Dim prev As Integer = 0


		While completedProcesses <> numOfProcesses
			Dim idx As Integer = -1
			Dim mn As Integer = 10000000

			For i = 0 To (numOfProcesses - 1)
				If Processes(i).ArrivalTime <= currentTime And isCompleted(i) = False Then
					If burstRemaining(i) < mn Then
						mn = burstRemaining(i)
						idx = i
					End If

					If burstRemaining(i) = mn Then
						If Processes(i).ArrivalTime < Processes(idx).ArrivalTime Then
							mn = burstRemaining(i)
							idx = i
						End If
					End If
				End If
			Next

			If idx <> -1 Then
				If burstRemaining(idx) = Processes(idx).BurstTime Then
					Processes(idx).StartTime = currentTime
					totalIdleTime += Processes(idx).StartTime - prev
				End If

				'ReDim Preserve processWalkthrough(currentTime)
				processWalkthrough.Add(Processes(idx).Id)

				'result += "P-" & processWalkthrough(currentTime).ToString & "     "
				'timePassed += "C-" & currentTime.ToString() & "     "

				burstRemaining(idx) -= 1
				currentTime += 1
				prev = currentTime


				If burstRemaining(idx) = 0 Then
					If completedProcesses = (numOfProcesses - 1) Then
						processWalkthrough.Add(Processes(idx).Id)
					End If

					Processes(idx).CompletionTime = currentTime
					Processes(idx).TurnAroundTime = Processes(idx).CompletionTime - Processes(idx).ArrivalTime
					Processes(idx).WaitingTime = Processes(idx).TurnAroundTime - Processes(idx).BurstTime
					Processes(idx).ResponseTime = Processes(idx).StartTime - Processes(idx).ArrivalTime

					totalTurnAroundTime += Processes(idx).TurnAroundTime
					totalWaitingTime += Processes(idx).WaitingTime
					totalResponseTime += Processes(idx).ResponseTime

					isCompleted(idx) = True
					completedProcesses += 1
				End If
			Else
				currentTime += 1
			End If


		End While

		Dim minArrivalTime As Integer = 10000000
		Dim maxCompletionTime As Integer = -1

		For i = 0 To (numOfProcesses - 1)
			minArrivalTime = Math.Min(minArrivalTime, Processes(i).ArrivalTime)
			maxCompletionTime = Math.Max(maxCompletionTime, Processes(i).CompletionTime)
		Next

		avgTurnAroundTime = totalTurnAroundTime / numOfProcesses
		avgWaitingTime = totalWaitingTime / numOfProcesses
		avgResponseTime = totalResponseTime / numOfProcesses
		cpuUtilization = ((maxCompletionTime - totalIdleTime) / maxCompletionTime * 100)
		throughput = numOfProcesses / (maxCompletionTime - minArrivalTime)

		Dim prevProcessId As String = ""
		Dim columnWidth As Integer = 0
		Dim count = 0
		Dim eventLog As String = ""

		Main.DataGridView1.Columns.Clear()
		Main.DataGridView1.Rows.Clear()

		For i = 0 To (processWalkthrough.Count - 1)
			If processWalkthrough(i) = prevProcessId Then
				columnWidth += 25

				If i = (processWalkthrough.Count - 1) Then
					Utils.AddProcessIdColumn(processWalkthrough(i), count, columnWidth)
					Utils.AddColoredRows(processWalkthrough(i), count)
					eventLog += i.ToString & ": Process " & processWalkthrough(i) & " done." & vbCrLf
				Else
					eventLog += i.ToString & ": Process " & processWalkthrough(i) & " running." & vbCrLf
				End If

			ElseIf processWalkthrough(i) <> prevProcessId Then
				Utils.AddProcessIdColumn(prevProcessId, count, columnWidth)
				Utils.AddColoredRows(prevProcessId, count)

				If i <> 0 Then
					If processWalkthrough.LastIndexOf(prevProcessId) <= i Then
						eventLog += i.ToString & ": Process " & prevProcessId & " done. " & " Process " & processWalkthrough(i) & " started" & vbCrLf
					Else
						eventLog += i.ToString & ": Process " & prevProcessId & " preempted. " & " Process " & processWalkthrough(i) & " started" & vbCrLf
					End If

				Else
					eventLog += i.ToString & ": Process " & processWalkthrough(i) & " started." & vbCrLf
				End If

				columnWidth = 0
				count += 1
			End If
			prevProcessId = processWalkthrough(i)
		Next

		Main.RichTextBox2.Text = eventLog
	End Sub
End Class
