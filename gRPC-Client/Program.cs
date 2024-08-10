using Grpc.Net.Client;
using gRPC_Message_Client;

var channel = GrpcChannel.ForAddress("https://localhost:7205/");

//var greetClient = new Greeter.GreeterClient(channel);
//HelloReply reply = await greetClient.SayHelloAsync(new HelloRequest { Name = "Puyz" });

var messageClient = new Message.MessageClient(channel);
MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest { 
    Username = "Puyz",
    Message = "Merhaba"
});

Console.WriteLine(response.Message);

Console.ReadLine();