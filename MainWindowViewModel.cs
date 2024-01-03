using System;
using CommunityToolkit.Mvvm.Input;

namespace RPG;

public class MainWindowViewModel : ViewModelBase
{
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

    private double Cookies { get; set; }
    private double Multiplikator { get; set; } = 0.1;

    public RelayCommand<object> Times { get; }

    public RelayCommand Count { get; }

    public MainWindowViewModel()
    {
        ContentRefresh();
        Count = new RelayCommand(CountMethod);
        Times = new RelayCommand<object>(TimesMethod);
    }

    private void TimesMethod(object zahl)
    {
        double price = Convert.ToDouble(zahl);
        if (Cookies >= price)
        {
            Cookies -= price;
            switch (zahl)
            {
                case "10":
                    Multiplikator += 0.1;
                    break;
                case "50":
                    Multiplikator += 0.5;
                    break;
                case "100":
                    Multiplikator += 1;
                    break;
                case "200":
                    Multiplikator += 2;
                    break;
                case "500":
                    Multiplikator += 5;
                    break;
                case "1000":
                    Multiplikator += 10;
                    break;
                case "2000":
                    Multiplikator += 20;
                    break;
                case "5000":
                    Multiplikator += 50;
                    break;
                case "10000":
                    Multiplikator += 100;
                    break;
            }

            InfoVisible = false;
            ContentRefresh();
        }
        else
        {
            InfoVisible = true;
        }
    }

    private void ContentRefresh()
    {
        CookieContent = $"Cookies: {Cookies:F1}";
        PerClickContent = $"Pro Click: {Multiplikator:F1}";
    }

    private void CountMethod()
    {
        ContentRefresh();
        Cookies += Multiplikator;
        InfoVisible = false;
    }
}