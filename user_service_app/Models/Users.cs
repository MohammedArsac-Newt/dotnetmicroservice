using System.ComponentModel.DataAnnotations;

namespace user_service_app.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
    }
}
