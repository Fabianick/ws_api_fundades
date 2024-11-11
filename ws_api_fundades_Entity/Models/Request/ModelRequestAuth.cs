using System.ComponentModel.DataAnnotations;

namespace ws_api_fundades_Entity.Models.Request
{
    public class ModelRequestAuth
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
