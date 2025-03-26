using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Model
{
    public class Pets_detail
    {
        [Key]
        public int PetsID { get; set; }
        public string PetsName { get; set; }
        public string PetsType { get; set; }
        public string PetsBreed { get; set; }
        public decimal weight { get; set; }
        public int PetsAge { get; set; }
        public string PetsGender { get; set; }
        public bool IsVaccinated { get; set; }
        public bool neutered { get; set; }
        public bool Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; } //Nullable DateTime]]
        public string PetsPhoto { get; set; }
    }
}
