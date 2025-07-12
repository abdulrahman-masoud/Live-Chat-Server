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
Live-Chat-Server/
├── ChatServer/
│   ├── Program.cs          # TCP server implementation
│   └── ChatServer.csproj   # Server project file
├── ChatClient/
│   ├── Program.cs          # TCP client implementation
│   └── ChatClient.csproj   # Client project file
└── README.md               # This file
```

## Prerequisites

- .NET 8.0 SDK or later
- Windows, Linux, or macOS
- Terminal/Command Prompt access

## Installation & Setup

1. **Clone or download the project**
   ```bash
   git clone <repository-url>
   cd Live-Chat-Server
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

1. **Open a terminal and navigate to the server directory:**
   ```bash
   cd ChatServer
   ```

2. **Run the server:**
   ```bash
   dotnet run
   ```

3. **You should see:**
   ```
   Server started on port 5000...
   ```

### Connecting Clients

1. **Open a new terminal window/tab and navigate to the client directory:**
   ```bash
   cd ChatClient
   ```

2. **Run the client:**
   ```bash
   dotnet run
   ```

3. **Enter your username when prompted:**
   ```
   Enter your name: YourName
   ```

4. **Start chatting!** Type messages and press Enter to send.

5. **To disconnect:** Press Enter on an empty line.

### Running Multiple Clients

Open additional terminal windows and repeat the client connection steps to simulate multiple users chatting simultaneously.

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

### Running Multiple Instances for Testing
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

### Configuration Options
- **Server Port**: Default is 5000, modify in `ChatServer/Program.cs`
- **Buffer Size**: Default is 1024 bytes for message buffers
- **Server Address**: Currently set to `127.0.0.1` (localhost) for local testing

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

## Getting Started

1. **Download and install .NET 8.0 SDK** from [Microsoft's official website](https://dotnet.microsoft.com/download)
2. **Clone this repository** or download the source code
3. **Follow the Installation & Setup instructions** above
4. **Start the server first**, then connect multiple clients to test the chat functionality

