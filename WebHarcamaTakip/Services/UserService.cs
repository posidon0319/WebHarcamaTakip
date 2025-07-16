using System.Text.Json;
using WebHarcamaTakip.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebHarcamaTakip.Services
{
    public static class UserService
    {
        private static string path = "App_Data/users.json";
        private static List<User>? _cache = null;

        public static List<User> GetAll()
        {
            if (_cache != null)
                return _cache;

            if (!File.Exists(path))
                return new List<User>();

            var json = File.ReadAllText(path);
            _cache = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            return _cache;
        }

        public static void Add(User user)
        {
            var all = GetAll();
            user.Id = all.Count > 0 ? all.Max(x => x.Id) + 1 : 1;
            all.Add(user);
            Save(all);
        }

        public static User? GetByUsername(string username)
        {
            return GetAll().FirstOrDefault(u => u.Username == username);
        }

        public static void Save(List<User> list)
        {
            Directory.CreateDirectory("App_Data");
            File.WriteAllText(path, JsonSerializer.Serialize(list));
            _cache = list;
        }
    }
}
