## Create gRPC Server

```bash
dotnet new grpc --name project_name
```

## .csproj
```xml
<ItemGroup>
    <Protobuf Include="Protos\greet.proto" GrpcServices="Server" />
    <Protobuf Include="Protos\message.proto" GrpcServices="Server" />
</ItemGroup>

<ItemGroup>
    <PackageReference Include="Grpc.AspNetCore" Version="2.49.0" />
</ItemGroup>
```