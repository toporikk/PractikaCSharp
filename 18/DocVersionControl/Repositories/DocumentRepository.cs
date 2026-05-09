using Microsoft.EntityFrameworkCore;
using DocVersionControl.Data;
using DocVersionControl.Models;

namespace DocVersionControl.Repositories
{
    public class DocumentRepository : BaseRepository<Document>
    {
        public DocumentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Document>> GetUserDocumentsAsync(string username)
        {
            return await _dbSet
                .Where(d => d.OwnerUsername == username || d.CreatedBy == username)
                .Include(d => d.Versions)
                .ToListAsync();
        }

        public async Task<Document?> GetDocumentWithVersionsAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Versions)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}