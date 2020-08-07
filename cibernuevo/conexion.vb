Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Public Class conexion
    Const usuario As String = "root"
    Const password As String = ""
    Const servidor As String = "localhost"
    Const bd As String = "bdciber"
    Dim datosservidor As String = "server=" & servidor & ";user id=" & usuario & ";password=" & password & ";database=" & bd & ";"
    Dim conexion As New MySqlConnection(datosservidor)

    Public Function consultas(ByVal query, ByVal notificacion) As DataSet
        Dim ds As New DataSet
        '   MsgBox(query)
        Try
            If Not conexion.State = ConnectionState.Open Then
                conexion.ConnectionString = datosservidor
                conexion.Open()
            End If
            Dim comando As New MySqlCommand(query, conexion)
            Dim adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(ds)
            adaptador.Dispose()
            conexion.Close()
            If notificacion = "" Then
            Else
                MsgBox(notificacion)
            End If
        Catch ex As Exception
            conexion.Close()
            MsgBox(ex.ToString)
            MsgBox("hola mundo")
        End Try
        Return ds
    End Function

End Class