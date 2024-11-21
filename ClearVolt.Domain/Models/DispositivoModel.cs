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
    [Table("Dispositivo")]
    public class DispositivoModel
    {
        [Key]
        public int id_dispositivo {  get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string marca { get; set; }
        [Required]
        public string identificador { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public UsuarioModel Usuario { get; set; }

        [JsonIgnore]
        [InverseProperty("Dispositivo")]
        public ICollection<ConfiguracaoColetaModel> ConfigColeta { get; set; }

        [JsonIgnore]
        [InverseProperty("Dispositivo")]
        public ICollection<DadoColetadoModel> DadoColetado { get; set; }
    }
}
