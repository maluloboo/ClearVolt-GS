using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClearVolt.DTO.Role
{
    public class RoleCreateDto
    {
        [Required]
        public string nome { get; set; }
    }
}
