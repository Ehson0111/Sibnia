﻿using System;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnVesy_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VesyPage());
        }

        private void btnModel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModelyPage());

        }

        private void btnVinty_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VintyPage());

        }

        private void btnGradiurovka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Gradiurovka());

        }

        private void btnTruba_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TrubyPage());

        }
    }
}
