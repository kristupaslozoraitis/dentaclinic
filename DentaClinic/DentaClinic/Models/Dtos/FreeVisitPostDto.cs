namespace DentaClinic.Models.Dtos
{
    public class FreeVisitPostDto
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
