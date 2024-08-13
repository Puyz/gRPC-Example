using gRPC_Client;



ClientFileService fileService = new();


// Upload
string file = @"filePath/file_name.extension";
await fileService.Upload(file);


// Download
await fileService.Download("file_name.extension");

Console.ReadLine();

