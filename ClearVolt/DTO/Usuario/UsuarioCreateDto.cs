using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClearVolt.DTO.Usuario
{
    public class UsuarioCreateDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string senha { get; set; }

        [ForeignKey("Pessoa")]
        public int id_pessoa { get; set; }

        [ForeignKey("Role")]
        public int id_role { get; set; }
    }
}
