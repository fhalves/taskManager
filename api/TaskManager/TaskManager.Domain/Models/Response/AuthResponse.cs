namespace TaskManager.Domain.Models.Response
{
    public class AuthResponse
    {
        public bool Success { get; set; }

        public string Token { get; set; }

        public double ExpiresInSeconds { get; set; }
    }
}
