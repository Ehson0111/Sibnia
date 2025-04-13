using Sibnia.Models;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages.EditingPage
{
    public partial class VintyVesyDisplayPage : Page
    {
        private sibnia_practicaEntities db = new sibnia_practicaEntities();
        public Vinty Vint { get; set; }

        public VintyVesyDisplayPage(int vintId)
        {
            InitializeComponent();
            LoadData(vintId);
            this.DataContext = this;
        }

        private void LoadData(int vintId)
        {
            Vint = db.Vinty
                     .Include(v => v.Vesy.Select(ves => ves.tip_vesov))
                     .FirstOrDefault(v => v.id_vint == vintId);

            if (Vint == null)
            {
                MessageBox.Show("Винт не найден!");
                NavigationService.GoBack();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}