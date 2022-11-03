namespace DentaClinic.Models.Dtos
{
    public class SuccessfulLoginDto
    {
        public SuccessfulLoginDto(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}
