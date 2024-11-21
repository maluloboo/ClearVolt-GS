using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.ConfiguracaoColeta
{
    public class ConfiguracaoColetaCreateDto
    {
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
