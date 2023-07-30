using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class PCView
    {
        public string? ID { get; set; }

        [DisplayName("Имя ПК")]
        public string? pcName { get; set; }

        [DisplayName("Дата выдачи")]
        public string? dateOfIssue { get; set; }

        [DisplayName("Пользователь")]
        public string? userName { get; set; }

        [DisplayName("Отдел")]
        public string? departmentName { get; set; }

        [DisplayName("Марка")]
        public string? pcMark { get; set; }

        [DisplayName("SN системного блока")]
        public string? snSystemBlock { get; set; }

        [DisplayName("Характеристика")]
        public string? characteristic { get; set; }

        [DisplayName("Поставщик")]
        public string? providerName { get; set; }

        [DisplayName("Категория")]
        public string? category { get; set; }

        [DisplayName("ОС")]
        public string? OS { get; set; }

        [DisplayName("Инвентарный номер")]
        public string? inventoryNumber { get; set; }

        [DisplayName("Скл №ПК")]
        public string? skladNumberPC { get; set; }

        [DisplayName("Монитор")]
        public string? monitorName { get; set; }

        [DisplayName("SN монитора")]
        public string? snMonitor { get; set; }

        [DisplayName("Переферийные устройства")]
        public string? additionalDevices { get; set; }

        [DisplayName("Кабинет")]
        public string? cabinet { get; set; }

        [DisplayName("МОЛ")]
        public string? MOL { get; set; }

        [DisplayName("Проект")]
        public string? projectName { get; set; }

        [DisplayName("Основание")]
        public string? foundation { get; set; }

        [DisplayName("Примечание")]
        public string? note { get; set; }
    }
}
