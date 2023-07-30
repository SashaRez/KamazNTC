using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class PrinterView
    {
        public int ID { get; set; }

        [DisplayName("Марка")]
        public string? MarkPrinter { get; set; }

        [DisplayName("Имя хоста")]
        public string? HostName { get; set; }

        [DisplayName("Серийный номер")]
        public string? snNumberPrinter { get; set; }

        [DisplayName("Инвентарный номер")]
        public string? InventoryNumberPrinter { get; set; }

        [DisplayName("IP-адрес")]
        public string? IPAddressPrinter { get; set; }

        [DisplayName("Кабинет")]
        public string? Cabinet { get; set; }
    }
}