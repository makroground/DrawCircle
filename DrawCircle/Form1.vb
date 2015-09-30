Imports System.Drawing.Drawing2D

Public Class Form1

    Dim graphics As Graphics
    Dim circleRadius As Integer = 50
    Dim cPresent = False

    Dim level As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
        'DrawACircle(New Point(100, 100), 30)
    End Sub

    Private Sub Draw(e As PaintEventArgs)
    End Sub

    Private Sub DrawACircle(ByRef center As Point, ByVal radius As Integer)

        graphics = Me.CreateGraphics

        'Dim pn As New Pen(Color.Blue)
        Dim gp As GraphicsPath = New GraphicsPath
        gp.AddEllipse(500, 500, 30, 30)

        Dim rect As New Rectangle(center.X / 2, center.Y / 2, radius * 2, radius * 2)
        'graphics.DrawEllipse(pn, rect)
        graphics.FillEllipse(Brushes.Black, rect)
        graphics.SetClip(gp, CombineMode.Replace)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (cPresent) Then
            If (graphics.IsClipEmpty = False) Then
                graphics.Clear(Color.Beige)
            End If
            cPresent = False
        Else
            DrawACircle(New Point(1000, 1000), 15)
            cPresent = True
        End If
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If (graphics.IsVisible(e.X, e.Y)) Then
            MsgBox("Circle was clicked")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If (graphics.IsClipEmpty = False) Then
        'graphics.Clear(Color.White)
        'End If
        Timer1.Stop()
    End Sub
End Class
