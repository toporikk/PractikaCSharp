using DocVersionControl.Data;
using DocVersionControl.Models;
using Microsoft.EntityFrameworkCore;

namespace DocVersionControl.Repositories
{
    public class VersionRepository : BaseRepository<DocumentVersion>
    {
        public VersionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<DocumentVersion>> GetDocumentVersionsAsync(int documentId)
        {
            return await _dbSet
                .Where(v => v.DocumentId == documentId)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();
        }
    }
}