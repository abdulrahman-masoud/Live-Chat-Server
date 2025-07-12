// ChatClient.cs
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class ChatClient
{
    static async Task Main()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        using TcpClient client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();

        // Send username
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        await stream.WriteAsync(nameBytes, 0, nameBytes.Length);

        // Start receiving task
        var cancellationTokenSource = new CancellationTokenSource();
        Task receiveTask = ReceiveMessagesAsync(stream, cancellationTokenSource.Token);

        // Sending loop
        while (true)
        {
            string message = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(message)) break;

            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }

        cancellationTokenSource.Cancel();
        await receiveTask;
    }

    static async Task ReceiveMessagesAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[1024];
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                if (bytesRead == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("\n" + message);
                Console.Write("> ");
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when cancellation is requested
        }
        catch
        {
            Console.WriteLine("Disconnected from server.");
        }
    }
}
