using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Model
{
    public class Admin_Login
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
