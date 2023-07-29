using Microsoft.EntityFrameworkCore;
using MinAPI.Models;

namespace MinAPI.Data
{
    public class TableRowRepo : ITableRowRepo
    {
        private AppDbContext _context;

        public TableRowRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateTableRow(TableRow row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            await _context.AddAsync(row);
        }

        public void DeleteTableRow(TableRow row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }
            
                _context.TableRow.Remove(row);
        }

        public async Task<IEnumerable<TableRow>> GetAllTableRows()
        {
            return await _context.TableRow.ToListAsync();
        }

        public async Task<TableRow?> GetTableRowById(int id)
        {
            return await _context.TableRow.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<TableRow?> GetTableRowByLogin(string login)
        {
            return await _context.TableRow.FirstOrDefaultAsync(c => c.Login == login);
        }

        public async Task<TableRow?> GetTableRowByNickname(string nickname)
        {
            return await _context.TableRow.FirstOrDefaultAsync(c => c.Nickname == nickname);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    
        public async Task UpdateAllPostRowsByField(string fieldName, string fieldValue, string newValue)
        {
            var itemsToUpdate = await _context.PostRow.Where(c => EF.Property<string>(c, fieldName) == fieldValue).ToListAsync();

            if (itemsToUpdate.Count > 0)
            {
                // Изменяем найденные записи на новое значение, введенное пользователем
                foreach (var item in itemsToUpdate)
                {
                    // EF.Property<string>(item, fieldName) = newValue;
                    switch (fieldName)
                    {
                        case "Nickname":
                            item.Nickname = newValue;
                            break;
                        default:
                            break;
                    }
                }

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("No records found with this value.");
            }
        }
    }
}