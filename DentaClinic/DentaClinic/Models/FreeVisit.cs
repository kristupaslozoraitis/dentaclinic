using DentaClinic.Utils;
using System.ComponentModel.DataAnnotations;

namespace DentaClinic.Models
{
    public class FreeVisit : IUserOwnedResource
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string DoctorFullName { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
