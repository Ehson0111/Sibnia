using Sibnia.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages
{
    public partial class BindVintyToVesyPage : Page
    {
        private readonly sibnia_practicaEntities _context = new sibnia_practicaEntities();
        private List<Vinty> _allVinty;

        public BindVintyToVesyPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка весов
            cbVesy.ItemsSource = _context.Vesy.ToList();

            // Загрузка всех винтов
            _allVinty = _context.Vinty.ToList();
            lvVinty.ItemsSource = _allVinty;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbVesy.SelectedItem is Vesy selectedVesy)
            {
                var selectedVinty = lvVinty.SelectedItems.Cast<Vinty>().ToList();

                // Очистка существующих связей
                selectedVesy.Vinty.Clear();

                // Добавление новых связей
                foreach (var vint in selectedVinty)
                {
                    selectedVesy.Vinty.Add(vint);
                }

                _context.SaveChanges();
                MessageBox.Show("Связи успешно сохранены!");
                NavigationService?.GoBack();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите весы!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}