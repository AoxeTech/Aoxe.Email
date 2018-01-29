# Zaabee.EmailUtility

The fluent style wrappers for SmtpClient.

```csharp
public void Test()
{
        var emailHelper = new EmailHelper();
        var fileStream = FileToStream(@"D:\test.txt");
        emailHelper.Host("Your SMTP server's IP.")
        .Port(25)
        .UserName("The userName for NetworkCredential")
        .Password("The password for NetworkCredential")
        .From("From address")
        .To(new List<string> {"123@live.com", "456@gmail.com"})
        .To("789@yahoo.com")
        .Cc("987@hotmail.com")
        .Bcc("654@msn.com")
        .Subject($"email test({DateTime.Now}+{Guid.NewGuid()})")
        .Body(@"Across the Great Wall we can reach every corner in the world.")
        .Attachment(fileStream, "test_attachment")
        .Send();
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