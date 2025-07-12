# Live Chat Server

A real-time TCP-based chat application built with C# and .NET 8.0, featuring a multi-client server and console-based client interface.

## Features

- **Multi-client Support**: Multiple users can connect and chat simultaneously
- **Real-time Messaging**: Instant message delivery to all connected clients
- **User Join/Leave Notifications**: Visual indicators with emojis when users connect or disconnect
- **Async/Await Architecture**: Modern C# patterns for optimal performance
- **Cross-platform**: Runs on Windows, Linux, and macOS

## Project Structure

```
dotnet/
├── ChatServer/
│   ├── Program.cs          # TCP server implementation
│   └── ChatServer.csproj   # Server project file
├── ChatClient/
│   ├── Program.cs          # TCP client implementation
│   └── ChatClient.csproj   # Client project file
└── README.md               # This file
```

## Requirements

- .NET 8.0 SDK or later
- Windows, Linux, or macOS

## Installation & Setup

1. **Navigate to the project directory**
   ```bash
   cd /home/shoyo/socket_programming/dotnet
   ```

2. **Build the projects**
   ```bash
   # Build server
   cd ChatServer
   dotnet build
   cd ..
   
   # Build client
   cd ChatClient
   dotnet build
   cd ..
   ```

## Usage

### Starting the Server

1. Navigate to the server directory:
   ```bash
   cd ChatServer
   ```

2. Run the server:
   ```bash
   dotnet run
   ```

3. You should see:
   ```
   Server started on port 5000...
   ```

### Connecting Clients

1. Open a new terminal and navigate to the client directory:
   ```bash
   cd ChatClient
   ```

2. Run the client:
   ```bash
   dotnet run
   ```

3. Enter your username when prompted:
   ```
   Enter your name: YourName
   ```

4. Start chatting! Type messages and press Enter to send.

5. To disconnect, press Enter on an empty line.

### Multiple Clients

Repeat the client connection steps in separate terminal windows to simulate multiple users.

## Architecture

### Server (`ChatServer/Program.cs`)
- **TCP Listener**: Listens on port 5000 for incoming connections
- **Client Management**: Maintains a thread-safe list of connected clients
- **Message Broadcasting**: Sends messages to all connected clients except the sender
- **Async Operations**: Uses `async/await` for non-blocking I/O operations

### Client (`ChatClient/Program.cs`)
- **TCP Connection**: Connects to server at `127.0.0.1:5000`
- **Dual Threading**: Separate threads for sending and receiving messages
- **Cancellation Support**: Graceful shutdown with `CancellationToken`
- **Real-time Display**: Continuous listening for incoming messages

## Technical Details

### Communication Protocol
1. Client connects to server via TCP
2. First message sent is the username
3. Server broadcasts join notification: `📢 Username joined the chat`
4. All subsequent messages are broadcast as: `Username: message`
5. Leave notifications: `❌ Username left the chat`

### Key Features
- **Thread Safety**: Uses locks for client list management
- **Error Handling**: Graceful handling of client disconnections
- **Resource Management**: Proper disposal of network resources
- **Async I/O**: Non-blocking network operations

## Example Chat Session

```
Server Console:
Server started on port 5000...
Client connected.
📢 Alice joined the chat
Alice: Hello everyone!
📢 Bob joined the chat
Bob: Hi Alice!
Alice: How are you doing?
❌ Alice left the chat

Client Console (Alice):
Enter your name: Alice
📢 Alice joined the chat
> Hello everyone!
📢 Bob joined the chat
Bob: Hi Alice!
> How are you doing?

Client Console (Bob):
Enter your name: Bob
Alice: Hello everyone!
📢 Bob joined the chat
> Hi Alice!
Alice: How are you doing?
>
```

## Development

### Building from Source
```bash
# Restore dependencies
dotnet restore

# Build all projects
dotnet build

# Run tests (if any)
dotnet test
```

### Running Multiple Instances
```bash
# Terminal 1 - Start server
cd ChatServer && dotnet run

# Terminal 2 - First client
cd ChatClient && dotnet run

# Terminal 3 - Second client  
cd ChatClient && dotnet run

# Terminal 4 - Third client
cd ChatClient && dotnet run
```

### Configuration
- **Server Port**: Default is 5000, can be modified in `ChatServer/Program.cs`
- **Buffer Size**: Default is 1024 bytes for message buffers
- **Server IP**: Currently set to `127.0.0.1` (localhost)

## Code Structure

### Server Architecture
```csharp
// Main components:
- TcpListener: Accepts incoming connections
- HandleClientAsync(): Manages individual client connections
- Broadcast(): Sends messages to all connected clients
- Thread-safe client list management
```

### Client Architecture
```csharp
// Main components:
- TcpClient: Connects to server
- ReceiveMessagesAsync(): Listens for incoming messages
- Main thread: Handles user input
- CancellationToken: Graceful shutdown
```

## Troubleshooting

**Connection Issues:**
- Ensure the server is running before starting clients
- Check firewall settings for port 5000
- Verify no other applications are using port 5000
- Use `netstat -tulpn | grep 5000` to check port usage

**Build Issues:**
- Ensure .NET 8.0 SDK is installed: `dotnet --version`
- Try `dotnet clean` followed by `dotnet build`
- Check for missing dependencies: `dotnet restore`

**Runtime Issues:**
- If client can't connect, verify server IP address (127.0.0.1)
- Check console output for error messages
- Ensure proper network connectivity

## Performance Notes

- **Scalability**: Can handle multiple concurrent connections
- **Memory Usage**: ~1KB buffer per client connection
- **Thread Usage**: One thread per client + main server thread
- **Network**: TCP ensures reliable message delivery

## License

This project is for educational purposes demonstrating TCP socket programming with C# and .NET.

## Related Projects

This workspace also contains:
- `../py/`: Python UDP socket implementation (simple echo server)
- `../chat server repo/`: Alternative implementation

## Contributing

Feel free to fork and modify this code for learning purposes. Key areas for enhancement:
- Add message encryption
- Implement user authentication
- Add private messaging
- Create a GUI client
- Add message history/logging
