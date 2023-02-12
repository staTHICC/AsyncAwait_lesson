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
using AsyncAwait_App.Models;
using System.Net;
using System.Diagnostics;

namespace AsyncAwait_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            RunSyncLoad();

            stopWatch.Stop();

            long time = stopWatch.ElapsedMilliseconds;

            textBlock.Text += $"Total time: {time} mc\n\n";
        }

        private async void BtnAsync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await RunAsyncLoad();

            stopWatch.Stop();

            long time = stopWatch.ElapsedMilliseconds;

            textBlock.Text += $"Total time: {time} mc\n\n";
        }

        private async Task RunAsyncLoad()
        {
            List<string> sites = PrepareData();

            foreach (var site in sites)
            {
                DataModel model = await Task.Run(() => DownloadSite(site));
                ReportInfo(model);
            }
        }

        private void RunSyncLoad()
        {
            List<string> sites = PrepareData();

            foreach (var site in sites)
            {
                DataModel model = DownloadSite(site);
                ReportInfo(model);
            }
        }

        private void ReportInfo(DataModel dataModel)
        {
            textBlock.Text += $"Site: {dataModel.Url}, Length: {dataModel.Data.Length}\n";
        }

        private List<string> PrepareData()
        {
            List<string> list = new List<string>()
            {
                "https://dzen.ru/?yredirect=true",
                "https://vk.com/feed",
                "https://www.pinterest.de/"
            };

            return list;
        }

        private DataModel DownloadSite(string url)
        {
            // download from internet
            WebClient client = new WebClient();

            DataModel dataModel = new DataModel();

            dataModel.Url = url;
            dataModel.Data = client.DownloadString(url);

            return dataModel;
        }
    }
}
