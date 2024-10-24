namespace Asset.Domain
{
    public interface IAssetRepository
    {
        Task AddAsync(AssetEntity asset);
    }
}