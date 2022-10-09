namespace DentaClinic.Models.Dtos
{
    public class VisitUpdateDto
    {
        public DateTime Time { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Service { get; set; }
    }
}
