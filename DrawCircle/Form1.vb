Imports System.Drawing.Drawing2D

Public Class Form1

    Dim graphics As Graphics
    Dim circleRadius As Integer = 30
    Dim currentRadius As Integer = circleRadius
    Dim circlePos As Point
    Dim cPresent = False

    Dim speed As Integer = 1
    Dim level As Integer
    Dim points As Integer
    Dim failes As Integer
    Dim maxFailes As Integer = 10
    Dim catched As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
        'DrawACircle(New Point(100, 100), 30)
    End Sub

    Private Sub Draw(e As PaintEventArgs)
    End Sub

    Private Sub DrawACircle(ByRef center As Point, ByVal radius As Integer)

        Dim xCenter As Integer = center.X - radius
        Dim yCenter As Integer = center.Y - radius
        Dim diameter As Integer = radius * 2
        graphics = Me.CreateGraphics

        Dim pn As New Pen(Color.Red)
        Dim gp As GraphicsPath = New GraphicsPath


        gp.AddEllipse(xCenter, yCenter, diameter, diameter)

        Dim rect As New Rectangle(xCenter, yCenter, diameter, diameter)
        'graphics.DrawEllipse(pn, rect)
        graphics.FillEllipse(Brushes.Black, rect)
        'graphics.DrawLine(pn, New Point(center.X - radius, center.Y), New Point(center.X + radius, center.Y))
        'graphics.DrawLine(pn, New Point(center.X, center.Y - radius), New Point(center.X, center.Y + radius))
        graphics.SetClip(gp, CombineMode.Replace)
        'Debug.WriteLine("x: " & xCenter & "y: " & yCenter)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (failes >= maxFailes) Then
            If (graphics.IsClipEmpty = False) Then
                graphics.Clear(Color.Beige)
            End If
            cPresent = False
            MsgBox("Looooosed!!!")
            Timer1.Stop()
        ElseIf (catched) Then
            If (graphics.IsClipEmpty = False) Then
                graphics.Clear(Color.Beige)
            End If
            cPresent = False
            currentRadius = circleRadius
            points += 100
            Label4.Text = points.ToString
            catched = False
        End If
        If (cPresent) Then
            If (currentRadius > 0) Then
                currentRadius -= speed
                If (graphics.IsClipEmpty = False) Then
                    graphics.Clear(Color.Beige)
                End If
                DrawACircle(circlePos, currentRadius)
            Else
                failes += 1
                Label2.Text = failes.ToString
                If (graphics.IsClipEmpty = False) Then
                    graphics.Clear(Color.Beige)
                End If
                currentRadius = circleRadius
                cPresent = False
            End If
        Else
            circlePos = New Point(200, 200)
            DrawACircle(circlePos, circleRadius)
            cPresent = True
        End If
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If (graphics.IsVisible(e.X, e.Y)) Then
            'MsgBox("Circle was clicked")
            catched = True
        End If
    End Sub

End Class

