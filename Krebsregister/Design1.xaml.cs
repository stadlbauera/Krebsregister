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
using System.Windows.Shapes;

namespace Krebsregister
{
    /// <summary>
    /// Interaktionslogik für Design1.xaml
    /// </summary>
    public partial class Design1 : Window
    {
        public Design1()
        {
            InitializeComponent();
            LiveCharts.Wpf.GeoMap geoMap = new LiveCharts.Wpf.GeoMap();
            Random r = new Random();
            Dictionary<string, double> map = new Dictionary<string, double>();
            map["W"] = r.Next(0, 100);
            map["OOE"] = r.Next(0, 100);
            map["NOE"] = r.Next(0, 100);
            map["BGL"] = r.Next(0, 100);
            map["SBG"] = r.Next(0, 100);
            map["KTN"] = r.Next(0, 100);
            map["TIR"] = r.Next(0, 100);
            map["VAB"] = r.Next(0, 100);
            map["SMK"] = r.Next(0, 100);
            geoMap.HeatMap = map;
            geoMap.Source = @"Austria.xml";

            DataContext = this;


        }

        private void d1bNewCancerRegistry_Click(object sender, RoutedEventArgs e)
        {
            new CancerRegistry1().Show();

        }

        private void erstellen_Click(object sender, RoutedEventArgs e)
        {
            new CancerRegistry1().Show();
        }

        private void Beenden(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
    }
}
