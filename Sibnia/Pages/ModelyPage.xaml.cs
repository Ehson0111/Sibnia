using Sibnia.Models;
using Sibnia.Services;
using System;
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

namespace Sibnia.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModelyPage.xaml
    /// </summary>
    public partial class ModelyPage : Page
    {
        private static sibnia_practicaEntities db;
        public ModelyPage()
        {
            InitializeComponent();

            db = new sibnia_practicaEntities();
            //DataContext = db.ModeliSamolyotov.ToList();
            LoadData();
        
        }

        private void LoadData()
        {
            var Vesy = db.ModeliSamolyotov.ToList();

            employeesDataGrid.ItemsSource = Vesy; // Привязка данных к Lis
        }

        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }


        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
