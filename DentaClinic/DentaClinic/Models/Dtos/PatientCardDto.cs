using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class PatientCardDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("surname")]
        public string Surname { get; set; }
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("personalNumber")]
        public long PersonalNumber { get; set; }
        [JsonPropertyName("homeAddress")]
        public string HomeAddress { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }
}
