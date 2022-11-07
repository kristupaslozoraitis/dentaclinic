using DentaClinic.Utils;

namespace DentaClinic.Models
{
    public class VisitFeedback : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public Visit Visit { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
