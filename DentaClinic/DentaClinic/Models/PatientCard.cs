using DentaClinic.Utils;
using System.Text.Json.Serialization;

namespace DentaClinic.Models
{
    public class PatientCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public int PersonalNumber { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public PatientCard(int id, string name, string surname, DateOnly birthDate, int personalNumber, string homeAddress, string phoneNumber, int height, int weight)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            PersonalNumber = personalNumber;
            HomeAddress = homeAddress;
            PhoneNumber = phoneNumber;
            Height = height;
            Weight = weight;
        }
    }
}
