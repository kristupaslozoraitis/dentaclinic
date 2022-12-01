using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("userInformation")]
        public PatientCardPostDto PatientCardData { get; set; }
    }
}
