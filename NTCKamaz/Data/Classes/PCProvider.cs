using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCProvider
    {
        [Key]
        public int ID { get; set; }

        [Column("Поставщик")]
        public string? providerName { get; set; }
    }
}