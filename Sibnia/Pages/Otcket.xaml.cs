using Sibnia.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Otcket.xaml.cs
namespace Sibnia.Pages
{
    public partial class Otcket : Page
    {
        private sibnia_practicaEntities context = new sibnia_practicaEntities();

        public Otcket()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            // Загрузка данных из БД без преобразований
            var dbData = context.Vesy
                .AsEnumerable() // Переключаемся на LINQ to Objects
                .Select(v => new ReportItem
                {
                    VesyName = v.nazvanie_vesov,
                    Truba = v.AerodinamicheskieTruby?.nazvanie_truby,
                    TipVesov = v.tip_vesov?.tip_vesov1,
                    Modeli = string.Join(", ", v.ModeliSamolyotov.Select(m => m.nazvanie_modeli)),
                    Vinty = string.Join(", ", v.Vinty.Select(vt => vt.nomer_vinta)),
                    GraduirovkiCount = v.Graduirovki.Count
                }).ToList();

            dgReport.ItemsSource = dbData;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filePath = System.IO.Path.Combine(desktopPath, "report.txt");

                var sb = new StringBuilder();
                sb.AppendLine("Отчет по схемам связей");
                sb.AppendLine($"Дата генерации: {DateTime.Now:g}");
                sb.AppendLine("".PadRight(40, '='));

                foreach (ReportItem item in dgReport.Items)
                {
                    sb.AppendLine($"• Весы: {item.VesyName}");
                    sb.AppendLine($"• Труба: {item.Truba}");
                    sb.AppendLine($"• Тип весов: {item.TipVesov}");
                    sb.AppendLine($"• Модели: {item.Modeli}");
                    sb.AppendLine($"• Винты: {item.Vinty}");
                    sb.AppendLine($"• Градуировки: {item.GraduirovkiCount}");
                    sb.AppendLine("".PadRight(30, '-'));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

                MessageBox.Show($"Файл успешно сохранен:\n{filePath}",
                                "Экспорт завершен",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте:\n{ex.Message}",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }

    public class ReportItem 
    {
        public string VesyName { get; set; }
        public string Truba { get; set; }
        public string TipVesov { get; set; }
        public string Modeli { get; set; }
        public string Vinty { get; set; }
        public int GraduirovkiCount { get; set; }
    }
}