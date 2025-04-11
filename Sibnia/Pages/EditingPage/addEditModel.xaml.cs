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
    /// Логика взаимодействия для addEditModel.xaml
    /// </summary>
    public partial class addEditModel : Page
    {

        bool isnewmodels;
        //public  MyProperty { get; set; }


        int idmodels;
        private sibnia_practicaEntities db = new sibnia_practicaEntities();
        public addEditModel()
        {
            InitializeComponent();
            isnewmodels = true;
            DataContext = new ModeliSamolyotov();


            Del.Visibility = Visibility.Hidden;

        }

        public addEditModel(int id)
        {
            InitializeComponent();
            idmodels = id;
            isnewmodels = false;

            LoadData();

        }
        private void LoadData()
        {

            try
            {

                var models = db.ModeliSamolyotov.FirstOrDefault(x => x.id_model == idmodels);

                if (models == null)
                {

                    MessageBox.Show("Модель не найден");
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


            cmbtip_vesov.ItemsSource=db.Vesy.ToList();
                    //cmbtip_vesov.ItemsSource = _db.tip_vesov.ToList();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {

            var modelsam = db.ModeliSamolyotov.Find(idmodels);
            db.ModeliSamolyotov.Remove(modelsam);
            db.SaveChanges();

            MessageBox.Show("Успешно ");
            NavigationService.GoBack();


        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = DataContext as ModeliSamolyotov;
                if (model == null)
                {
                    MessageBox.Show("Ошибка данных модели!");
                    return;
                }

                // Валидация данных
                if (string.IsNullOrWhiteSpace(model.nazvanie_modeli))
                {
                    MessageBox.Show("Введите название модели!");
                    return;
                }
                //var vesy = Helpel.GetContext().ModeliSamolyotov.FirstOrDefault(x => x.id_model == 1).Vesy.First().nazvanie_vesov;
                //MessageBox.Show("ВЕсы" + vesy);
                


                if (string.IsNullOrWhiteSpace(model.nomer_modeli))
                {
                    MessageBox.Show("Введите номер модели!");
                    return;
                }

                if (isnewmodels)
                {
                    // Для новой модели
                    db.ModeliSamolyotov.Add(model);
                }
                else
                {
                    // Для редактирования существующей
                    var editmodel = db.ModeliSamolyotov.Find(idmodels);
                    if (editmodel == null)
                    {
                        MessageBox.Show("Модель не найдена!");
                        return;
                    }

                    editmodel.nazvanie_modeli = model.nazvanie_modeli;
                    editmodel.nomer_modeli = model.nomer_modeli;
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

