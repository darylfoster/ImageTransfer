# ImageTransfer

## Description
Simple file transfer project defining a server and a client. The server is responsible for receiving a file from the client and saving it to disk. The client is responsible for sending a file to the server. The data transmission from the client to the server is gzip compressed.

## Subprojects
### WebHost - HTTP host application
Start the host application by executing the following command from the ImageTransfer/WebHost directory:
```
dotnet run
```
The endpoint is available at http://localhost:5000/ImageTransfer. (The port will vary depending on the environment.)

Uploaded files are saved to the ImageTransfer/WebHost/uploads directory.
### WebClient - console client application
To upload a file, execute the following command from the ImageTransfer/WebClient directory:
```
dotnet run <file path> <host url>
```
Example:
```
dotnet run C:\Users\user\Desktop\image.jpg http://localhost:5000/ImageTransfer
```