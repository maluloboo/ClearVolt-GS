using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Domain.Models
{
    [Table("Pessoa")]
    public class PessoaModel
    {
        [Key]
        public int id_pessoa { get; set; }
        [Required]
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public DateTime data_nascimento { get; set; }

        [Required]
        public string cpf { get; set; }
        public string telefone { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
