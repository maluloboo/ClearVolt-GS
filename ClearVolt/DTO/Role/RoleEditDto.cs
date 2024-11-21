using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.Role
{
    public class RoleEditDto
    {
        [Key]
        public int id_role { get; set; }
        [Required]
        public string nome { get; set; }
    }
}
