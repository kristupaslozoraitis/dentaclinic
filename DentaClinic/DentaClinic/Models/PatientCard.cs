using DentaClinic.Utils;
using System.Text.Json.Serialization;

namespace DentaClinic.Models
{
    public class PatientCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public long PersonalNumber { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
