' Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

Imports Windows.Devices.Enumeration
Imports Windows.Devices.PointOfService
''' <summary>
''' Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Dim devices As DeviceInformationCollection
    Private Async Function btnDisplay_ClickAsync(sender As Object, e As RoutedEventArgs) As Task
        Using lineDisplay = Await ClaimedLineDisplay.FromIdAsync(devices.First.Id)


            Dim position = New Point(1, 0)

            Dim attribute = LineDisplayTextAttribute.Normal

            If Not lineDisplay.Capabilities.CanBlink = LineDisplayTextAttributeGranularity.NotSupported Then

                attribute = LineDisplayTextAttribute.Blink
            End If

            Await lineDisplay.DefaultWindow.TryClearTextAsync()
            Await lineDisplay.DefaultWindow.TryDisplayTextAsync(text1.Text, attribute, position)

        End Using
    End Function

    Private Sub MainPage_Loading(sender As FrameworkElement, args As Object) Handles Me.Loading

    End Sub

    Private Async Function MainPage_LoadedAsync(sender As Object, e As RoutedEventArgs) As Task Handles Me.Loaded
        devices = Await DeviceInformation.FindAllAsync(LineDisplay.GetDeviceSelector(PosConnectionTypes.All))
        If devices.Count > 0 Then btnDisplay.Visibility = Visibility.Visible


    End Function
End Class
