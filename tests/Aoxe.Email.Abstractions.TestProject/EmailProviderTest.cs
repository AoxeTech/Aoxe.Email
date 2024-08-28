namespace Aoxe.Email.Abstractions.TestProject;

public class EmailProviderTest
{
    [Fact]
    public async Task SendAsyncTest()
    {
        var emailProvider = new NullEmailProvider();
        await emailProvider.SendAsync(new Models.Email());
    }

    [Fact]
    public void DisposeTest()
    {
        var emailProvider = new NullEmailProvider();
        emailProvider.Dispose();
    }
}
