Imports System.Drawing.Drawing2D

Public Class Form1

    Dim graphics As Graphics                                                                                        ' Graphics Objekt, welches den Kreis darstellt
    Dim maxRadius As Integer = 30                                                                                   ' Maximale Größe des Kreises (Wird nur bei einem "Fail" dargestellt)
    Dim currentRadius As Integer = 0                                                                                ' Beinhaltet die aktuelle Größe des Kreises, der von 0 bis 'maxRadius' variiert
    Dim circlePos As Point                                                                                          ' Aktuelle Koordinaten des Kreises innerhalb der Form (begrenzter Zufallswert)
    Dim cPresent = False                                                                                            ' Gibt an, ob sich zum Zeitpunkt ein Kreis auf der Form befindet

    Dim speed As Integer = 1                                                                                        ' Schwellwert für das Wachsen des Kreises (bezogen auf den Radius) pro Timerdurchlauf
    Dim level As Integer                                                                                            ' Level im Spiel
    Dim points As Integer                                                                                           ' Spielpunkte pro Level
    Dim failes As Integer                                                                                           ' Quantität der nicht getroffenen/angeklickten Kreise
    Dim maxFailes As Integer = 10                                                                                   ' Maximal mögliche 'failes' bis zum Verlieren des Spieles
    Dim catched As Boolean = False                                                                                  ' Gibt an, ob der aktuelle Kreis getroffen wurde

    Dim wallTop = 0                                                                                                 ' Platzhalter für den oberen Beginn des Spielfeldes
    Dim wallBottom = 0                                                                                              ' Platzhalter für das untere Ende des Spielfeldes
    Dim wallStart = 0                                                                                               ' Platzhalter für den linken Beginn des Spielfeldes
    Dim wallStop = 0                                                                                                ' Platzhalter für das rechte Ende des Spielfeldes

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Timer1.Start()                                                                                              ' Spiel starten
        Button1.Visible = False                                                                                     ' Start Knopf ausblenden, solange das Spiel läuft

    End Sub


    Private Sub DrawACircle(ByRef center As Point, ByVal radius As Integer)

        graphics = Me.CreateGraphics
        Dim xCenter As Integer = center.X - radius
        Dim yCenter As Integer = center.Y - radius
        Dim diameter As Integer = radius * 2

        'Dim pn As New Pen(Color.Red)
        Dim gp As GraphicsPath = New GraphicsPath
        gp.AddEllipse(xCenter, yCenter, diameter, diameter)
        graphics.SetClip(gp, CombineMode.Replace)

        Dim rect As New Rectangle(xCenter, yCenter, diameter, diameter)
        'graphics.DrawEllipse(pn, rect)
        graphics.FillEllipse(Brushes.Black, rect)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        wallTop = 100
        wallBottom = Me.Height - 100
        wallStart = 100
        wallStop = Me.Width - 100

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
            currentRadius = 0
            points += 100
            Label5.Text = points.ToString
            catched = False
        Else
            If (cPresent) Then
                If (currentRadius <= maxRadius) Then
                    currentRadius += speed
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
                    currentRadius = 0
                    cPresent = False
                End If
            Else
                Dim rndX As Integer = CInt(Int(((wallStop - wallStart + 1) * Rnd()) + wallStart))
                Dim rndY As Integer = CInt(Int(((wallBottom - wallTop + 1) * Rnd()) + wallTop))
                circlePos = New Point(rndX, rndY)
                DrawACircle(circlePos, currentRadius)
                cPresent = True
            End If
        End If

    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Not graphics Is Nothing Then
            If graphics.IsVisible(e.X, e.Y) Then
                'MsgBox("Circle was clicked")
                catched = True
            End If
        End If
    End Sub

End Class

