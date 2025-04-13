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
    /// Логика взаимодействия для TrubyPage.xaml
    /// </summary>
    public partial class TrubyPage : Page
    {
        private sibnia_practicaEntities db = new sibnia_practicaEntities();
        public TrubyPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            db?.Dispose();

            // Создаем новый контекст
            db = new sibnia_practicaEntities();

            // Загружаем данные с отслеживанием изменений
            var model = db.AerodinamicheskieTruby

                .AsNoTracking() // Отключаем отслеживание для повышения производительности
                .ToList();

            // Полностью обновляем ItemsSource
            employeesDataGrid.ItemsSource = null;
            employeesDataGrid.ItemsSource = model;
            //employeesDataGrid.ItemsSource = model; // Привязка данных к Lis
        }

        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (employeesDataGrid.SelectedItem != null)
            {
                var selected = employeesDataGrid.SelectedItem as dynamic;
                if (selected != null)
                {
                    int idmodels = selected.id_truba;
                    NavigationService.Navigate(new AddEdiTruba(idmodels));
                }
            }
        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddEdiTruba());

        }
         

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Helpel.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());

                LoadData();

            }
        }
    }
}
