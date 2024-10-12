namespace Aoxe.Aws.SimpleEmail.Provider;

public interface ISesClientFactory
{
    IAmazonSimpleEmailServiceV2 Create();
}
