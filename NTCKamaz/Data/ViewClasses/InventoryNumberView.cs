using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class InventoryNumberView
    {

        public int ID { get; set; }

        [DisplayName("Инвентарный номер")]
        public string? InventoryNumbers { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }

        [DisplayName("ID Монитора")]
        public int? monitorID { get; set; }

    }
}