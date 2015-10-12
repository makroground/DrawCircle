Imports System.Drawing.Drawing2D

Public Class frm_gameWindow

    Dim graphics(99) As Graphics                                                                                        ' Graphics Objekt, welches den Kreis darstellt
    Dim maxRadius As Integer = 20                                                                                   ' Maximale Größe des Kreises (Wird nur bei einem "Fail" dargestellt)
    Dim currentRadius As Integer = 0                                                                                ' Beinhaltet die aktuelle Größe des Kreises, der von 0 bis 'maxRadius' variiert
    Dim circlePos(99) As Point                                                                                          ' Aktuelle Koordinaten des Kreises innerhalb der Form (begrenzter Zufallswert)
    Dim cPresent As Boolean = False                                                                                 ' Gibt an, ob sich zum Zeitpunkt ein Kreis auf der Form befindet
    Dim cGrowing As Boolean = False                                                                                 ' Gibt an, ob sich der Kreis gerade vergrößert oder verkleinert

    Dim speed As Integer = 1                                                                                        ' Schwellwert für das Wachsen des Kreises (bezogen auf den Radius) pro Timerdurchlauf
    Dim level As Integer = 1                                                                                        ' Level im Spiel
    Dim numberOfCircles As Integer = 2                                                                              ' Anzahl der gleichzeitig dargestellten Kreise im Spiel
    Dim cPoints As Integer
    Dim points As Integer                                                                                           ' Spielpunkte pro Level
    Dim failes As Integer                                                                                           ' Quantität der nicht getroffenen/angeklickten Kreise
    Dim maxFailes As Integer = 10                                                                                   ' Maximal mögliche 'failes' bis zum Verlieren des Spieles
    Dim catched As Boolean = False                                                                                  ' Gibt an, ob der aktuelle Kreis getroffen wurde
    Dim catchedCircle As Graphics

    Dim wallTop = 0                                                                                                 ' Platzhalter für den oberen Beginn des Spielfeldes
    Dim wallBottom = 0                                                                                              ' Platzhalter für das untere Ende des Spielfeldes
    Dim wallStart = 0                                                                                               ' Platzhalter für den linken Beginn des Spielfeldes
    Dim wallStop = 0                                                                                                ' Platzhalter für das rechte Ende des Spielfeldes

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_startGame.Click

        Timer1.Start()                                                                                              ' Spiel starten
        btn_startGame.Visible = False                                                                               ' Start Knopf ausblenden, solange das Spiel läuft

    End Sub


    Private Sub DrawACircle(ByVal radius As Integer, ByVal level As Integer)

        wallTop = 100
        wallBottom = Me.Height - 100
        wallStart = 100
        wallStop = Me.Width - 100

        For i As Integer = 0 To (level - 1) Step 1
            Dim rndX As Integer = CInt(Int(((wallStop - wallStart + 1) * Rnd()) + wallStart))
            Dim rndY As Integer = CInt(Int(((wallBottom - wallTop + 1) * Rnd()) + wallTop))
            circlePos(i) = New Point(rndX, rndY)

            graphics(i) = Me.CreateGraphics
            Dim xCenter As Integer = circlePos(i).X - radius
            Dim yCenter As Integer = circlePos(i).Y - radius
            Dim diameter As Integer = radius * 2

            'Dim pn As New Pen(Color.Red)
            Dim gp As GraphicsPath = New GraphicsPath
            gp.AddEllipse(xCenter, yCenter, diameter, diameter)
            graphics(i).SetClip(gp, CombineMode.Replace)

            Dim rect As New Rectangle(xCenter, yCenter, diameter, diameter)
            'graphics.DrawEllipse(pn, rect)
            graphics(i).FillEllipse(Brushes.Black, rect)
        Next i

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        

        If (level = 1 And points = 3000) Then
            level = 2
            points = 0
            numberOfCircles = level
        End If

        For i As Integer = 0 To level - 1 Step 1
            If (failes >= maxFailes) Then
                If (graphics(i).IsClipEmpty = False) Then
                    graphics(i).Clear(Color.Beige)
                End If
                cPresent = False
                MsgBox("Du bist schlecht.")
                Timer1.Stop()
            ElseIf (catched) Then
                If (graphics(i).IsClipEmpty = False) Then
                    graphics(i).Clear(Color.Beige)
                End If
                cPresent = False
                currentRadius = 0
                points += 100
                lbl_points.Text = points.ToString
                catched = False
            Else
                If (cPresent) Then
                    If cGrowing Then
                        If (currentRadius <= maxRadius) Then
                            currentRadius += speed
                            If (graphics(i).IsClipEmpty = False) Then
                                graphics(i).Clear(Color.Beige)
                            End If
                            DrawACircle(currentRadius, level)
                        ElseIf (currentRadius > maxRadius) Then
                            cGrowing = False
                        End If
                    Else
                        If (currentRadius > 0) Then
                            currentRadius -= speed
                            If (graphics(i).IsClipEmpty = False) Then
                                graphics(i).Clear(Color.Beige)
                            End If
                            DrawACircle(currentRadius, level)
                        Else
                            failes += 1
                            lbl_fails.Text = failes.ToString
                            If (graphics(i).IsClipEmpty = False) Then
                                graphics(i).Clear(Color.Beige)
                            End If
                            currentRadius = 0
                            cPresent = False
                        End If
                    End If
                Else
                    DrawACircle(currentRadius, level)
                    cPresent = True
                    cGrowing = True
                End If
            End If

        Next i


    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        For i As Integer = 0 To level - 1 Step 1
            If Not graphics(i) Is Nothing Then
                If graphics(i).IsVisible(e.X, e.Y) Then
                    'MsgBox("Circle was clicked")
                    catchedCircle = graphics(i)
                    catched = True
                End If
            End If
        Next i

    End Sub

End Class


