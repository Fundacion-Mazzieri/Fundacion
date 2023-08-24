using Fundacion.Models;

namespace Fundacion.Data.DTO
{
    public class UsuarioDTO
    {
        public long UsDni { get; set; }
        public string UsContrasena { get; set; } = null!;
        public string rol { get; set; }
        public bool Autenticado { get; set; } = false;
    }
}
