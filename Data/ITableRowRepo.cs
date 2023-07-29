using MinAPI.Models;

namespace MinAPI.Data
{
    public interface ITableRowRepo
    {
        Task SaveChanges();
        Task<TableRow?> GetTableRowById(int id);
        Task<TableRow?> GetTableRowByLogin(string login);
        Task<TableRow?> GetTableRowByNickname(string nickname);
        Task<IEnumerable<TableRow>> GetAllTableRows();
        Task CreateTableRow(TableRow row);
        void DeleteTableRow(TableRow row);
        Task UpdateAllPostRowsByField(string fieldName, string fieldValue, string newValue);
    }
}