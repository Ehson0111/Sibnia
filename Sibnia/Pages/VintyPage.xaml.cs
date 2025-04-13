using Sibnia.Models;
using Sibnia.Pages.EditingPage;
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
    /// Логика взаимодействия для VintyPage.xaml
    /// </summary>
    public partial class VintyPage : Page
    {

        private sibnia_practicaEntities db;
        public VintyPage()
        {
            InitializeComponent();
            DataContext = new Vinty();
            db=new sibnia_practicaEntities();
            LoadData(); 


        }

        private void LoadData() {

            db?.Dispose();

            // Создаем новый контекст
            db = new sibnia_practicaEntities();

            // Загружаем данные с отслеживанием изменений
            var viny = db.Vinty

                .AsNoTracking() // Отключаем отслеживание для повышения производительности
                .ToList();

            // Полностью обновляем ItemsSource
            employeesDataGrid.ItemsSource = null;
            employeesDataGrid.ItemsSource = viny;
            //employeesDataGrid.ItemsSource = model; // Привязка данных к Lis


        }
        private void adduser_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddEditVint());

        }
         



        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void employeesDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {
                    int vesyid = selectedEmployee.id_vint; // Получение ID сотрудника
                    NavigationService.Navigate(new AddEditVint(vesyid)); // Переход на страницу редактирования
                }
            }

        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Helpel.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());

                LoadData();

            }

        }

        private void showsvyaz_Click(object sender, RoutedEventArgs e)
        {
            if (employeesDataGrid.SelectedItem is Vinty selectedVesy)
            {
                NavigationService.Navigate(new VintyVesyDisplayPage(selectedVesy.id_vint));
            }
            else
            {
                MessageBox.Show("Выберите весы!");
            }
        }


        private void privyzatvesam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BindVintyToVesyPage());

        }
    }
}
