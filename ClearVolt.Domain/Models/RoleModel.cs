using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Domain.Models
{
    [Table("Role")]
    public class RoleModel
    {
        [Key]
        public int id_role {  get; set; }
        [Required]
        public string nome { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
