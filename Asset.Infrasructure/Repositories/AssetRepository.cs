using Asset.Domain;

namespace FinancialTask.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetContext _context;

        public AssetRepository(AssetContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AssetEntity asset)
        {
            try
            {
                var entity = (await _context.Assets.AddAsync(asset)).Entity;
                _context.SaveChanges();
            }
            catch (Exception) { }
        }
    }
}
