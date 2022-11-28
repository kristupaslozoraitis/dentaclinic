namespace DentaClinic.Models
{
    public class FreeVisit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Service Service { get; set; }
    }
}
