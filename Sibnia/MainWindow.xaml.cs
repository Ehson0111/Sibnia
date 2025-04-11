using Newtonsoft.Json;
using Sibnia.Models;
using Sibnia.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sibnia
{
    public partial class MainWindow : Window
    {
        //private Dictionary<string, Dictionary<string, double>> _originalData;
        //private int _graduationId;

        public MainWindow()
        {
            InitializeComponent();
            //Loaded += MainWindow_Loaded;
            fr_content.Content= new Page1();


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            fr_content.GoBack();

        }
        private void fr_content_ContentRendered(object sender, EventArgs e)
        {

            if (fr_content.CanGoBack)
                btnBack.Visibility = Visibility.Visible;
            else
                btnBack.Visibility = Visibility.Hidden;
        }

        //private void btnVesy_Click(object sender, RoutedEventArgs e)
        //{
        //    fr_content.Content = new VesyPage();
        //}

        //private void btnModel_Click(object sender, RoutedEventArgs e)
        //{
        //    fr_content.Content = new ModelyPage();

        //}

        //private void btnVinty_Click(object sender, RoutedEventArgs e)
        //{
        //    fr_content.Content = new VintyPage();

        //}

        //private void btnGradiurovka_Click(object sender, RoutedEventArgs e)
        //{
        //    fr_content.Content = new Gradiurovka();

        //}

        //private void btnTruba_Click(object sender, RoutedEventArgs e)
        //{
        //    fr_content.Content = new TrubyPage();

        //}
    }
}