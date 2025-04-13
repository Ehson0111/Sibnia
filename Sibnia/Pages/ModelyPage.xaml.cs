using Sibnia.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages
{
    public partial class ModelyPage : Page
    {
        private sibnia_practicaEntities db = new sibnia_practicaEntities();

        public ModelyPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            employeesDataGrid.ItemsSource = db.ModeliSamolyotov.ToList();
        }

        // Обработчик для кнопок "Привязать весы" и "Показать связы"
        private void joinmodel_Click(object sender, RoutedEventArgs e)
        {
            if (employeesDataGrid.SelectedItem is ModeliSamolyotov selectedModel)
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    if (btn.Name == "SvyasVesyModel")
                    {
                        // Переход к странице привязки весов
                        NavigationService.Navigate(new EditingPage.SvyazVesyModel(selectedModel.id_model));
                    }
                    else if (btn.Name == "SvyasView")
                    {
                        // Переход к странице отображения весов
                        NavigationService.Navigate(new EditingPage.VesyDisplayPage(selectedModel.id_model));
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите модель из списка.");
            }
        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {
            // Здесь код для добавления новой модели (реализация по необходимости)
        }

        private void employeesDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // При двойном клике можно открыть страницу для редактирования модели
            //if (employeesDataGrid.SelectedItem is ModeliSamolyotov selectedModel)
            //{
            //    NavigationService.Navigate(new EditingPage.SvyazVesyModel(selectedModel.id_model));
            //}     

            //#endif

            if (employeesDataGrid.SelectedItem!=null)
            {
                var emleyesitem = employeesDataGrid.SelectedItem as dynamic;
                if (emleyesitem!=null)
                {
                    NavigationService.Navigate(new addEditModel(emleyesitem.id_model));
                    
                }
                
            }
            //#endif
        }
    }
}
