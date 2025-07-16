namespace WebHarcamaTakip.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Basit örnek için şifre hash yok, gerçek projede hash gerekir!
    }
}
