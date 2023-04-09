Public Class FCFS
    Public Sub Execute()
        Dim Index As Integer
        Dim Add As Integer
        Dim ProcList As New List(Of Process)()
        Dim Utils As New Utils

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

            ProcList.Add(New Process())
            ProcList(i).Id = processIds(i)
            ProcList(i).ArrivalTime = Val(TbAtProcesses(i).Text)
            ProcList(i).BurstTime = Val(TbCbProcesses(i).Text)
        Next

        'Sort the ProcList from the smallest arrival time
        For i As Integer = 0 To ProcList.Count - 1
            Index = i
            For j As Integer = i + 1 To ProcList.Count - 1

                If (ProcList(j).ArrivalTime < ProcList(Index).ArrivalTime) Then
                    Index = j
                End If
            Next
            Dim temp As Process = ProcList(i)
            ProcList(i) = ProcList(Index)
            ProcList(Index) = temp
        Next

        Dim prevProcessId As String = ""
        Dim eventLog = ""
        Dim currentTime As Integer = 0

        'Create event log
        For i = 0 To (ProcList.Count - 1)
            For j = 0 To (ProcList(i).BurstTime - 1)
                If ProcList(i).Id <> prevProcessId Then
                    If prevProcessId = "" Then
                        eventLog += currentTime.ToString + ": Process " & ProcList(i).Id & " started." & vbCrLf
                    Else
                        eventLog += currentTime.ToString + ": Process " & prevProcessId & " done. Process " & ProcList(i).Id & " started" & vbCrLf
                    End If
                Else
                    eventLog += currentTime.ToString + ": Process " & ProcList(i).Id & " running." & vbCrLf
                End If

                processWalkthrough.Add(ProcList(i).Id)

                currentTime += 1
                prevProcessId = ProcList(i).Id
            Next
        Next

        eventLog += currentTime.ToString + ": Process " & prevProcessId & " done."
        Main.RichTextBox2.Text = eventLog


        Utils.VisualizeProcessWalkthrough(processWalkthrough)
    End Sub
End Class
