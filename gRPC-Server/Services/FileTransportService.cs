﻿using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpcFileTransportServer;

namespace gRPCServer.Services
{
    public class FileTransportService : FileService.FileServiceBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileTransportService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public override async Task<Empty> FileUpload(IAsyncStreamReader<BytesContent> requestStream, ServerCallContext context)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "FileStorage");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileStream fileStream = null;

            try
            {
                int count = 0;
                decimal chunkSize = 0;
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    if (count++ == 0)
                    {
                        fileStream = new FileStream($"{path}/{requestStream.Current.Info.FileName}{requestStream.Current.Info.FileExtension}", FileMode.CreateNew);
                        fileStream.SetLength(requestStream.Current.FileSize);
                    }

                    var buffer = requestStream.Current.Buffer.ToByteArray();
                    await fileStream.WriteAsync(buffer, 0, requestStream.Current.ReadedByte);

                    Console.Write("\r{0:N2}%", (chunkSize += requestStream.Current.ReadedByte) * 100 / requestStream.Current.FileSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (fileStream != null)
            {
                await fileStream.DisposeAsync();
                fileStream.Close();
            }
            return new Empty();
        }

        public override async Task FileDownload(grpcFileTransportServer.FileInfo request, IServerStreamWriter<BytesContent> responseStream, ServerCallContext context)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "FileStorage");

            using FileStream fileStream = new($"{path}/{request.FileName}{request.FileExtension}", FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[2048];

            BytesContent content = new()
            {
                FileSize = fileStream.Length,
                Info = new grpcFileTransportServer.FileInfo
                {
                    FileName = Path.GetFileNameWithoutExtension(fileStream.Name),
                    FileExtension = Path.GetExtension(fileStream.Name)
                },
                ReadedByte = 0
            };


            while ((content.ReadedByte = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                content.Buffer = ByteString.CopyFrom(buffer);
                await responseStream.WriteAsync(content);
            }
            fileStream.Close();
        }
    }
}
