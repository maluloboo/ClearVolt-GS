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
    [Table("Usuario")]
    public class UsuarioModel
    {
        [Key]
        public int id_usuario {  get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string senha { get; set; }

        [JsonIgnore]
        [InverseProperty("Usuario")]
        public ICollection<PessoaModel> Pessoas { get; set; }

        [JsonIgnore]
        [InverseProperty("Usuario")]
        public ICollection<RoleModel> Roles { get; set; }

        [JsonIgnore]
        [InverseProperty("Usuario")]
        public ICollection<DispositivoModel> Dispositivos { get; set; }
    }
}
