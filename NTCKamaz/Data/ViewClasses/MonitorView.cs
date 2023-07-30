using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class MonitorView
    {
        public int? ID { get; set; }

        [DisplayName("Марка монитора")]
        public string? MarkMonitor { get; set; }

        [DisplayName("Поставщик")]
        public string? Provider { get; set; }

        [DisplayName("SN монитора")]
        public string? SNMonitor { get; set; }

        [DisplayName("Инвентарный номер")]
        public string? InventoryNumber { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }

    }
}