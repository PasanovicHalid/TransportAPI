namespace TransportAPI.Authentication.DTOs
{
    public class DriverRegisterResultDTO
    {
        public bool Succeeded { get; set; }

        public TokenDTO Token { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
