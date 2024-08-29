namespace Aoxe.Email.Abstractions.TestProject;

public class FileHelperTest
{
    [Fact]
    public async Task GetFileTest()
    {
        var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
        Assert.Equal("AttachmentTestFile.txt", fileName);
        Assert.NotNull(fileBytes);
    }

    [Fact]
    public async Task AttachmentsByPathExTest()
    {
        await Assert.ThrowsAsync<FileNotFoundException>(
            async () => await FileHelper.LoadFileBytesAsync(".\\notExist.txt")
        );
    }
}
