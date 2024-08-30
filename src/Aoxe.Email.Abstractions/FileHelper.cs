namespace Aoxe.Email.Abstractions;

public static class FileHelper
{
    public static async ValueTask<(string, byte[])> LoadFileBytesAsync(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
            throw new FileNotFoundException($"File {filePath} not found.");

        using var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read
        );
        var bytes = new byte[fileStream.Length];
        _ = await fileStream.ReadAsync(bytes, 0, bytes.Length);
        return (fileInfo.Name, bytes);
    }
}
