using Sibnia.Models;
using Sibnia.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddEditVesy.xaml
    /// </summary>
    public partial class AddEditVesy : Page
    {
        private readonly sibnia_practicaEntities _db;
        private int _vesuId;
        private bool _isNeweVesy;
        /// <summary>
        /// режим добавление
        /// </summary>
        public AddEditVesy()
        {
            InitializeComponent();
            _db = new sibnia_practicaEntities();
            _isNeweVesy = true;
            DataContext = new Vesy();
            Del.Visibility = Visibility.Hidden;
            LoadComboBoxData();
        }
        /// <summary>
        /// режим изминение 
        /// </summary>
        /// <param name="vesyid"></param>
        public AddEditVesy(int vesyid)
        {
         
            InitializeComponent();
            _db = new sibnia_practicaEntities();
            _vesuId = vesyid;
            _isNeweVesy = false;
            LoadData(vesyid);
        }

        private void LoadComboBoxData()
        {
            cmbtip_vesov.ItemsSource = _db.tip_vesov.ToList();
            cmbtruba.ItemsSource = _db.AerodinamicheskieTruby.ToList();
        }

        private void LoadData(int id)
        {
            var vesy = _db.Vesy
                .AsNoTracking()
                .Include(v => v.tip_vesov)
                .Include(v => v.AerodinamicheskieTruby)
                .FirstOrDefault(v => v.id_vesy == id);

            //var vesy1= _db.Vesy.Find(id);
            //var vesy2 = _db.Vesy.FirstOrDefault(x=>x.id_vesy==id);
            if (vesy == null)
            {
                MessageBox.Show("Весы не найдены!");
                NavigationService.GoBack();
                return;
            }

            // Создаем новый объект для редактирования
            DataContext = _db.Vesy.Find(id);
            //{
            //    id_vesy = vesy.id_vesy,
            //    //nazvanie_vesov = vesy.nazvanie_vesov,
            //    id_tip_vesov = vesy.id_tip_vesov,
            //    kolichestvo_komponent = vesy.kolichestvo_komponent,
            //    put_pasporta = vesy.put_pasporta,
            //    put_chertezha = vesy.put_chertezha,
            //    komponenty = vesy.komponenty,
            //    id_truba = vesy.id_truba
            //};

            LoadComboBoxData();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vesy = DataContext as Vesy;
                if (vesy == null) return;

                // Валидация
                if (string.IsNullOrWhiteSpace(vesy.nazvanie_vesov) || vesy.id_tip_vesov == 0)
                {
                    MessageBox.Show("Заполните обязательные поля!");
                    return;
                }

                if (_isNeweVesy)
                {
                    _db.Vesy.Add(vesy);
                }
                else
                {
                    var existingVesy = _db.Vesy.Find(_vesuId);
                    if (existingVesy == null)
                    {
                        MessageBox.Show("Весы не найдены в базе данных!");
                        return;
                    }

                    // Обновляем только изменяемые поля
                    existingVesy.nazvanie_vesov = vesy.nazvanie_vesov;
                    existingVesy.id_tip_vesov = vesy.id_tip_vesov;
                    existingVesy.kolichestvo_komponent = vesy.kolichestvo_komponent;
                    existingVesy.put_pasporta = vesy.put_pasporta;
                    existingVesy.put_chertezha = vesy.put_chertezha;
                    existingVesy.komponenty = vesy.komponenty;
                    existingVesy.id_truba = vesy.id_truba;
                }

                _db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}\n\n{ex.InnerException?.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    base.OnNavigatedFrom(e);
        //    _db?.Dispose();
        ////}
        //private void Cancel_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.GoBack();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BrowseDrawing_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Документы|*.pdf;*.doc;*.docx|Все файлы|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                // Установка пути через ViewModel
                chertezha.Text = dialog.FileName;
            }
        }

        private void Browsepassport_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Документы|*.pdf;*.doc;*.docx|Все файлы|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                // Установка пути через ViewModel
                passport.Text = dialog.FileName;
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var vesy =_db.Vesy.Find(_vesuId);
            //_db = new sibnia_practicaEntities();
            _db.Vesy.Remove(vesy);
            _db.SaveChanges();

            NavigationService.GoBack();
        }
    }
}
