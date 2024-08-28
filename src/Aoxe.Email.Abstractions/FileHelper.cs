namespace Aoxe.Email.Abstractions;

public static class FileHelper
{
    public static (string, byte[]) LoadFileBytes(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
            throw new FileNotFoundException($"File {filePath} not found.");
        using var fileStream = fileInfo.OpenRead();
        var bytes = new byte[fileStream.Length];
        _ = fileStream.Read(bytes, 0, bytes.Length);
        return (fileInfo.Name, bytes);
    }
}
