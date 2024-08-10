using Grpc.Core;
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

// Server Streaming
AsyncServerStreamingCall<MessageResponse> response = messageClient.SendMessage(new MessageRequest
{
    Username = "Puyz",
    Message = "Merhaba"
});

CancellationTokenSource tokenSource = new();

while (await response.ResponseStream.MoveNext(tokenSource.Token))
{
    Console.WriteLine(response.ResponseStream.Current.Message);
}

Console.ReadLine();