namespace DentaClinic.Models.Dtos
{
    public class VisitPostDto
    {
        public DateTime Time { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Service { get; set; }
    }
}
