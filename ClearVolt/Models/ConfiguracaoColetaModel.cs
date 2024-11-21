using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClearVolt.Domain.Models
{
    [Table("Configuracao_Coleta")]
    public class ConfiguracaoColetaModel
    {
        [Key]
        public int id_configuracao {  get; set; }
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

        [JsonIgnore]
        [InverseProperty("Configuracao")]
        public ICollection<DispositivoModel> Dispositivo { get; set; }
    }
}
