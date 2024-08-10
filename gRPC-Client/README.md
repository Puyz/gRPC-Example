# Client i�in gerekli paketler

- **Google.Protobuf:** Protobuf serialization ve deserialization i�lemlerini yapan k�t�phane.
- **Grpc.Net.Client:** .NET Mimarisine uygun gRPC k�t�phanesi.
- **Grpc.Tools:** Proto dosyalar�n� derlemek i�in gerekli compiler'� ve di�er ara�lar� i�eren k�t�phane.

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
