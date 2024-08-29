namespace Aoxe.Email.Abstractions.Models;

public static class Defaults
{
    /// <summary>
    /// https://www.iana.org/assignments/media-types/media-types.xhtml
    /// </summary>
    #region MediaType

    public const string MediaTypeApplication = "application";
    public const string MediaTypeAudio = "audio";
    public const string MediaTypeExample = "example";
    public const string MediaTypeFont = "font";
    public const string MediaTypeHaptics = "haptics";
    public const string MediaTypeImage = "image";
    public const string MediaTypeMessage = "message";
    public const string MediaTypeModel = "model";
    public const string MediaTypeMultipart = "multipart";
    public const string MediaTypeText = "text";
    public const string MediaTypeVideo = "video";

    #endregion

    #region MediaSubType

    public const string MediaSubTypeHtml = "html";
    public const string MediaSubTypePlain = "plain";
    public const string MediaSubTypeXml = "xml";
    public const string MediaSubTypeJson = "json";
    public const string MediaSubTypeOctetStream = "octet-stream";
    public const string MediaSubTypeUrlEncoded = "x-www-form-urlencoded";

    #endregion
}
