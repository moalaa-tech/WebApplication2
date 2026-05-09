namespace CRM.Domain.Integrations
{
    public interface ISocialMediaApiClient
    {
        Task<bool> PublishPostAsync(string content);

    }
}
