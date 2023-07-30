using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCMonitor
    {
        [Key]
        public int ID { get; set; }

        [Column("ID Марки")]
        public int? markID { get; set; }

        [Column("ID Поставщика")]
        public int? providerID { get; set; }

        [Column("SN монитора")]
        public string? monitorSN { get; set; }

        [Column("ID Инвентарного номера")]
        public int? inventoryNumberID { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }
    }
}