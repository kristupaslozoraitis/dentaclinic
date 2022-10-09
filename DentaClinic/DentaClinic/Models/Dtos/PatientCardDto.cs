namespace DentaClinic.Models.Dtos
{
    public class PatientCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public long PersonalNumber { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
