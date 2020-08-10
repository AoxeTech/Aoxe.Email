# Zaabee.SmtpClient

## Quick start

```CSharp
public void Demo()
{
    var stmpClientHelper = new StmpClientHelper();
    var fileStream = FileToStream(@"D:\test_attachment.txt");
    var mail = new Mail();
    mail.From("From address")
        .Subject($"email test({DateTime.Now}+{Guid.NewGuid()})")
        .IsBodyHtml(true)
        .BodyEncoding(Encoding.UTF8)
        .Body(@"Across the Great Wall we can reach every corner in the world.")
        .Priority(MailPriority.High)
        .To(new List<string> {"123@live.com", "456@gmail.com"})
        .Cc("987@hotmail.com")
        .Bcc("654@msn.com")
        .Attachment(fileStream, "test_attachment.txt");
    stmpClientHelper.Host("Your SMTP server's IP.")
        .Port(587)
        .UserName("The userName for NetworkCredential")
        .Password("The password for NetworkCredential")
        .Ssl(true)
        .DeliveryMethod(SmtpDeliveryMethod.Network)
        .DeliveryFormat(SmtpDeliveryFormat.SevenBit)
        .Timeout(TimeSpan.FromSeconds(100))
        .Send(mail);
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
