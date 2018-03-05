

Imports Newtonsoft.Json

Public Class HubMsg
    Public Property H As String
    Public Property M As String
    <JsonProperty("A")>
    Public Property LA As List(Of A)
End Class

Public Class A
    Public Property Id As Integer
    Public Property Meldung As String
    Public Property Datum As Date
End Class
