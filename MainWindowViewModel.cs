using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Xml.Serialization;
using CommunityToolkit.Mvvm.Input;

namespace RPG
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static Timer timer;
        private GameData GameData { get; set; }

        public MainWindowViewModel(GameData gameData, string cookieContent, string perClickContent)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            GameData = gameData;
            _cookieContent = cookieContent;
            _perClickContent = perClickContent;
            LoadGameData();
            CheckMultiplikator();
            ContentRefresh();
            Count = new RelayCommand(CountMethod);
            Times = new RelayCommand<object>(TimesMethod);
            AFKCookieCommand = new RelayCommand(AfkCookieCommandMethod);
        }

        private void AfkCookieCommandMethod()
        {
            if (GameData.AfkBuyPrice <= GameData.Cookies)
            {
                if (GameData.Multiplikator != AfkCookie)
                {
                    GameData.AfkBuyPrice *= 3;
                    Console.WriteLine("Preis erhöht.");
                }

                AfkCookie = GameData.Multiplikator;
                Console.WriteLine("AfkCookie Anzahl geupdated.");
                if (timer != null)
                {
                    timer.Enabled = false;
                    timer.Elapsed -= OnTimedEvent;
                    timer.Dispose();
                    Console.WriteLine("Timer gestoppt.");
                }

                timer = new Timer(1000);
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
                Console.WriteLine("Timer gestartet.");
            }
            else
            {
                InfoVisible = true;
                ContentRefresh();
                Console.WriteLine("Nicht genug Cookies.");
            }
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            SaveGameData();
        }

        private void CheckMultiplikator()
        {
            if (GameData.Multiplikator < 0.1)
            {
                GameData.Multiplikator += 0.1;
                AfkCookie = 0;
                GameData.AfkBuyPrice = 1;
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            GameData.Cookies += AfkCookie;
            ContentRefresh();
        }

        private void TimesMethod(object zahl)
        {
            double price = Convert.ToDouble(zahl);
            if (GameData.Cookies >= price)
            {
                GameData.Cookies -= price;
                switch (zahl)
                {
                    // Zehn
                    case "10":
                        GameData.Multiplikator += 0.1;
                        break;
                    case "50":
                        GameData.Multiplikator += 0.5;
                        break;
                    //Hundert
                    case "100":
                        GameData.Multiplikator += 1;
                        break;
                    case "200":
                        GameData.Multiplikator += 2;
                        break;
                    case "500":
                        GameData.Multiplikator += 5;
                        break;
                    //Tausend
                    case "1000":
                        GameData.Multiplikator += 10;
                        break;
                    case "2000":
                        GameData.Multiplikator += 20;
                        break;
                    case "5000":
                        GameData.Multiplikator += 50;
                        break;
                    //Zehntausend
                    case "10000":
                        GameData.Multiplikator += 100;
                        break;
                    case "20000":
                        GameData.Multiplikator += 200;
                        break;
                    case "50000":
                        GameData.Multiplikator += 500;
                        break;
                    //Hunderttausend
                    case "100000":
                        GameData.Multiplikator += 1000;
                        break;
                    case "200000":
                        GameData.Multiplikator += 2000;
                        break;
                    case "500000":
                        GameData.Multiplikator += 5000;
                        break;
                    //Millionen
                    case "1000000":
                        GameData.Multiplikator += 10000;
                        break;
                    case "2000000":
                        GameData.Multiplikator += 20000;
                        break;
                    case "5000000":
                        GameData.Multiplikator += 50000;
                        break;
                    //Zehnmillionen
                    case "10000000":
                        GameData.Multiplikator += 100000;
                        break;
                    case "20000000":
                        GameData.Multiplikator += 200000;
                        break;
                    case "50000000":
                        GameData.Multiplikator += 500000;
                        break;
                    //Hundertmillionen
                    case "100000000":
                        GameData.Multiplikator += 1000000;
                        break;
                    case "200000000":
                        GameData.Multiplikator += 2000000;
                        break;
                    case "500000000":
                        GameData.Multiplikator += 5000000;
                        break;
                    //Milliarden
                    case "1000000000":
                        GameData.Multiplikator += 10000000;
                        break;
                    case "2000000000":
                        GameData.Multiplikator += 20000000;
                        break;
                    case "5000000000":
                        GameData.Multiplikator += 50000000;
                        break;
                    case "10000000000":
                        GameData.Multiplikator += 100000000;
                        break;
                    case "20000000000":
                        GameData.Multiplikator += 200000000;
                        break;
                    case "50000000000":
                        GameData.Multiplikator += 500000000;
                        break;
                    case "100000000000":
                        GameData.Multiplikator += 1000000000;
                        break;
                }

                InfoVisible = false;
                ContentRefresh();
            }
            else
            {
                InfoVisible = true;
            }

            SaveGameData();
        }

        private void CountMethod()
        {
            ContentRefresh();
            GameData.Cookies = Math.Round(GameData.Cookies, 1) + Math.Round(GameData.Multiplikator, 1);
            InfoVisible = false;

            SaveGameData();
        }

        private void SaveGameData()
        {
            using (var writer = new StreamWriter("GameData.xml"))
            {
                var serializer = new XmlSerializer(typeof(GameData));
                serializer.Serialize(writer, GameData);
            }
        }

        private void LoadGameData()
        {
            if (File.Exists("GameData.xml"))
            {
                using (var reader = new StreamReader("GameData.xml"))
                {
                    var serializer = new XmlSerializer(typeof(GameData));
                    GameData = (GameData)serializer.Deserialize(reader);
                }
            }
        }

        public RelayCommand<object> Times { get; }
        public RelayCommand Count { get; }
        public RelayCommand AFKCookieCommand { get; }

        private string _afkMoneyContent;

        public string AFKMoneyContent
        {
            get => _afkMoneyContent;
            set
            {
                if (_afkMoneyContent != value)
                {
                    _afkMoneyContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cookieContent;

        public string CookieContent
        {
            get => _cookieContent;
            set
            {
                if (_cookieContent != value)
                {
                    _cookieContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _perClickContent;

        public string PerClickContent
        {
            get => _perClickContent;
            set
            {
                if (_perClickContent != value)
                {
                    _perClickContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _infoVisible;

        public bool InfoVisible
        {
            get => _infoVisible;
            set
            {
                if (value == _infoVisible) return;
                _infoVisible = value;
                OnPropertyChanged();
            }
        }

        private double AfkCookie { get; set; }

        private void ContentRefresh()
        {
            string formattedString = String.Format("{0:N1}", GameData.Cookies);
            formattedString = formattedString.Replace(",", ".");
            CookieContent = $"Cookies: {formattedString}";
            PerClickContent = $"Pro Click: {GameData.Multiplikator:F1}";
            AFKMoneyContent = GameData.AfkBuyPrice.ToString() + " Cookies";
        }
    }
}