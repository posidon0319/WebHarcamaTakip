using System.Text.Json;
using WebHarcamaTakip.Models;

namespace WebHarcamaTakip.Services
{
    public class ExpenseService
    {
        private static string path = "App_Data/expenses.json";
        private static List<Expense>? _cache = null;

        public static List<Expense> GetAll()
        {
            if (_cache != null)
                return _cache;

            if (!File.Exists(path))
                return new List<Expense>();

            var json = File.ReadAllText(path);
            _cache = JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
            return _cache;
        }

        public static void Add(Expense e)
        {
            var all = GetAll();
            e.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
            all.Add(e);
            Save(all);
        }

        public static void Save(List<Expense> list)
        {
            Directory.CreateDirectory("App_Data");
            File.WriteAllText(path, JsonSerializer.Serialize(list));
            _cache = list;
        }

        public static Expense? GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public static void Delete(int id)
        {
            var all = GetAll();
            var item = all.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                all.Remove(item);
                Save(all);
            }
        }
    }
}
