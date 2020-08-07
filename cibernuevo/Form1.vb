Public Class Form1
    Dim conexion As New conexion
    Dim ds As New DataSet
    Dim diafecha As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        diafecha = Date.Now.Year & "/" & Date.Now.Month & "/" & Date.Now.Day
        ds = conexion.consultas("SELECT * FROM tiempos WHERE Fecha='" & diafecha & "' order by Id desc", "")
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.Columns.Add("Tiemporestante", "Tiempo restante")


        tiempo()
    End Sub
    Public Function tiempo()
        Try

            ds = conexion.consultas("SELECT * FROM tiempos WHERE Fecha='" & diafecha & "' order by Id desc", "")
            DataGridView1.DataSource = ds.Tables(0)
            Dim variable As Integer = 0
            For Each item In ds.Tables(0).Rows

                Dim inicio As DateTime = Date.Now
                Dim fin As DateTime = Convert.ToDateTime(ds.Tables(0).Rows(variable).Item(3).ToString)
                Dim horas As Integer = (CInt(inicio.Hour) * 60 * 60)
                Dim minutos As Integer = (CInt(inicio.Minute) * 60)
                Dim segundos As Integer = inicio.Second
                Dim tiempototal As Integer = horas + minutos + segundos

                Dim horas2 As Integer = (CInt(fin.Hour) * 60 * 60)
                Dim minutos2 As Integer = (CInt(fin.Minute) * 60)
                Dim segundos2 As Integer = fin.Second
                Dim tiempototal2 As Integer = horas2 + minutos2 + segundos2


                Dim decimall As Double = tiempototal2 - tiempototal

                decimall = decimall / 60 / 60

                Dim Horast As Long = Int(decimall)

                Dim residuo As Double = decimall - Horast
                Dim minutost As Long = Int(residuo * 60)
                Dim segundost As Long = ((residuo * 60) - Int(residuo * 60)) * 60
                If (Horast < 0 Or minutost < 0 Or segundost < 0) Then
                    DataGridView1.Rows(variable).DefaultCellStyle.BackColor = Color.Yellow

                Else
                    DataGridView1.Rows(variable).Cells("Tiemporestante").Value = Horast & ":" & minutost & ":" & segundost
                    DataGridView1.Rows(variable).DefaultCellStyle.BackColor = Color.LightGreen
                End If
                If (Horast = 0 And minutost = 0 And segundost = 1) Then
                    My.Computer.Audio.Play("1.wav", AudioPlayMode.Background)

                End If



                variable = variable + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Label3.Text = Date.Now.Hour & ":" & Date.Now.Minute & ":" & Date.Now.Second

        Return Nothing
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tiempo()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, ComboBox1.TextChanged
        Try
            Dim monto As Double
            If (ComboBox1.Text = "Computadora 1" Or ComboBox1.Text = "Computadora 2" Or ComboBox1.Text = "Computadora 3" Or ComboBox1.Text = "Computadora 4") Then
                monto = 10
            Else
                monto = 15
                If (TextBox1.Text = "25") Then
                    monto = 12.5
                End If
                If (TextBox1.Text = "30") Then
                    monto = 10
                End If
            End If

            Dim fin As DateTime = Date.Now
            Dim horas2 As Integer = (CInt(fin.Hour) * 60 * 60)
            Dim minutos2 As Integer = (CInt(fin.Minute) * 60)
            Dim segundos2 As Integer = fin.Second
            Dim tiempototal2 As Integer = horas2 + minutos2 + segundos2
            Dim decimall As Double = (CDbl(TextBox1.Text) / monto * 60 * 60) + tiempototal2
            decimall = decimall / 60 / 60
            Dim Horast As Long = Int(decimall)

            Dim residuo As Double = decimall - Horast
            Dim minutost As Long = Int(residuo * 60)
            Dim segundost As Long = ((residuo * 60) - Int(residuo * 60)) * 60
            If (Horast < 0 Or minutost < 0 Or segundost < 0) Then
            Else
                TextBox2.Text = Horast & ":" & minutost & ":" & segundost
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim inicio As DateTime = Date.Now
        Dim horas2 As Integer = (inicio.Hour)
        Dim minutos2 As Integer = (inicio.Minute)
        Dim segundos2 As Integer = inicio.Second
        Dim tiempo As String = horas2 & ":" & minutos2 & ":" & segundos2
        Dim fecha = Date.Now.Year & "/" & Date.Now.Month & "/" & Date.Now.Day



        conexion.consultas("INSERT INTO `tiempos` (`Id`, `Equipo`, `Inicio`, `Fin`, `Fecha`, `Observacion`,`Monto`) VALUES (NULL, '" & ComboBox1.Text & "', '" & tiempo & "', '" & TextBox2.Text & "', '" & fecha & "', '','" & TextBox1.Text & "');
", "")
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged

    End Sub

    Private Sub btndetener_Click(sender As Object, e As EventArgs) Handles btndetener.Click
        If (TextBox3.Text <> "") Then
            Dim horafin As DateTime = Date.Now
            Dim horas2 As Integer = (horafin.Hour)
            Dim minutos2 As Integer = (horafin.Minute)
            Dim segundos2 As Integer = horafin.Second
            Dim tiempo As String = horas2 & ":" & minutos2 & ":" & segundos2
            conexion.consultas("UPDATE tiempos SET Fin='" & tiempo & "' WHERE Id=" & TextBox3.Text & "", "")
            TextBox3.Text = ""
        Else
            MsgBox("Selecciona un Id")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox4.Text <> "") Then
            conexion.consultas("UPDATE tiempos SET Observacion='" & RichTextBox1.Text & "' WHERE Id=" & TextBox4.Text & "", "")
            RichTextBox1.Text = ""
            TextBox4.Text = ""
        Else
            MsgBox("Selecciona un Id")
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

        diafecha = DateTimePicker1.Value.Year & "/" & DateTimePicker1.Value.Month & "/" & DateTimePicker1.Value.Day
    End Sub
End Class

