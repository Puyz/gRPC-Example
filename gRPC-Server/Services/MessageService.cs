using Grpc.Core;
using gRPC_Message_Server;

namespace gRPC_Server.Services;

public class MessageService : Message.MessageBase
{
    public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        Console.WriteLine($"User: {request.Username} -> Message: {request.Message}");
        return new MessageResponse
        {
            Message = "The message was received successfully."
        };
    } 
}

