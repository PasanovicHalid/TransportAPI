namespace Application.Authentication.Contracts
{
    public class AuthenticationResult
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
    }
}
