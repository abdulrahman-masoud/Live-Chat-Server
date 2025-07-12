// ChatServer.cs
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatServer
{
    static List<TcpClient> clients = new List<TcpClient>();
    static object lockObj = new object();

    static async Task Main()
    {
        int port = 5000;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Console.WriteLine($"Server started on port {port}...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            lock (lockObj) clients.Add(client);

            Console.WriteLine("Client connected.");
            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    static async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        string username = "";

        try
        {
            // First message is the username
            int nameLen = stream.Read(buffer, 0, buffer.Length);
            username = Encoding.UTF8.GetString(buffer, 0, nameLen);
            await Broadcast($"📢 {username} joined the chat", client);

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                await Broadcast($"{username}: {message}", client);
            }
        }
        catch
        {
            Console.WriteLine($"Error handling client {username}: {client.Client.RemoteEndPoint}");
            await Broadcast($"❌ {username} left the chat", client);    
        }
        finally
        {
            lock (lockObj) clients.Remove(client);
            await Broadcast($"❌ {username} left the chat", client);
            client.Close();
        }
    }

    static async Task Broadcast(string message, TcpClient sender)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);
        lock (lockObj)
        {
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.WriteAsync(msg, 0, msg.Length);
                    }
                    catch { }
                }
            }
        }
        Console.WriteLine(message);
    }
}
