# Zaabee.SmtpClient

The fluent style wrappers for MailKit.

```csharp
[Fact]
public void Test()
{
        var mailKitHelper = new MailKitHelper();
        var sendMessage = new SendMessage();
        var fileStream = FileToStream(@"D:\test_attachment.txt");
        mailKitHelper.Host("Your SMTP server's IP.")
        .Port(587)
        .UserName("The user name for NetworkCredential")
        .Password("The password for NetworkCredential")
        .Ssl(true)
        .Send(sendMessage.From("From email")
                .Subject($"email test({DateTime.Now}+{Guid.NewGuid()})")
                .Body(@"Across the Great Wall we can reach every corner in the world.")
                .To(new List<string> {"123@live.com", "456@gmail.com"})
                .Cc("789@hotmail.com")
                .Bcc("123@163.com")
                .Attachment(fileStream, "test_attachment.txt"));
}

private static Stream FileToStream(string fileName)
{
        var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        var bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        fileStream.Close();
        return new MemoryStream(bytes);
}
```