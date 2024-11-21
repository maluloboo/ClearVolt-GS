using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.DadoColetado
{
    public class DadoColetadoEditDto
    {
        [Key]
        public int id_dado { get; set; }
        public int temperatura { get; set; }
        public int umidade { get; set; }
        [Required]
        public DateTime data_dado { get; set; }
        [Required]
        public string identificador { get; set; }

        [ForeignKey("Dispositivo")]
        public int id_dispositivo { get; set; }
    }
}
