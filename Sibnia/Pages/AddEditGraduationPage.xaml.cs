using Newtonsoft.Json;
using Sibnia.Models;
using Sibnia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages
{
    public partial class AddEditGraduationPage : Page
    {
        private Graduirovki _currentGraduation;
        int? idcerrent;
        private ObservableCollection<GraduationRow> _editableParameters;

        private static readonly string[] ParameterKeys = new[]
        {
            "X", "Y", "Z", "MX", "MY", "MZ",
            "X²", "Y²", "Z²", "MX²", "MY²", "MZ²",
            "X*Y", "X*Z", "X*MX", "X*MY", "X*MZ",
            "Y*Z", "Y*MX", "Y*MY", "Y*MZ",
            "Z*MX", "Z*MY", "Z*MZ",
            "MX*MY", "MX*MZ", "MY*MZ"
        };

        public AddEditGraduationPage(int? graduationId = null)
        {
            InitializeComponent();
            idcerrent = graduationId;

            DataContext = new Graduirovki();
            using (var db = new sibnia_practicaEntities())
            {
                cbVesy.ItemsSource = db.Vesy.ToList();
                DataContext = db.Graduirovki.Find(graduationId);
                if (graduationId.HasValue)
                {
                    _currentGraduation = db.Graduirovki
                        .FirstOrDefault(g => g.id_graduirovka == graduationId);

                    if (_currentGraduation != null)
                    {
                        tbName.Text = _currentGraduation.nazvanie_graduirovki;
                        cbVesy.SelectedValue = _currentGraduation.id_vesy;

                        var matrix = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(_currentGraduation.dannye);

                        _editableParameters = new ObservableCollection<GraduationRow>(
                         matrix.Select(row => new GraduationRow
                         {
                             Parameter = row.Key,
                             X = row.Value.ContainsKey("X") ? row.Value["X"] : 0,
                             Y = row.Value.ContainsKey("Y") ? row.Value["Y"] : 0,
                             Z = row.Value.ContainsKey("Z") ? row.Value["Z"] : 0,
                             MX = row.Value.ContainsKey("MX") ? row.Value["MX"] : 0,
                             MY = row.Value.ContainsKey("MY") ? row.Value["MY"] : 0,
                             MZ = row.Value.ContainsKey("MZ") ? row.Value["MZ"] : 0
                         }));

                    }
                }
                else
                {
                    _currentGraduation = new Graduirovki { data_graduirovki = DateTime.Now };


                    _editableParameters = new ObservableCollection<GraduationRow>(
                        ParameterKeys.Select(p => new GraduationRow
                        {
                            Parameter = p,
                            X = 0,
                            Y = 0,
                            Z = 0,
                            MX = 0,
                            MY = 0,
                            MZ = 0
                        }));
                }
            }

            dataGrid.ItemsSource = _editableParameters;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var matrix = _editableParameters.ToDictionary(
                    row => row.Parameter,
                    row => new Dictionary<string, double>
                    {
                        { "X", row.X },
                        { "Y", row.Y },
                        { "Z", row.Z },
                        { "MX", row.MX },
                        { "MY", row.MY },
                        { "MZ", row.MZ }
                    });

                using (var db = new sibnia_practicaEntities())
                {
                    _currentGraduation.nazvanie_graduirovki = tbName.Text;
                    _currentGraduation.id_vesy = (int)cbVesy.SelectedValue;
                    _currentGraduation.id_vesy = (int)cbVesy.SelectedValue;
                    _currentGraduation.data_graduirovki = DateTime.Now;
                    _currentGraduation.dannye = JsonConvert.SerializeObject(matrix);

                    if (_currentGraduation.id_graduirovka == 0)
                        db.Graduirovki.Add(_currentGraduation);
                    else
                        db.Entry(_currentGraduation).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private sibnia_practicaEntities _db;


        private void dtnDel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _db = new sibnia_practicaEntities();

                var grad = _db.Graduirovki.Find( idcerrent);
                //_db = new sibnia_practicaEntities();
                _db.Graduirovki.Remove(grad);
                _db.SaveChanges();

                NavigationService.GoBack();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка удаление т.к" + ex);
                //throw;
            }
        }
    }

    public class GraduationRow
    {
        public string Parameter { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double MX { get; set; }
        public double MY { get; set; }
        public double MZ { get; set; }
    }
}
