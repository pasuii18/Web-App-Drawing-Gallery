using MinAPI.Models;

namespace MinAPI.Data
{
    public interface IPostRowRepo
    {
        Task SaveChanges();
        Task<PostRow?> GetPostRowById(int id);
        Task<IEnumerable<PostRow>> GetAllPostRows();
        Task CreatePostRow(PostRow row);
        void DeletePostRow(PostRow row);
        Task<IEnumerable<PostRow>> GetAllPostRowsByField(string nickname, string fieldValue);
    }
}