namespace WebMonitoringApi.Data.Models
{
    public class SignInResult
    {
        public bool Succeeded { get; set; }
        public string Jwt { get; set; }
    }
}
