' Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

Imports Microsoft.AspNet.SignalR.Client
Imports Newtonsoft.Json
Imports Windows.Devices.Enumeration
Imports Windows.Devices.PointOfService
''' <summary>
''' Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Dim devices As DeviceInformationCollection
    Private Async Function btnDisplay_ClickAsync(sender As Object, e As RoutedEventArgs) As Task
        Await DisplayPOS("Demo click")
    End Function

    Private Async Function DisplayPOS(msg As String) As Task
        Using lineDisplay = Await ClaimedLineDisplay.FromIdAsync(devices.First.Id)


            Dim position = New Point(0, 0)

            Dim attribute = LineDisplayTextAttribute.Normal

            If Not lineDisplay.Capabilities.CanBlink = LineDisplayTextAttributeGranularity.NotSupported Then

                attribute = LineDisplayTextAttribute.Blink
            End If

            Await lineDisplay.DefaultWindow.TryClearTextAsync()
            Await lineDisplay.DefaultWindow.TryDisplayTextAsync(msg, attribute, position)

        End Using
    End Function

    Private Sub MainPage_Loading(sender As FrameworkElement, args As Object) Handles Me.Loading
        Dim myApp = CType(Application.Current, App)
        AddHandler myApp.MyHubConnection.Received, AddressOf GotMsgAsync
        If myApp.MyHubConnection.State <> ConnectionState.Connected Then


            myApp.MyHubConnection.Start()
        End If

    End Sub

    Private Async Function GotMsgAsync(json As String) As Task
        Dim msg = JsonConvert.DeserializeObject(Of HubMsg)(json)

        Await DisplayPOS(msg.LA.First.Meldung)
    End Function

    Private Async Function MainPage_LoadedAsync(sender As Object, e As RoutedEventArgs) As Task Handles Me.Loaded
        devices = Await DeviceInformation.FindAllAsync(LineDisplay.GetDeviceSelector(PosConnectionTypes.All))
        If devices.Count > 0 Then btnDisplay.Visibility = Visibility.Visible


    End Function
End Class
