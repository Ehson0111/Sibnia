using Sibnia.Models;
using System.Data.Entity; // Для Include
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages.EditingPage
{
    public partial class VesyDisplayPage : Page
    {
        private sibnia_practicaEntities db = new sibnia_practicaEntities();
        public ModeliSamolyotov Model { get; set; }

        public VesyDisplayPage(int modelId)
        {
            InitializeComponent();
            LoadData(modelId);
            this.DataContext = this;
        }

        private void LoadData(int modelId)
        {
            // Загружаем модель с весами и их типами (Include используется для загрузки связанных сущностей)
            Model = db.ModeliSamolyotov
                      .Include(m => m.Vesy.Select(v => v.tip_vesov))
                      .FirstOrDefault(m => m.id_model == modelId);
            if (Model == null)
            {
                MessageBox.Show("Модель не найдена!");
                NavigationService.GoBack();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
