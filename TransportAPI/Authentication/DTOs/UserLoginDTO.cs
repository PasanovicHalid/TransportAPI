using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TransportAPI.Authentication.DTOs
{
    public class UserLoginDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
