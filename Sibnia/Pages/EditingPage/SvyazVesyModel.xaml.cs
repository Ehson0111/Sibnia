using Sibnia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Для метода Include
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sibnia.Pages.EditingPage
{
    public partial class SvyazVesyModel : Page
    {
        // Экземпляр контекста базы данных
        private sibnia_practicaEntities db = new sibnia_practicaEntities();
        private int modelId;
        private ModeliSamolyotov currentModel;

        public SvyazVesyModel(int modelId)
        {
            InitializeComponent();
            this.modelId = modelId;
            LoadData();
        }

        /// <summary>
        /// Загрузка данных о модели и соответствующих весах.
        /// </summary>
        private void LoadData()
        {
            try
            {
                // Загружаем модель с привязанными весами и типами весов
                currentModel = db.ModeliSamolyotov
                                .Include("Vesy.tip_vesov")
                                .FirstOrDefault(m => m.id_model == modelId);
                if (currentModel == null)
                {
                    MessageBox.Show("Модель не найдена!");
                    NavigationService.GoBack();
                    return;
                }

                // Отображаем информацию о модели
                txtModelName.Text = currentModel.nazvanie_modeli;
                txtModelNumber.Text = currentModel.nomer_modeli;

                // Загружаем все аэродинамические весы
                var aeroVesy = db.Vesy
                                .Where(v => v.id_tip_vesov == 1 || v.id_tip_vesov==2) 
                                .ToList();
                comboAeroVesy.ItemsSource = aeroVesy;

                // Устанавливаем выбранный аэродинамический вес (если уже привязан)
                var currentAero = currentModel.Vesy
                                    .FirstOrDefault(v => v.id_tip_vesov==1);
                if (currentAero != null)
                {
                    comboAeroVesy.SelectedValue = currentAero.id_vesy;
                }

                // Загружаем все весы для винтовых характеристик
                var vintVesy = db.Vesy
                                .Where(v => v.id_tip_vesov==3)
                                .ToList();
                // Формируем список для выбора с флагами
                var vintSelections = new List<VintVesySelection>();
                foreach (var v in vintVesy)
                {
                    vintSelections.Add(new VintVesySelection
                    {
                        Vesy = v,
                        IsSelected = currentModel.Vesy.Any(mv => mv.id_vesy == v.id_vesy)
                    });
                }
                listVintVesy.ItemsSource = vintSelections;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }

        /// <summary>
        /// Сохранение выбранных весов для модели.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем текущие привязки весов
            currentModel.Vesy.Clear();

            // Добавляем выбранные аэродинамические весы
            if (comboAeroVesy.SelectedValue != null)
            {
                int selectedAeroId = (int)comboAeroVesy.SelectedValue;
                var selectedAero = db.Vesy.Find(selectedAeroId);
                if (selectedAero != null)
                    currentModel.Vesy.Add(selectedAero);
            }

            // Добавляем выбранные весы для винтов (могут быть несколько)
            if (listVintVesy.ItemsSource is List<VintVesySelection> vintSelections)
            {
                foreach (var sel in vintSelections)
                {
                    if (sel.IsSelected)
                    {
                        var selectedVesy = db.Vesy.Find(sel.Vesy.id_vesy);
                        if (selectedVesy != null)
                            currentModel.Vesy.Add(selectedVesy);
                    }
                }
            }

            try
            {
                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        /// <summary>
        /// Переход назад без сохранения изменений.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    /// <summary>
    /// Класс-обёртка для выбора весов для винтов с флагом.
    /// </summary>
    public class VintVesySelection
    {
        public Vesy Vesy { get; set; }
        public bool IsSelected { get; set; }
        public string nazvanie_vesov => Vesy?.nazvanie_vesov;
    }
}
