using Grpc.Net.Client;
using gRPC_Message_Client;

var channel = GrpcChannel.ForAddress("https://localhost:7205/");

//var greetClient = new Greeter.GreeterClient(channel);
//HelloReply reply = await greetClient.SayHelloAsync(new HelloRequest { Name = "Puyz" });

var messageClient = new Message.MessageClient(channel);

/* Unary
 
var response = await messageClient.SendMessageAsync(new MessageRequest { 
    Username = "Puyz",
    Message = "Merhaba"
});
Console.WriteLine(response.Message);
*/

/* Server Streaming
 
MessageResponse response = messageClient.SendMessage(new MessageRequest
{
    Username = "Puyz",
    Message = "Merhaba"
});

CancellationTokenSource tokenSource = new();

while (await response.ResponseStream.MoveNext(tokenSource.Token))
{
    Console.WriteLine(response.ResponseStream.Current.Message);
}
*/

/* Client Streaming
 
var request = messageClient.SendMessage();
for (int i = 0; i < 5; i++)
{
    await Task.Delay(1000);
    await request.RequestStream.WriteAsync(new MessageRequest
    {
        Username = "Ömer",
        Message = $"{i}. Mesaj"
    });
}

// Stream data'nın sonlandığını bildiriyoruz.
await request.RequestStream.CompleteAsync();

Console.WriteLine((await request.ResponseAsync).Message);
*/

// Bi-directional Streaming

var request = messageClient.SendMessage();
var taskRequest = Task.Run(async () =>
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(1000);
        await request.RequestStream.WriteAsync(new MessageRequest
        {
            Username = "Ömer",
            Message = $"{i}. Mesaj"
        });
    }
    
});


CancellationTokenSource cancellationToken = new();

while (await request.ResponseStream.MoveNext(cancellationToken.Token))
{
    Console.WriteLine(request.ResponseStream.Current.Message);
}

await taskRequest;
await request.RequestStream.CompleteAsync();












Console.ReadLine();