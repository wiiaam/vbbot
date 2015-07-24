Imports System.Net.Sockets
Imports System.IO
Imports System.Text

Public Class Application
	Public Shared Dim client As New TcpClient()
	Public Shared Dim stream As Stream
    Public Shared Dim writer As StreamWriter
    Public Shared Dim reader As StreamReader

	Public Shared Sub Main()
		connect()
		join()
		listen()
	End Sub

	Public Shared Sub connect()
		client.connect("irc.rizon.net",6667)
		stream = client.getStream()
		writer = new StreamWriter(stream)
		reader = new StreamReader(stream)
		send("NICK vbbot")
		send("USER vbbot vbbot irc.rizon.net :get triggered gen2")
	End Sub

	Public Shared Sub listen()
		While True
            Dim line as String = reader.ReadLine()
            If Not line Is Nothing 
            	Try
            		Console.WriteLine((line))
            		If line.StartsWith("PING ")
	            		send("PONG " + line.Split(" ".ToCharArray())(1))
            		End If
            		Dim split As String() = line.Split(" ".ToCharArray())
					Console.WriteLine("HERE"+split(3)+"HERE")
            		If String.Compare(split(1), "PRIVMSG") = 0
            			If split(3).Equals(":.bots")
	            			msg(split(2),"Reporting in! [VB.Net]")
	            		End If	
           			End If
           		Catch exc as Exception
           			Console.WriteLine("Exception: " + exc.toString())
           		End Try
           	End If
		End While
	End Sub

	Public Shared Sub join()
		send("JOIN #taylorswift")
		send("JOIN #pasta")
	End Sub

	Public Shared Sub send(ByVal str as String)
		Dim enc As New UTF8Encoding
		Dim tosend As String = str + Constants.vbCrLf
		Dim sendBytes As [Byte]() = enc.GetBytes(tosend)
        stream.Write(sendBytes,	0, sendBytes.Length)
        Console.Write("Sending: " + tosend)
        stream.Flush()
	End Sub

	Public Shared Sub msg(ByVal target as String, ByVal str as String)
		send("PRIVMSG " + target + " :" + str)
	End Sub

	Public Shared Sub notice(ByVal target as String, ByVal str as String)
		send("NOTICE " + target + " :" + str)

	End Sub
End Class

