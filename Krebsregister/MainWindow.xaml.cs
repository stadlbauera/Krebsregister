using LiveCharts.Wpf;
using LiveCharts;
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
using System.Net;
using System.IO;

using System.Security.Cryptography;

namespace Krebsregister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AreaChart();
            BarChart();
            PieChart();
            NegativStackChart();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private void AreaChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }
        public SeriesCollection SeriesCollectionBC { get; set; }
        public string[] LabelsBC { get; set; }
        public Func<double, string> Formatter { get; set; }
        private void BarChart()
        {
            SeriesCollectionBC = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollectionBC.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            //also adding values updates and animates the chart automatically
            SeriesCollectionBC[1].Values.Add(48d);

            LabelsBC = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }
        private void PieChart()
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public SeriesCollection SeriesCollectionNSC { get; set; }
        public string[] LabelsNSC { get; set; }
        public Func<double, string> FormatterNSC { get; set; }
        private void NegativStackChart()
        {
            SeriesCollectionNSC = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Title = "Male",
                    Values = new ChartValues<double> {.5, .7, .8, .8, .6, .2, .6}
                },
                new StackedRowSeries
                {
                    Title = "Female",
                    Values = new ChartValues<double> {-.5, -.7, -.8, -.8, -.6, -.2, -.6}
                }
            };

            LabelsNSC = new[] { "0-20", "20-35", "35-45", "45-55", "55-65", "65-70", ">70" };
            FormatterNSC = value => Math.Abs(value).ToString("P");

            DataContext = this;
        }
        private void bDesign1_Click(object sender, RoutedEventArgs e)
        {
            new Design1().Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //GetCSVDateien();
            createBundeslandDictionary();
            createGeschlechtDictionary();
            createKrebsDictionary();
            readEintraege();
            for(int i = 0; i < testEintraege.Count; i++)
            {
                lvtestEintraege.Items.Add(testEintraege[i].ToString());
            }
            

        }

        Dictionary<String, String> krebsdic = new Dictionary<string, string>(); 

        private void createKrebsDictionary()
        {
            WebRequest request = WebRequest.Create("https://data.statistik.gv.at/data/OGD_krebs_ext_KREBS_1_C-TUM_ICD10_3ST-0.csv");
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] fields = reader.ReadLine().Split(";");
                krebsdic.Add(fields[0], fields[1].Substring(6));
            }
            reader.Close();
            response.Close();
        }

        Dictionary<String, String> geschlechterdic = new Dictionary<string, string>();
        private void createGeschlechtDictionary()
        {
            WebRequest request = WebRequest.Create("https://data.statistik.gv.at/data/OGD_krebs_ext_KREBS_1_C-KRE_GESCHLECHT-0.csv");
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] fields = reader.ReadLine().Split(";");
                geschlechterdic.Add(fields[0], fields[1]);
            }
            reader.Close();
            response.Close();
        }

        Dictionary<String, String> bundeslaenderdic = new Dictionary<string, string>();

        private void createBundeslandDictionary()
        {
            WebRequest request = WebRequest.Create("https://data.statistik.gv.at/data/OGD_krebs_ext_KREBS_1_C-BUNDESLAND-0.csv");
            request.Method = "GET"; 
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] fields = reader.ReadLine().Split(";");
                bundeslaenderdic.Add(fields[0], fields[1]);
            }
            reader.Close();
            response.Close();
        }

        private void GetCSVDateien()
        {
            //CSV-Dateien herunterladen

            List<string> urls = new List<string>();
            urls.Add("OGD_krebs_ext_KREBS_1");
            urls.Add("OGD_krebs_ext_KREBS_1_HEADER");
            urls.Add("OGD_krebs_ext_KREBS_1_C-TUM_ICD10_3ST-0");
            urls.Add("OGD_krebs_ext_KREBS_1_C-BERJ-0");
            urls.Add("OGD_krebs_ext_KREBS_1_C-BUNDESLAND-0");
            urls.Add("OGD_krebs_ext_KREBS_1_C-KRE_GESCHLECHT-0");

            for (int i = 0; i < urls.Count; i++)
            {
                string url = $@"https://data.statistik.gv.at/data/{urls[i]}.csv";

                using (WebClient wc = new WebClient())
                {
                    byte[] buffer = wc.DownloadData(url);
                    string path = $@"C:\Users\Markus Stadlbauer\Documents\Schule\5. Klasse\PRE\Projekt\{urls[i]}.csv";
                    Stream stream = new FileStream(path, FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(buffer);
                    stream.Close();
                }
            }
        }
        List<TestEintrag> testEintraege = new List<TestEintrag>();

        private void readEintraege()
        {
            
                WebRequest request = WebRequest.Create("https://data.statistik.gv.at/data/OGD_krebs_ext_KREBS_1.csv");
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    TestEintrag newEintrag;
                    TryParse(line, out newEintrag);
                    testEintraege.Add(newEintrag);
                 }
                reader.Close();
                response.Close();

            

            
        }

        private void TryParse(string line, out TestEintrag newEintrag)
        { 
            string[] fields = line.Split(";");
            newEintrag = new TestEintrag { ICD10 = krebsdic[fields[0]],
                Berichtsjahr = Int32.Parse(fields[1].Replace("BERJ-", "")),
                Bundesland = bundeslaenderdic[fields[2]],
                Geschlecht = geschlechterdic[fields[3]],
                AnzahlMeldungen = Int32.Parse(fields[4])};
        }
    }
}
