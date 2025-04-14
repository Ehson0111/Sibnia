using Sibnia.Models;
using Sibnia.Pages.EditingPage;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sibnia.Pages
{
    public partial class GraduationListPage : Page
    {
        private sibnia_practicaEntities db;

        public GraduationListPage()
        {
            InitializeComponent();
            db = new sibnia_practicaEntities();
            LoadData();
        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditGraduationPage());
        }

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db?.Dispose();
                db = new sibnia_practicaEntities();
                LoadData();
            }
        }

        private void employeesDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (employeesDataGrid.SelectedItem is Graduirovki selected)
            {
                NavigationService.Navigate(new AddEditGraduationPage(selected.id_graduirovka));
            }
        }

        private void LoadData()
        {
            var data = db.Graduirovki
                .Include(g => g.Vesy)
                .AsNoTracking()
                .ToList();

            employeesDataGrid.ItemsSource = data;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            // Реализация экспорта при необходимости
        }

        private void employeesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (employeesDataGrid.SelectedItem is Graduirovki selected)
            {
                NavigationService.Navigate(new AddEditGraduationPage(selected.id_graduirovka));
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db?.Dispose();
                db = new sibnia_practicaEntities();
                LoadData();
            }

        }
    }
}