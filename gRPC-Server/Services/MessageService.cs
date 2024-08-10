using Grpc.Core;
using gRPC_Message_Server;

namespace gRPC_Server.Services;

public class MessageService : Message.MessageBase
{

    public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {
        Console.WriteLine($"User: {request.Username} | Message: {request.Message}");

        for (int i = 0; i <= 10; i++)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(new MessageResponse
            {
                Message = $"{i} -> The message was received successfully."
            });
        }
    }
   
}

