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
        email.Recipients = new();
        Assert.NotNull(email.Recipients);
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
        email.EmailTo(address0, name0);
        email.EmailTo([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.To.First().Address);
        Assert.Equal(name0, email.Recipients.To.First().Name);
        Assert.Equal(address1, email.Recipients.To.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.To.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.To.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.To.Skip(2).First().Name);
        email.Recipients.To = new();
        Assert.Empty(email.Recipients.To);
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
        email.EmailCc(address0, name0);
        email.EmailCc([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.Cc.First().Address);
        Assert.Equal(name0, email.Recipients.Cc.First().Name);
        Assert.Equal(address1, email.Recipients.Cc.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.Cc.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.Cc.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.Cc.Skip(2).First().Name);
        email.Recipients.Cc = new();
        Assert.Empty(email.Recipients.Cc);
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
        email.EmailBcc(address0, name0);
        email.EmailBcc([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.Recipients.Bcc.First().Address);
        Assert.Equal(name0, email.Recipients.Bcc.First().Name);
        Assert.Equal(address1, email.Recipients.Bcc.Skip(1).First().Address);
        Assert.Equal(name1, email.Recipients.Bcc.Skip(1).First().Name);
        Assert.Equal(address2, email.Recipients.Bcc.Skip(2).First().Address);
        Assert.Equal(name2, email.Recipients.Bcc.Skip(2).First().Name);
        email.Recipients.Bcc = new();
        Assert.Empty(email.Recipients.Bcc);
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
        email.EmailReplyTo(address0, name0);
        email.EmailReplyTo([new EmailAddress(address1, name1), new EmailAddress(address2, name2)]);
        Assert.NotNull(email);
        Assert.Equal(address0, email.ReplyTo.First().Address);
        Assert.Equal(name0, email.ReplyTo.First().Name);
        Assert.Equal(address1, email.ReplyTo.Skip(1).First().Address);
        Assert.Equal(name1, email.ReplyTo.Skip(1).First().Name);
        Assert.Equal(address2, email.ReplyTo.Skip(2).First().Address);
        Assert.Equal(name2, email.ReplyTo.Skip(2).First().Name);
        email.ReplyTo = new();
        Assert.Empty(email.ReplyTo);
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
        email.Subject(subject);
        email.TextBody(textBody);
        email.HtmlBody(htmlBody);
        Assert.NotNull(email);
        Assert.Equal(subject, email.Content.Subject);
        Assert.Equal(textBody, email.Content.TextBody);
        email.Content = new();
        Assert.NotNull(email.Content);
        Assert.Equal(string.Empty, email.Content.Subject);
        Assert.Equal(string.Empty, email.Content.TextBody);
        Assert.Equal(string.Empty, email.Content.HtmlBody);
    }

    [Fact]
    public async Task AttachmentsByPathTest()
    {
        var email = new Models.Email();
        var (fileName, fileBytes) = await new FileHelper().LoadFileBytesAsync(
            "AttachmentTestFile.txt"
        );
        email.Attach(fileName, fileBytes);
        email.Attach([new( fileName, fileBytes), new( fileName, fileBytes)]);
        Assert.NotNull(email);
        Assert.Equal(3, email.Attachments.Count);
        Assert.Equal(
            $"{MediaType.Application}/{MediaSubType.OctetStream}",
            email.Attachments.First().ContentType
        );
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.First().Name);
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.Skip(1).First().Name);
        Assert.Equal("AttachmentTestFile.txt", email.Attachments.Skip(2).First().Name);
        email.Attachments = new();
        Assert.Empty(email.Attachments);
    }

    [Fact]
    public void EmailAddressExTest()
    {
        Assert.Throws<ArgumentNullException>(() => new EmailAddress(string.Empty));
    }

    [Fact]
    public async Task AttachTest()
    {
        var (fileName, fileBytes) = await new FileHelper().LoadFileBytesAsync(
            "AttachmentTestFile.txt"
        );
        var attachment = new EmailAttachment { Content = fileBytes, Name = "test.txt" };
        Assert.NotNull(attachment.Content);
        Assert.Equal("test.txt", attachment.Name);
    }

    [Fact]
    public async Task AttachmentsByBytesTest()
    {
        var (fileName, fileBytes) = await new FileHelper().LoadFileBytesAsync(
            "AttachmentTestFile.txt"
        );
        var email = new Models.Email();
        email.Attach("test0.txt", fileBytes);
        email.Attach(new[] { ("test1.txt", fileBytes), ("test2.txt", fileBytes) });
        Assert.NotNull(email);
        Assert.Equal(3, email.Attachments.Count);
        Assert.Equal("test0.txt", email.Attachments.First().Name);
        Assert.Equal("test1.txt", email.Attachments.Skip(1).First().Name);
        Assert.Equal("test2.txt", email.Attachments.Skip(2).First().Name);
    }

    [Fact]
    public void MediaTypeTest()
    {
        Assert.Equal("text", MediaType.Text);
        Assert.Equal("image", MediaType.Image);
        Assert.Equal("audio", MediaType.Audio);
        Assert.Equal("video", MediaType.Video);
        Assert.Equal("application", MediaType.Application);
        Assert.Equal("multipart", MediaType.Multipart);
        Assert.Equal("message", MediaType.Message);
    }

    [Fact]
    public void MediaSubTypeTest()
    {
        Assert.Equal("html", MediaSubType.Html);
        Assert.Equal("plain", MediaSubType.Plain);
        Assert.Equal("xml", MediaSubType.Xml);
        Assert.Equal("json", MediaSubType.Json);
        Assert.Equal("octet-stream", MediaSubType.OctetStream);
        Assert.Equal("x-www-form-urlencoded", MediaSubType.UrlEncoded);
    }
}
