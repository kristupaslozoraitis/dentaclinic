namespace DentaClinic.Models
{
    public class VisitFeedback
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public Visit Visit { get; set; }
    }
}
