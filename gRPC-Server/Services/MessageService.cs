using Grpc.Core;
using gRPC_Message_Server;

namespace gRPC_Server.Services;

public class MessageService : Message.MessageBase
{
    /* Unary Service
      
    public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        Console.WriteLine($"User: {request.Username} -> Message: {request.Message}");
        return new MessageResponse
        {
            Message = "The message was received successfully."
        };
    } 
     
     */

    /* Server Streaming Service
     
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
   */

    /* Client Streaming Service
    public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            Console.WriteLine($"User: {requestStream.Current.Username} | Message: {requestStream.Current.Message}");
        }
        return new MessageResponse
        {
            Message = "The message was received successfully."
        };
    }
    */

    // Bi-directional Streaming Service
    public override async Task SendMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {
        var task = Task.Run(async () =>
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"User: {requestStream.Current.Username} | Message: {requestStream.Current.Message}");
            }
        });

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(new MessageResponse
            {
                Message = $"{i}. The message was received successfully."
            });
        }

        await task;
    }
}

