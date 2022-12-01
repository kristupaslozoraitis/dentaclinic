using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class VisitPostDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("doctorName")]
        public string DoctorName { get; set; }
        [JsonPropertyName("doctorSurname")]
        public string DoctorSurname { get; set; }
        [JsonPropertyName("service")]
        public string Service { get; set; }
        [JsonPropertyName("freeVisitId")]
        public int FreeVisitId { get; set; }
    }
}
