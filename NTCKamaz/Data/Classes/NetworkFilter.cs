using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class NetworkFilter
    {
        [Key]
        public int ID { get; set; }

        [Column("Сетевой фильтр")]
        public string? networkFilter { get; set; }
    }
}