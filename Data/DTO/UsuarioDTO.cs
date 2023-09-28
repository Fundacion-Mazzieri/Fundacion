using Fundacion.Models;

namespace Fundacion.Data.DTO
{
    public class UsuarioDTO
    {
        public long UsDni { get; set; }
        public string UsNombre { get; set; } = null!;
        public string UsContrasena { get; set; } = null!;
        public string rol { get; set; }
        public string UsApellido { get; set; } = null!;
        public bool Autenticado { get; set; } = false;
    }
}
