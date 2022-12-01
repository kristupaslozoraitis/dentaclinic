namespace DentaClinic.Models.Dtos
{
    public class FreeVisitDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public string DoctorFullName { get; set; }
        public int ServiceId { get; set; }
        public string Service { get; set; }
    }
}
