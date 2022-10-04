namespace DentaClinic.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientCardId { get; set; }
        public DateTime Time { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Service { get; set; }
    }
}
