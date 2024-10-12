namespace Aoxe.Aws.SimpleEmail;

public interface ISesClientFactory
{
    IAmazonSimpleEmailServiceV2 Create();
}
