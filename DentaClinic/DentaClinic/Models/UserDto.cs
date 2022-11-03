namespace DentaClinic.Models
{
    public class UserDto
    {
        public UserDto(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
    }
}
