namespace Aoxe.Email.Abstractions.TestProject;

public class FileHelperTest
{
    [Fact]
    public void GetFileTest()
    {
        var (fileName, fileBytes) = FileHelper.LoadFileBytes("AttachmentTestFile.txt");
        Assert.Equal("AttachmentTestFile.txt", fileName);
        Assert.NotNull(fileBytes);
    }

    [Fact]
    public void AttachmentsByPathExTest()
    {
        Assert.Throws<FileNotFoundException>(() => FileHelper.LoadFileBytes(".\\notExist.txt"));
    }
}
