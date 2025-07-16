using System.Text.Json;
using WebHarcamaTakip.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebHarcamaTakip.Services
{
    public static class ExpenseService
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

        public static void Add(Expense expense)
        {
            var all = GetAll();
            expense.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
            all.Add(expense);
            Save(all);
        }

        public static void Delete(int id)
        {
            var all = GetAll();
            var item = all.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                all.Remove(item);
                Save(all);
            }
        }

        public static void Save(List<Expense> list)
        {
            Directory.CreateDirectory("App_Data");
            File.WriteAllText(path, JsonSerializer.Serialize(list));
            _cache = list;
        }
    }
}
