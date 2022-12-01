using DentaClinic.Utils;

namespace DentaClinic.Models
{
    public class Service : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
