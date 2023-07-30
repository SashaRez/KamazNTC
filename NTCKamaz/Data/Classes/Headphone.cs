using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class Headphone
    {
        [Key]
        public int ID { get; set; }

        [Column("Наушник")]
        public string? headphones { get; set; }
    }
}