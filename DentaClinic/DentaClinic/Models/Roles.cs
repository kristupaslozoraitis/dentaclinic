namespace DentaClinic.Models
{
    public static class Roles
    {
        public const string RegisteredUser = nameof(RegisteredUser);
        public const string Odontologist = nameof(Odontologist);

        public static readonly IReadOnlyCollection<string> All = new[] { RegisteredUser, Odontologist };
    }
}
