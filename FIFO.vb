Public Class FIFO
	Public Sub Execute()
        Dim Index As Integer
        Dim Add As Integer
        Dim ProcList As New List(Of Process)()

        Dim Utils As New Utils

        'ProcList.Add(New Process("A", 0, 8))
        'ProcList.Add(New Process("B", 3, 4))
        'ProcList.Add(New Process("C", 4, 5))
        'ProcList.Add(New Process("D", 6, 3))
        'ProcList.Add(New Process("E", 10, 2))

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
            'burstRemaining(i) = Processes(i).BurstTime
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

        For Ctr1 = 0 To (ProcList.Count - 1)
            For Ctr2 = 0 To (ProcList(Ctr1).BurstTime - 1)
                Console.WriteLine(ProcList(Ctr1).Id)
                'RichTextBox1.Text = RichTextBox1.Text & " " & ProcList(Ctr1).Id
            Next
        Next
    End Sub
End Class
