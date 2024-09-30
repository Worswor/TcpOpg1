// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System;
using TcpOpg1;

Console.WriteLine("TCP Server 1");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();
while (true)
{
	TcpClient socket = listener.AcceptTcpClient();
	IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
	Console.WriteLine("Client connected: " + clientEndPoint.Address);

	Task.Run(() => HandleClient(socket));
}

listener.Stop();

void HandleClient(TcpClient socket)
{
	NetworkStream ns = socket.GetStream();
	StreamReader reader = new StreamReader(ns);
	StreamWriter writer = new StreamWriter(ns);



	while (socket.Connected)
	{
		string? message = reader.ReadLine();
		Console.WriteLine(message);
		writer.WriteLine(message);
		writer.Flush();

		if (message == "stop")
		{
			socket.Close();
		}

		// Lets the user decide a random number between a choosen Max and Min value.
		if (message.StartsWith("random number"))
		{
			string firstNumber = reader.ReadLine();
			if (int.TryParse(firstNumber, out int faceValueOne))
			{
				string secondNumber = reader.ReadLine();
				if (int.TryParse(secondNumber, out int faceValueTwo))
				{
					if (faceValueOne <= faceValueTwo)
					{
						int faceValue = NumberGenerator.Generate(faceValueOne, faceValueTwo);
						writer.WriteLine($"Result: {faceValue}");
						writer.Flush();
					}
					else if (faceValueOne > faceValueTwo)
					{
						int faceValue = NumberGenerator.Generate(faceValueTwo, faceValueOne);
						writer.WriteLine($"Result: {faceValue}");
						writer.Flush();
					}
				}
			}
		}

		// Lets the user do addition with numbers
		if (message.StartsWith("add"))
		{
			string firstNumber = reader.ReadLine();
			if (int.TryParse(firstNumber, out int valueOne))
			{
				string secondNumber = reader.ReadLine();
				if (int.TryParse(secondNumber, out int valueTwo))
				{
					int result = valueOne + valueTwo;
					writer.WriteLine($"Result: {result}");
					writer.Flush();
				}
			}
		}

		// Lets the user do subtraction with numbers
		if (message.StartsWith("subtract"))
		{
			string firstNumber = reader.ReadLine();
			if (int.TryParse(firstNumber, out int valueOne))
			{
				string secondNumber = reader.ReadLine();
				if (int.TryParse(secondNumber, out int valueTwo))
				{
					int result = valueOne - valueTwo;
					writer.WriteLine($"Result: {result}");
					writer.Flush();
				}
			}
		}

		// In case the user write a useless command
		else
		{
			writer.WriteLine(message);
			// Clears all buffers. Remember to use this at the end.
			writer.Flush();
		}
	}
}