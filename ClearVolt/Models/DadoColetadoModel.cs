using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Domain.Models
{
    [Table("Dado_Coletado")]
    public class DadoColetadoModel
    {
        [Key]
        public int id_dado {  get; set; }
        public int temperatura { get; set; }
        public int umidade { get; set; }
        [Required]
        public DateTime data_dado { get; set; }
        [Required]
        public string identificador { get; set; }

        [ForeignKey("Dispositivo")]
        public int id_dispositivo { get; set; }
        public DispositivoModel Dispositivo { get; set; }
    }
}
