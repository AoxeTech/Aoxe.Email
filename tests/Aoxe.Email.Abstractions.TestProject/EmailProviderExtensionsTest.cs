namespace Aoxe.Email.Abstractions.TestProject;

public class EmailProviderExtensionsTest
{
    [Theory]
    [InlineData("From@Fake.com", "From")]
    public void FromTest(string address, string name)
    {
        using var emailProvider = new NullEmailProvider();
        emailProvider.From(address, name);
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address, emailProvider.Email.From.Address);
        Assert.Equal(name, emailProvider.Email.From.Name);
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
        using var emailProvider = new NullEmailProvider();
        emailProvider.To(address0, name0);
        emailProvider.To(
            new[] { new EmailAddress(address1, name1), new EmailAddress(address2, name2) }
        );
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address0, emailProvider.Email.Recipients.To.First().Address);
        Assert.Equal(name0, emailProvider.Email.Recipients.To.First().Name);
        Assert.Equal(address1, emailProvider.Email.Recipients.To.Skip(1).First().Address);
        Assert.Equal(name1, emailProvider.Email.Recipients.To.Skip(1).First().Name);
        Assert.Equal(address2, emailProvider.Email.Recipients.To.Skip(2).First().Address);
        Assert.Equal(name2, emailProvider.Email.Recipients.To.Skip(2).First().Name);
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
        using var emailProvider = new NullEmailProvider();
        emailProvider.Cc(address0, name0);
        emailProvider.Cc(
            new[] { new EmailAddress(address1, name1), new EmailAddress(address2, name2) }
        );
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address0, emailProvider.Email.Recipients.Cc.First().Address);
        Assert.Equal(name0, emailProvider.Email.Recipients.Cc.First().Name);
        Assert.Equal(address1, emailProvider.Email.Recipients.Cc.Skip(1).First().Address);
        Assert.Equal(name1, emailProvider.Email.Recipients.Cc.Skip(1).First().Name);
        Assert.Equal(address2, emailProvider.Email.Recipients.Cc.Skip(2).First().Address);
        Assert.Equal(name2, emailProvider.Email.Recipients.Cc.Skip(2).First().Name);
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
        using var emailProvider = new NullEmailProvider();
        emailProvider.Bcc(address0, name0);
        emailProvider.Bcc(
            new[] { new EmailAddress(address1, name1), new EmailAddress(address2, name2) }
        );
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address0, emailProvider.Email.Recipients.Bcc.First().Address);
        Assert.Equal(name0, emailProvider.Email.Recipients.Bcc.First().Name);
        Assert.Equal(address1, emailProvider.Email.Recipients.Bcc.Skip(1).First().Address);
        Assert.Equal(name1, emailProvider.Email.Recipients.Bcc.Skip(1).First().Name);
        Assert.Equal(address2, emailProvider.Email.Recipients.Bcc.Skip(2).First().Address);
        Assert.Equal(name2, emailProvider.Email.Recipients.Bcc.Skip(2).First().Name);
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
        using var emailProvider = new NullEmailProvider();
        emailProvider.ReplyTo(address0, name0);
        emailProvider.ReplyTo(
            new[] { new EmailAddress(address1, name1), new EmailAddress(address2, name2) }
        );
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address0, emailProvider.Email.ReplyTo.First().Address);
        Assert.Equal(name0, emailProvider.Email.ReplyTo.First().Name);
        Assert.Equal(address1, emailProvider.Email.ReplyTo.Skip(1).First().Address);
        Assert.Equal(name1, emailProvider.Email.ReplyTo.Skip(1).First().Name);
        Assert.Equal(address2, emailProvider.Email.ReplyTo.Skip(2).First().Address);
        Assert.Equal(name2, emailProvider.Email.ReplyTo.Skip(2).First().Name);
    }

    [Theory]
    [InlineData("Sender@Fake.com", "Sender")]
    public void SenderTest(string address, string name)
    {
        using var emailProvider = new NullEmailProvider();
        emailProvider.Sender(address, name);
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(address, emailProvider.Email.Sender.Address);
        Assert.Equal(name, emailProvider.Email.Sender.Name);
    }

    [Theory]
    [InlineData("Subject", "TextBody", "HtmlBody")]
    public void ContentTest(string subject, string textBody, string htmlBody)
    {
        using var emailProvider = new NullEmailProvider();
        emailProvider.Subject(subject);
        emailProvider.TextBody(textBody);
        emailProvider.HtmlBody(htmlBody);
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(subject, emailProvider.Email.Content.Subject);
        Assert.Equal(textBody, emailProvider.Email.Content.TextBody);
        Assert.Equal(htmlBody, emailProvider.Email.Content.HtmlBody);
    }

    [Fact]
    public void AttachmentsByPathTest()
    {
        using var emailProvider = new NullEmailProvider();
        emailProvider.Attach(".\\AttachmentTestFile.txt");
        emailProvider.Attach(new[] { ".\\AttachmentTestFile.txt", ".\\AttachmentTestFile.txt" });
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(3, emailProvider.Email.Attachments.Count);
        Assert.Equal("AttachmentTestFile.txt", emailProvider.Email.Attachments.First().Name);
        Assert.Equal(
            "AttachmentTestFile.txt",
            emailProvider.Email.Attachments.Skip(1).First().Name
        );
        Assert.Equal(
            "AttachmentTestFile.txt",
            emailProvider.Email.Attachments.Skip(2).First().Name
        );
    }

    [Fact]
    public void AttachmentsByPathExTest()
    {
        using var emailProvider = new NullEmailProvider();
        Assert.Throws<FileNotFoundException>(() => emailProvider.Attach(".\\notExist.txt"));
    }

    [Fact]
    public void EmailIdExTest()
    {
        Assert.Throws<ArgumentNullException>(
            () => new Email.Abstractions.Models.Email(string.Empty)
        );
    }

    [Fact]
    public void EmailAddressExTest()
    {
        Assert.Throws<ArgumentNullException>(() => new EmailAddress(string.Empty));
    }

    [Fact]
    public async Task AttachTest()
    {
        var fileBytes = await FileToBytesAsync(".\\AttachmentTestFile.txt");
        var attachment = new EmailAttachment { Content = fileBytes, Name = "test.txt" };
        Assert.NotNull(attachment.Content);
        Assert.Equal("test.txt", attachment.Name);
    }

    [Fact]
    public void CleanEmailTest()
    {
        using var emailProvider = new NullEmailProvider();
        emailProvider.Email = new Models.Email();
        Assert.NotNull(emailProvider.Email);
        emailProvider.CleanEmail();
        Assert.Null(emailProvider.Email);
    }

    [Fact]
    public async Task SendEmailTest()
    {
        using var emailProvider = new NullEmailProvider();
        await Assert.ThrowsAsync<NullReferenceException>(
            async () => await emailProvider.SendAsync()
        );
        emailProvider.Email = new Models.Email();
        Assert.NotNull(emailProvider.Email);
        await emailProvider.SendAsync();
        Assert.Null(emailProvider.Email);
    }

    [Fact]
    public async Task AttachmentsByBytesTest()
    {
        var fileBytes = await FileToBytesAsync(".\\AttachmentTestFile.txt");
        using var emailProvider = new NullEmailProvider();
        emailProvider.Attach("test0.txt", fileBytes);
        emailProvider.Attach(new[] { ("test1.txt", fileBytes), ("test2.txt", fileBytes) });
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(3, emailProvider.Email.Attachments.Count);
        Assert.Equal("test0.txt", emailProvider.Email.Attachments.First().Name);
        Assert.Equal("test1.txt", emailProvider.Email.Attachments.Skip(1).First().Name);
        Assert.Equal("test2.txt", emailProvider.Email.Attachments.Skip(2).First().Name);
    }

    [Fact]
    public async Task EmailCommandTest()
    {
        var emailCommandTest = new Models.Email("test");
        Assert.Equal("test", emailCommandTest.Id);

        var emailCommand = await CreateEmailAsync();
        using var emailProvider = new NullEmailProvider();
        Assert.Null(emailProvider.Email);
        emailProvider.Email(emailCommand);
        Assert.NotNull(emailProvider.Email);
        Assert.Equal(emailProvider.Email.Id, emailCommand.Id);
    }

    private Task<byte[]> FileToBytesAsync(string path)
    {
        byte[] bytes;
        using (
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
        )
        {
            bytes = new byte[fileStream.Length];
            _ = fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        return Task.FromResult(bytes);
    }

    private async Task<Email.Abstractions.Models.Email> CreateEmailAsync() =>
        new()
        {
            From = new EmailAddress { Address = "From@Fake.com", Name = "From" },
            Sender = new EmailAddress { Address = "Sender@Fake.com", Name = "Sender" },
            Content = new EmailContent
            {
                Subject = $"Test {DateTime.UtcNow}",
                TextBody = "This is a test email",
                HtmlBody = "<h1>This is a test email</h1>"
            },
            Recipients = new EmailRecipients
            {
                To =  [new EmailAddress("To@Fake.com", "To")],
                Cc =  [new EmailAddress("Cc@Fake.com", "Cc")],
                Bcc =  [new EmailAddress("Bcc@Fake.com", "Bcc")]
            },
            ReplyTo =  [new EmailAddress("ReplyTo@Fake.com", "ReplyTo")],
            Attachments =
            [
                new EmailAttachment
                {
                    Name = "test1.txt",
                    Content = await FileToBytesAsync(".\\AttachmentTestFile.txt")
                },
                new EmailAttachment
                {
                    Name = "test2.txt",
                    Content = await FileToBytesAsync(".\\AttachmentTestFile.txt")
                }
            ]
        };
}
