using Newtonsoft.Json;
using Sibnia.Models;
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
    /// Логика взаимодействия для Gradiurovka.xaml
    /// </summary>
    public partial class Gradiurovka : Page
    {
        private Dictionary<string, Dictionary<string, double>> _originalData;
        private int _graduationId;

        public Gradiurovka(int graduationId)
        {
            InitializeComponent();
            _graduationId = graduationId;
            Loaded += (s, e) => LoadGraduationData();
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGraduationData();
        }

        private void LoadGraduationData()
        {
            using (var context = new sibnia_practicaEntities())
            {
                var graduation = context.Graduirovki
                    .FirstOrDefault(g => g.id_graduirovka == _graduationId);
                if (graduation != null)
                {

                    if (graduation != null && !string.IsNullOrEmpty(graduation.dannye))
                    {
                        _graduationId = graduation.id_graduirovka;
                        _originalData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(graduation.dannye);
                        DisplayData(_originalData);
                    }
                    else
                    {
                        MessageBox.Show("No graduation data found");
                    }
                }
            }
        }

        private void DisplayData(Dictionary<string, Dictionary<string, double>> data)
        {
            var rows = new List<GraduationRow>();

            foreach (var item in data)
            {
                rows.Add(new GraduationRow
                {
                    Parameter = item.Key,
                    X = item.Value.ContainsKey("X") ? item.Value["X"] : 0,
                    Y = item.Value.ContainsKey("Y") ? item.Value["Y"] : 0,
                    Z = item.Value.ContainsKey("Z") ? item.Value["Z"] : 0,
                    MX = item.Value.ContainsKey("MX") ? item.Value["MX"] : 0,
                    MY = item.Value.ContainsKey("MY") ? item.Value["MY"] : 0,
                    MZ = item.Value.ContainsKey("MZ") ? item.Value["MZ"] : 0
                });
            }

            dataGrid.ItemsSource = rows;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedRow = (GraduationRow)e.Row.Item;
                var column = e.Column as DataGridTextColumn;

                if (column != null)
                {
                    var textBox = e.EditingElement as TextBox;
                    if (textBox != null)
                    {
                        // Обновляем оригинальные данные при редактировании
                        var param = editedRow.Parameter;
                        var field = column.Header.ToString();

                        if (!_originalData[param].ContainsKey(field))
                        {
                            _originalData[param][field] = 0;
                        }

                        if (double.TryParse(textBox.Text, out double newValue))
                        {
                            _originalData[param][field] = newValue;
                        }
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new sibnia_practicaEntities())
                {
                    var graduation = context.Graduirovki.FirstOrDefault(g => g.id_graduirovka == _graduationId);
                    if (graduation != null)
                    {
                        graduation.dannye = JsonConvert.SerializeObject(_originalData);
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadGraduationData();
        }
    }
}
