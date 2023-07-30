using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Classes
{
    public class Printer
    {
        [Key]
        public int ID { get; set; }

        [Column("Марка")]
        public string? MarkPrinter { get; set; }

        [Column("Имя хоста")]
        public string? HostName { get; set; }

        [Column("Серийный номер")]
        public string? snNumberPrinter { get; set; }

        [Column("Инвентарный номер")]
        public string? InventoryNumberPrinter { get; set; }

        [Column("IP-адрес")]
        public string? IPAddressPrinter { get; set; }

        [Column("Кабинет")]
        public string? Cabinet { get; set; }


    }
}