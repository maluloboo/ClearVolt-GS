using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.ConfiguracaoColeta
{
    public class ConfiguracaoColetaEditDto
    {
        [Key]
        public int id_configuracao { get; set; }
        [Required]
        public string nome { get; set; }
        public string descricao { get; set; }
        [Required]
        public int temp_max { get; set; }
        [Required]
        public int umidade_min { get; set; }
        [Required]
        public int tempo_de_umidade_min { get; set; }
        public int intervalo_de_horas { get; set; }
    }
}
