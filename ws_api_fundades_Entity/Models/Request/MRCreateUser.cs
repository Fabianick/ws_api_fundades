using System.ComponentModel.DataAnnotations;

namespace ws_api_fundades_Entity.Models.Request
{
    public class MRCreateUser
    {
        [Required]
        public required string usuario { get; set; }
        [Required]
        public required string password { get; set; }
        [Required]
        public required string nombres { get; set; }
        [Required]
        public required string apellidos { get; set; }
        [Required]
        public required string email { get; set; }
        [Required]
        public required string telefono { get; set; }
        [Required]
        public required long rol { get; set; }
    }
}
