using DentaClinic.Utils;

namespace DentaClinic.Models
{
    public class Visit : IUserOwnedResource
    {
        public int Id { get; set; }
        public PatientCard PatientCard { get; set; }
        public DateTime Time { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Service { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
