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
    /// Логика взаимодействия для VesyPage.xaml
    /// </summary>
    public partial class VesyPage : Page
    {
        private static sibnia_practicaEntities db = new sibnia_practicaEntities();
        public VesyPage()
        {

            InitializeComponent();
            //datagridVesy.ItemsSource= db.Vesy.ToList();
            //DataContext= db.Vesy.ToList();
            LoadData();
        }

        private void LoadData()
        {
            var Vesy=db.Vesy.ToList();

            employeesDataGrid.ItemsSource = Vesy; // Привязка данных к ListView

        }
        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void employeesDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            //NavigationService.Navigate(new AddEditVesy());
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {
                    int vesyid = selectedEmployee.id_vesy; // Получение ID сотрудника
                    NavigationService.Navigate(new AddEditVesy(vesyid)); // Переход на страницу редактирования
                }
            }

        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adduser_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditVesy());

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Helpel.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());

                LoadData();

            }
        }
    }
}
