using Google.Protobuf;
using Grpc.Net.Client;
using grpcFileTransportClient;

namespace gRPC_Client
{
    public class ClientFileService
    {
        private readonly GrpcChannel _channel;
        private readonly FileService.FileServiceClient _fileServiceClient;
        public ClientFileService()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7205");
            _fileServiceClient = new(_channel);
        }

        public async Task Download(string fullFileName)
        {
            string downloadPath = @"DownloadPath\";

            grpcFileTransportClient.FileInfo fileInfo = new()
            {
                FileName = Path.GetFileNameWithoutExtension(fullFileName),
                FileExtension = Path.GetExtension(fullFileName)
            };

            FileStream fileStream = null;

            var download = _fileServiceClient.FileDownload(fileInfo);
            int count = 0;
            decimal chunkSize = 0;
            CancellationTokenSource tokenSource = new();
            while (await download.ResponseStream.MoveNext(tokenSource.Token))
            {
                if (count++ == 0)
                {
                    fileStream = new FileStream($"{downloadPath}/{download.ResponseStream.Current.Info.FileName}{download.ResponseStream.Current.Info.FileExtension}", FileMode.CreateNew);
                    fileStream.SetLength(download.ResponseStream.Current.FileSize);
                }

                var buffer = download.ResponseStream.Current.Buffer.ToByteArray();
                await fileStream.WriteAsync(buffer, 0, download.ResponseStream.Current.ReadedByte);
                Console.Write("\r{0:N2}%", (chunkSize += download.ResponseStream.Current.ReadedByte) * 100 / download.ResponseStream.Current.FileSize);
            }

            Console.WriteLine("Başarıyla indirildi.");
            await fileStream.DisposeAsync();
            fileStream.Close();
        }
        public async Task Upload(string file)
        {
            using FileStream fileStream = new(file, FileMode.Open);

            var content = new BytesContent
            {
                FileSize = fileStream.Length,
                Info = new grpcFileTransportClient.FileInfo { FileName = Path.GetFileNameWithoutExtension(file), FileExtension = Path.GetExtension(file) },
                ReadedByte = 0
            };

            var upload = _fileServiceClient.FileUpload();
            byte[] buffer = new byte[2048];

            while ((content.ReadedByte = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                content.Buffer = ByteString.CopyFrom(buffer);
                await upload.RequestStream.WriteAsync(content);
            }

            await upload.RequestStream.CompleteAsync();
            fileStream.Close();
        }


    }
}
