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

namespace Sibnia.Pages.EditingPage
{
    /// <summary>
    /// Логика взаимодействия для AddEditVint.xaml
    /// </summary>
    public partial class AddEditVint : Page
    {
        bool isnewobject;
        //public  MyProperty { get; set; }


        int idcurrent;
        private sibnia_practicaEntities db = new sibnia_practicaEntities();

        public AddEditVint()
        {
            InitializeComponent();

            isnewobject = true;
            DataContext = new Vinty();


            Del.Visibility = Visibility.Hidden;
        }
        public AddEditVint(int id)
        {
            InitializeComponent();

            idcurrent = id;
            isnewobject = false;

            LoadData();
        }
        private void LoadData()
        {

            try
            {

                var models = db.Vinty.FirstOrDefault(x => x.id_vint == idcurrent);

                if (models == null)
                {

                    MessageBox.Show("Не найдено");
                    NavigationService.GoBack();
                    return;
                }

                DataContext = models;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex);
                throw;
            }


            //cmbtip_vesov.ItemsSource = db.Vesy.ToList();
            //cmbtip_vesov.ItemsSource = _db.tip_vesov.ToList();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {

            var modelsam = db.Vinty.Find(idcurrent);
            db.Vinty.Remove(modelsam);
            db.SaveChanges();

            MessageBox.Show("Успешно ");
            NavigationService.GoBack();


        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = DataContext as Vinty;
                if (model == null)
                {
                    MessageBox.Show("Ошибка данных модели!");
                    return;
                }

                // Валидация данных
                if (string.IsNullOrWhiteSpace(model.nomer_vinta))
                {
                    MessageBox.Show("Введите номер!");
                    return;
                }
                //var vesy = Helpel.GetContext().ModeliSamolyotov.FirstOrDefault(x => x.id_model == 1).Vesy.First().nazvanie_vesov;
                //MessageBox.Show("ВЕсы" + vesy);



                //if (string.IsNullOrWhiteSpace(model.nomer_modeli))
                //{
                //    MessageBox.Show("Введите номер модели!");
                //    return;
                //}

                if (isnewobject)
                {
                    // Для новой модели
                    db.Vinty.Add(model);
                }
                else
                {
                    // Для редактирования существующей
                    var editmodel = db.Vinty.Find(idcurrent);
                    if (editmodel == null)
                    {
                        MessageBox.Show("Не найдено ");
                        return;
                    }

                    editmodel.nomer_vinta = model.nomer_vinta;
                    //editmodel.nomer_modeli = model.nomer_modeli;
                }

                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.GoBack();
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

    }
}
