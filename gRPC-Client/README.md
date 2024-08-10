# Client için gerekli paketler

- **Google.Protobuf:** Protobuf serialization ve deserialization iþlemlerini yapan kütüphane.
- **Grpc.Net.Client:** .NET Mimarisine uygun gRPC kütüphanesi.
- **Grpc.Tools:** Proto dosyalarýný derlemek için gerekli compiler'ý ve diðer araçlarý içeren kütüphane.

## .csproj
```xml
<ItemGroup>
    <Protobuf Include="Protos\greet.proto" GrpcServices="Client" />
    <Protobuf Include="Protos\message.proto" GrpcServices="Client" />
  </ItemGroup>

<ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.28.0-rc2" />
    <PackageReference Include="Grpc.Net.Client" Version="2.65.0" />
    <PackageReference Include="Grpc.Tools" Version="2.66.0-pre3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
  </ItemGroup>
```
