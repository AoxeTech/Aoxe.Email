namespace Aoxe.Email.Abstractions.Models;

public static class Defaults
{
    #region MediaType

    public const string MediaTypeText = "text";
    public const string MediaTypeImage = "image";
    public const string MediaTypeAudio = "audio";
    public const string MediaTypeVideo = "video";
    public const string MediaTypeApplication = "application";
    public const string MediaTypeMultipart = "multipart";
    public const string MediaTypeMessage = "message";

    #endregion

    #region MediaSubType

    public const string MediaSubTypeHtml = "html";
    public const string MediaSubTypePlain = "plain";
    public const string MediaSubTypeXml = "xml";
    public const string MediaSubTypeJson = "json";
    public const string MediaSubTypeOctetStream = "octet-stream";
    public const string MediaSubTypeUrlEncoded = "x-www-form-urlencoded";

    #endregion

    public const string ContentType = $"{MediaTypeApplication}/{MediaSubTypeOctetStream}";
}
