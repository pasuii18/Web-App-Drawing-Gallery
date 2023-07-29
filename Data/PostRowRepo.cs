using Microsoft.EntityFrameworkCore;
using MinAPI.Models;

namespace MinAPI.Data
{
    public class PostRowRepo : IPostRowRepo
    {
        private AppDbContext _context;

        public PostRowRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreatePostRow(PostRow row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            await _context.AddAsync(row);
        }

        public void DeletePostRow(PostRow row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            
                _context.PostRow.Remove(row);
        }

        public async Task<IEnumerable<PostRow>> GetAllPostRows()
        {
            return await _context.PostRow.ToListAsync();
        }

        public async Task<IEnumerable<PostRow>> GetAllPostRowsByField(string nickname, string fieldValue)
        {
            // Находим все записи, удовлетворяющие запросу пользователя
            return await _context.PostRow.Where(c => EF.Property<string>(c, nickname) == fieldValue).ToListAsync();
        }

        public async Task<PostRow?> GetPostRowById(int id)
        {
            return await _context.PostRow.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}