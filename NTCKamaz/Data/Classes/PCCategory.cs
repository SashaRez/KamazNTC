using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCCategory
    {

        [Key]
        public int ID { get; set; }

        [Column("Категория")]
        public string? pcCategory { get; set; }
    }
}
