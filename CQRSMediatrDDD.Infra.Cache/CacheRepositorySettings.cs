namespace CQRSMediatrDDD.Infra.Cache;

public class CacheRepositorySettings
{
    public string? ConnectionString { get; set; }
    public int TimeToLiveInseconds { get; set; }
}