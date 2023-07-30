using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class AssemblyView
    {
        public int ID { get; set; }

        [DisplayName("Процессор")]
        public string? Processor { get; set; }

        [DisplayName("Материнская плата")]
        public string? Motherboard { get; set; }

        [DisplayName("ОЗУ")]
        public string? RAM { get; set; }

        [DisplayName("Видеокарта")]
        public string? Videocard { get; set; }

        [DisplayName("HDD")]
        public string? HDD { get; set; }

        [DisplayName("SSD")]
        public string? SSD { get; set; }

        [DisplayName("Блок питания")]
        public string? BP { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }
    }
}