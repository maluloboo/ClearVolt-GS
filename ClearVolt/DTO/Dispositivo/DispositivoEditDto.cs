using ClearVolt.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.Dispositivo
{
    public class DispositivoEditDto
    {
        [Key]
        public int id_dispositivo { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string marca { get; set; }
        [Required]
        public string identificador { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }

        [ForeignKey("Configuracao_Coleta")]
        public int id_configuracao { get; set; }
    }
}
