using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class DepartmentView
    {
        public int ID { get; set; }

        [DisplayName("Сокращённое наименование")]
        public string? shortName { get; set; }

        [DisplayName("Полное наименование")]
        public string? fullName { get; set; }

        [DisplayName("Начальник")]
        public string? ChiefsName { get; set; }

    }
}
