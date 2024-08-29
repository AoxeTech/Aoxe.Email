namespace Aoxe.Email.Abstractions.TestProject;

public class EmailTest
{
    [Fact]
    public void IdTest()
    {
        var id = Guid.NewGuid().ToString();
        var email = new Models.Email(id);
        Assert.Equal(id, email.Id);
    }

    [Theory]
    [InlineData("From@Fake.com", "From")]
    public void FromTest(string address, string name)
    {
        var email = new Models.Email();
        email.EmailFrom(address, name);
        Assert.Equal(address, email.From.Address);
        Assert.Equal(name, email.From.Name);
    }

    [Fact]
    public void RecipientsTest()
    {
        var email = new Models.Email();
        Assert.NotNull(email.Recipients);
        Assert.Empty(email.Recipients.To);
        Assert.Empty(email.Recipients.Cc);
        Assert.Empty(email.Recipients.Bcc);
    }

    [Theory]
    [InlineData("To0@Fake.com", "To0", "To1@Fake.com", "To1", "To2@Fake.com", "To2")]
    public void ToTest(
        string address0,
        string name0,
        string address1,
        string name1,
        string address2,
        string name2
    )
    {
        var email = new Models.Email();
        email
            .EmailTo(address0, name0)
            .EmailTo([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.To.First().Address);
        Assert.Equal(name0, email.Recipients.To.First().Name);
        Assert.Equal(address1, email.Recipients.To.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.To.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.To.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.To.Skip(2).First().Name);
    }

    [Theory]
    [InlineData("Cc0@Fake.com", "Cc0", "Cc1@Fake.com", "Cc1", "Cc2@Fake.com", "Cc2")]
    public void CcTest(
        string address0,
        string name0,
        string address1,
        string name1,
        string address2,
        string name2
    )
    {
        var email = new Models.Email();
        email
            .EmailCc(address0, name0)
            .EmailCc([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.Cc.First().Address);
        Assert.Equal(name0, email.Recipients.Cc.First().Name);
        Assert.Equal(address1, email.Recipients.Cc.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.Cc.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.Cc.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.Cc.Skip(2).First().Name);
    }

    [Theory]
    [InlineData("Bcc0@Fake.com", "Bcc0", "Bcc1@Fake.com", "Bcc1", "Bcc2@Fake.com", "Bcc2")]
    public void BccTest(
        string address0,
        string name0,
        string address1,
        string name1,
        string address2,
        string name2
    )
    {
        var email = new Models.Email();
        email
            .EmailBcc(address0, name0)
            .EmailBcc([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.Bcc.First().Address);
        Assert.Equal(name0, email.Recipients.Bcc.First().Name);
        Assert.Equal(address1, email.Recipients.Bcc.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.Bcc.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.Bcc.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.Bcc.Skip(2).First().Name);
    }

    [Theory]
    [InlineData(
        "ReplyTo0@Fake.com",
        "ReplyTo0",
        "ReplyTo1@Fake.com",
        "ReplyTo1",
        "ReplyTo2@Fake.com",
        "ReplyTo2"
    )]
    public void ReplyToTest(
        string address0,
        string name0,
        string address1,
        string name1,
        string address2,
        string name2
    )
    {
        var email = new Models.Email();
        email
            .EmailReplyTo(address0, name0)
            .EmailReplyTo([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.ReplyTo.First().Address);
        Assert.Equal(name0, email.ReplyTo.First().Name);
        Assert.Equal(address1, email.ReplyTo.Skip(1).First().Address);
        Assert.Equal(name1, email.ReplyTo.Skip(1).First().Name);
        Assert.Equal(address2, email.ReplyTo.Skip(2).First().Address);
        Assert.Equal(name2, email.ReplyTo.Skip(2).First().Name);
    }

    [Theory]
    [InlineData("Sender@Fake.com", "Sender")]
    public void SenderTest(string address, string name)
    {
        var email = new Models.Email();
        email.EmailSender(address, name);
        Assert.NotNull(email);
        Assert.Equal(address, email.Sender.Address);
        Assert.Equal(name, email.Sender.Name);
    }

    [Theory]
    [InlineData("Subject", "TextBody", "HtmlBody")]
    public void ContentTest(string subject, string textBody, string htmlBody)
    {
        var email = new Models.Email();
        email.Subject(subject).TextBody(textBody).HtmlBody(htmlBody);
        Assert.NotNull(email);
        Assert.Equal(subject, email.Content.Subject);
        Assert.Equal(textBody, email.Content.TextBody);
        Assert.Equal(htmlBody, email.Content.HtmlBody);
    }

    [Fact]
    public async Task AttachmentsByPathTest()
    {
        var email = new Models.Email();
        var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
        email
            .Attach(fileName, fileBytes)
            .Attach([new(fileName, fileBytes), new(fileName, fileBytes)]);
        Assert.NotNull(email);
        Assert.Equal(3, email.Attachments.Count);
        Assert.Equal(
            $"{Defaults.MediaTypeApplication}/{Defaults.MediaSubTypeOctetStream}",
            email.Attachments.First().ContentType
        );
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.First().Name);
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.Skip(1).First().Name);
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.Skip(2).First().Name);
    }

    [Fact]
    public void EmailAddressExTest()
    {
        Assert.Throws<ArgumentNullException>(() => new EmailAddress(string.Empty));
    }

    [Fact]
    public async Task AttachTest()
    {
        var (_, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
        var attachment = new EmailAttachment("test.txt", fileBytes);
        Assert.NotNull(attachment.Content);
        Assert.Equal("test.txt", attachment.Name);
    }

    [Fact]
    public async Task AttachmentsByBytesTest()
    {
        var (_, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
        var email = new Models.Email();
        email
            .Attach("test0.txt", fileBytes)
            .Attach(new[] { ("test1.txt", fileBytes), ("test2.txt", fileBytes) });
        Assert.NotNull(email);
        Assert.Equal(3, email.Attachments.Count);
        Assert.Equal("test0.txt", email.Attachments.First().Name);
        Assert.Equal("test1.txt", email.Attachments.Skip(1).First().Name);
        Assert.Equal("test2.txt", email.Attachments.Skip(2).First().Name);
    }

    [Fact]
    public void AttachmentContentTypeTest()
    {
        var attachment = new EmailAttachment("TestAttachment", []);
        Assert.Equal(attachment.ContentType, $"{attachment.MediaType}/{attachment.SubMediaType}");
    }

    [Fact]
    public void MediaTypeTest()
    {
        Assert.Equal("text", Defaults.MediaTypeText);
        Assert.Equal("image", Defaults.MediaTypeImage);
        Assert.Equal("audio", Defaults.MediaTypeAudio);
        Assert.Equal("video", Defaults.MediaTypeVideo);
        Assert.Equal("application", Defaults.MediaTypeApplication);
        Assert.Equal("multipart", Defaults.MediaTypeMultipart);
        Assert.Equal("message", Defaults.MediaTypeMessage);
    }

    [Fact]
    public void MediaSubTypeTest()
    {
        Assert.Equal("html", Defaults.MediaSubTypeHtml);
        Assert.Equal("plain", Defaults.MediaSubTypePlain);
        Assert.Equal("xml", Defaults.MediaSubTypeXml);
        Assert.Equal("json", Defaults.MediaSubTypeJson);
        Assert.Equal("octet-stream", Defaults.MediaSubTypeOctetStream);
        Assert.Equal("x-www-form-urlencoded", Defaults.MediaSubTypeUrlEncoded);
    }

    [Fact]
    public async Task SendAsyncTest()
    {
        var email = new Models.Email();
        email
            .Subject("subject")
            .TextBody("textBody")
            .HtmlBody("htmlBody")
            .EmailFrom("address", "name")
            .EmailTo("address0", "name0")
            .EmailTo([new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")])
            .EmailCc("address0", "name0")
            .EmailCc([new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")])
            .EmailBcc("address0", "name0")
            .EmailBcc(
                [new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")]
            )
            .EmailReplyTo("address0", "name0")
            .EmailReplyTo(
                [new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")]
            )
            .EmailSender("address", "name");
        var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
        email
            .Attach(fileName, fileBytes)
            .Attach([new(fileName, fileBytes), new(fileName, fileBytes)]);
        var emailProvider = new NullEmailProvider();
        await email.SendByAsync(emailProvider);
    }
}
