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
using WeatherService;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GetWeatherService _weatherService;
        private Root temp;
        private GetCity _cities;
        private List<string> cityList;
        private ApplicationContext _applicationContext;
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            _applicationContext = new ApplicationContext();
            _weatherService = new GetWeatherService();
            _cities = new GetCity();
            cityList = _cities.FillCity();

        }

        public static DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Local);
        }

        private void BtnSearch(object sender, RoutedEventArgs e)
        {
            temp = _weatherService.GetWeather(txtBoxCity.Text);
            if (temp == null || temp.cod == 404)
            {
                panelInfo.Visibility = Visibility.Hidden;
                snackBarMsg.MessageQueue.Enqueue("city not found");
            }
            else
             if (temp.cod == 200)
            {
                panelInfo.Visibility = Visibility.Visible;
                txtBlkCity.Text = temp.name;
                txtBlkCurTemp.Text = Math.Round(temp.main.temp).ToString() + "°C";
                txtBlkDate.Text = UnixTimestampToDateTime(temp.dt).ToString("MMM dd, ddd - HH:mm");
                txtBlkFeelsTemp.Text = "Feels like " + (int)temp.main.feels_like + "°C";
                txtBlkHumidity.Text = "Humidity: " + temp.main.humidity + "%";
                txtBlkWind.Text = "Wind: " + (int)temp.wind.speed + "m/s";
                txtBlkMinMax.Text = "Min: " + (int)temp.main.temp_min + "°C / Max: " + (int)temp.main.temp_max + "°C";
            }
        }

        private async void BtnSave(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(() => {
                Dispatcher.Invoke(() =>
                {
                    if (temp != null)
                    {
                        WeatherList newRecord = new WeatherList
                        {
                            date = UnixTimestampToDateTime(temp.dt).ToString("yyyy-MM-dd HH:mm tt"),
                            city = temp.name,
                            temp = (int)Math.Round(temp.main.temp),
                            temp_feels = (int)temp.main.feels_like,
                            humidity = temp.main.humidity,
                            wind = (int)temp.wind.speed,
                            temp_min = (int)temp.main.temp_min,
                            temp_max = (int)temp.main.temp_max,
                        };

                        _applicationContext.historyWeather.Add(newRecord);
                        _applicationContext.SaveChanges();
                    }


                });


            });
            await task;



        }

        private void BtnLoadHistory(object sender, RoutedEventArgs e)
        {
            var registerWindow = new historyList(_applicationContext);
            registerWindow.ShowDialog();
        }


        private async void LoadCityList()
        {
            var task = Task.Run(() => {
                Dispatcher.Invoke(() =>
                {
                    string txtBoxText = txtBoxCity.Text.Trim();

                    if (!String.IsNullOrEmpty(txtBoxText))
                    {
                        List<string> tempCityList = new List<string>();
                        foreach (string item in cityList)
                        {
                            if (item.StartsWith(txtBoxText))
                                tempCityList.Add(item);

                            if (tempCityList.Count >= 6)
                                break;
                        }
                        txtBoxCity.ItemsSource = tempCityList;
                        txtBoxCity.IsDropDownOpen = true;

                    }

                });


            });
            await task;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch(sender, e);
            }
        }


        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadCityList();
        }

        private static int Add(int a, int b)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            return a + b;
        }


    }
}
