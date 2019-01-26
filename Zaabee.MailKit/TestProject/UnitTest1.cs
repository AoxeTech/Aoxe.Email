using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Zaabee.MailKit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var stmpClientHelper = new MailKitHelper();
            var sendMessage = new SendMessage();
            var fileStream = FileToStream(@"C:\Users\aeond\Desktop\test_attachment.txt");
            stmpClientHelper.Host("smtp.exmail.qq.com")
                .Port(587)
                .UserName("your user name")
                .Password("your password")
                .Ssl(true)
                .Send(sendMessage.From("from email")
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
    }
}