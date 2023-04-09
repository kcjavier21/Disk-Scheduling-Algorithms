Public Class SPF
    Public Sub Execute()
        Dim arrivalProc As New List(Of Process)()
        Dim queueProc As New List(Of Process)()
        Dim processing As Process
        Dim Utils As New Utils

        Dim currentTime As Integer = 0
        Dim done As Boolean = False
        Dim index As Integer

        Dim totalTurnAroundTime As Integer = 0
        Dim totalWaitingTime As Integer = 0

        Dim avgTurnAroundTime As Double = 0
        Dim avgWaitingTime As Double = 0

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

            arrivalProc.Add(New Process())
            arrivalProc(i).Id = processIds(i)
            arrivalProc(i).ArrivalTime = Val(TbAtProcesses(i).Text)
            arrivalProc(i).BurstTime = Val(TbCbProcesses(i).Text)
            'burstRemaining(i) = Processes(i).BurstTime
        Next

        Debug.WriteLine("Starting Summary:")
        For Each proc In arrivalProc
            Debug.WriteLine("ID: " & proc.Id & ", BT: " & proc.BurstTime & ", AT: " & proc.ArrivalTime & ", ST:" & proc.StartTime & ", CT:" & proc.CompletionTime & ", TAT:" & proc.TurnAroundTime & ", WT:" & proc.WaitingTime & ", RT:" & proc.ResponseTime)
        Next

        Dim prevProcessId As String = ""
        Dim evnt As String = ""
        Dim eventLog As String = ""


        Dim arrivedFlag As Boolean = False
        Dim finishedFlag As Boolean = False
        Dim startedFlag As Boolean = False

        While Not done
            'Debug.WriteLine("Step #" & currentTime)
            'Moving from arrivalProc to queueProc

            For i As Integer = 0 To arrivalProc.Count - 1
                If (arrivalProc(i).ArrivalTime <= currentTime) And Not (queueProc.Contains(arrivalProc(i))) Then
                    'arrivedFlag = True
                    'Debug.WriteLine(currentTime.ToString & ": Process " & arrivalProc(i).Id & " has arrived.")
                    evnt = currentTime.ToString & ": Process " & arrivalProc(i).Id & " has arrived."
                    queueProc.Add(arrivalProc(i))
                End If
            Next

            'Proccess queueProc
            If queueProc.Count <> 0 Then

                'Sort queueProc by burst time
                For i As Integer = 0 To queueProc.Count - 1
                    index = i
                    For j As Integer = i + 1 To queueProc.Count - 1
                        'Sort if process has no turn around time assigned
                        If (queueProc(j).BurstTime < queueProc(index).BurstTime) And (queueProc(index).TurnAroundTime Is Nothing) Then
                            index = j
                        End If
                    Next
                    Dim temp As Process = queueProc(i)
                    queueProc(i) = queueProc(index)
                    queueProc(index) = temp
                Next

                'Start Processing
                For Each proc In queueProc

                    'Check if something is being processed
                    If IsNothing(processing) Then
                        For i As Integer = 0 To queueProc.Count - 1
                            If queueProc(i).TurnAroundTime Is Nothing Then
                                processing = queueProc(i)
                                'Debug.WriteLine("Process ID (" & processing.Id & ") has started processing")
                                startedFlag = True

                                If processing.ArrivalTime = currentTime Then
                                    evnt = currentTime.ToString & ": Process " & processing.Id & " arrived and started."
                                ElseIf prevProcessId <> processing.Id Then
                                    evnt = currentTime.ToString & ": Process " & prevProcessId & " done." & " Process " & processing.Id & " started."
                                Else
                                    evnt = currentTime.ToString & ": Process " & processing.Id & " started."
                                End If

                                processWalkthrough.Add(processing.Id)

                                processing.StartTime = currentTime
                                processing.TurnAroundTime = processing.BurstTime + processing.WaitingTime

                                totalTurnAroundTime += processing.TurnAroundTime
                                totalWaitingTime += processing.WaitingTime

                                prevProcessId = processing.Id
                                Exit For
                            End If
                        Next
                        'Check if process is complete
                    ElseIf (processing.BurstTime <= 0) And (proc.Id = processing.Id) Then
                        'Debug.WriteLine("Process ID (" & processing.Id & ") has finished processing")
                        finishedFlag = True
                        evnt = currentTime.ToString & ": Process " & processing.Id & " done."

                        processWalkthrough.Add(processing.Id)

                        proc.CompletionTime = currentTime
                        processing.BurstTime = proc.BurstTime
                        proc = processing
                        processing = Nothing
                        If proc Is queueProc.Last() Then
                            done = True
                        End If
                    Else
                        prevProcessId = processing.Id
                    End If

                    'Increment values of waiting processes
                    If IsNothing(proc.TurnAroundTime) Then
                        proc.WaitingTime += 1
                    End If
                Next

                'Decrement burstTime of active process
                If Not IsNothing(processing) Then
                    processing.BurstTime -= 1
                End If
            End If

            'If finishedFlag = False Then
            '    eventLog = currentTime.ToString & ": Process " & prevProcessId & " running."
            'End If

            If startedFlag = False Then
                If finishedFlag = False Then
                    evnt = currentTime.ToString & ": Process " & prevProcessId & " running."
                    processWalkthrough.Add(processing.Id)
                End If
            End If

            eventLog += evnt & vbCrLf

            evnt = ""
            finishedFlag = False
            startedFlag = False

            currentTime += 1
        End While

        avgTurnAroundTime = totalTurnAroundTime / numOfProcesses
        avgWaitingTime = totalWaitingTime / numOfProcesses

        Main.RichTextBox2.Text = eventLog

        Main.LabelAvgWaitingTime.Text = Math.Round(avgWaitingTime, 2) & " ms"
        Main.LabelAvgTurnaroundTime.Text = Math.Round(avgTurnAroundTime, 2) & " ms"

        Utils.VisualizeProcessWalkthrough(processWalkthrough)
    End Sub
End Class
