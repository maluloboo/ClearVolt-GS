using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.Pessoa
{
    public class PessoaEditDto
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
    }
}
