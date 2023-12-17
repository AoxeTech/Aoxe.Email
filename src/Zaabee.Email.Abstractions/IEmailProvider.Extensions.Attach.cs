namespace Zaabee.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider Attach(
        this IEmailProvider emailProvider,
        string filePath,
        string? contentType = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        var (name, content) = GetAttachment(filePath);
        emailProvider
            .Email
            .Attachments
            .Add(
                new EmailAttachment
                {
                    Name = name,
                    ContentType = contentType ?? string.Empty,
                    Content = content
                }
            );
        return emailProvider;
    }

    public static IEmailProvider Attach(
        this IEmailProvider emailProvider,
        IEnumerable<string> filePaths,
        string? contentType = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider
            .Email
            .Attachments
            .AddRange(
                filePaths.Select(x =>
                {
                    var (name, content) = GetAttachment(x);
                    return new EmailAttachment
                    {
                        Name = name,
                        ContentType = contentType ?? string.Empty,
                        Content = content
                    };
                })
            );
        return emailProvider;
    }

    public static IEmailProvider Attach(
        this IEmailProvider emailProvider,
        string fileName,
        byte[] content,
        string? contentType = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider
            .Email
            .Attachments
            .Add(
                new EmailAttachment
                {
                    Name = fileName,
                    ContentType = contentType ?? string.Empty,
                    Content = content
                }
            );
        return emailProvider;
    }

    public static IEmailProvider Attach(
        this IEmailProvider emailProvider,
        IEnumerable<(string fileName, byte[] content)> attachments,
        string? contentType = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider
            .Email
            .Attachments
            .AddRange(
                attachments.Select(
                    x =>
                        new EmailAttachment
                        {
                            Name = x.fileName,
                            ContentType = contentType ?? string.Empty,
                            Content = x.content
                        }
                )
            );
        return emailProvider;
    }

    private static (string, byte[]) GetAttachment(string filePath)
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
