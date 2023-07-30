using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class InventoryNumber
    {
        [Key]
        public int ID { get; set; }

        [Column("Инвентарный номер")]
        public string? inventoryNumber { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }

        [Column("ID Монитора")]
        public int? monitorID { get; set; }
    }
}