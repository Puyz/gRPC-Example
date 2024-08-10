using Grpc.Net.Client;
using gRPC_Server;

var channel = GrpcChannel.ForAddress("https://localhost:7205/");
var greetClient = new Greeter.GreeterClient(channel);

HelloReply reply = await greetClient.SayHelloAsync(new HelloRequest { Name = "Puyz" });

Console.WriteLine(reply.Message);

Console.ReadLine();