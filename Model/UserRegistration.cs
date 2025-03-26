using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Model
{
    public class UserRegistration
    {
      

        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; } //Nullable DateTime
        public string? UserPhoto { get; set; } = "";
        public string UserPhoneNumber { get; set; }
        public string UserEmailID { get; set; }
    }
}
