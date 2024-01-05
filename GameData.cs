using System;
using RPG;

[Serializable]
public class GameData : ViewModelBase
{
    private int _afkBuyPrice;

    public int AfkBuyPrice
    {
        get => _afkBuyPrice;
        set
        {
            if (_afkBuyPrice != value)
            {
                _afkBuyPrice = value;
                OnPropertyChanged();
            }
        }
    }
    public double Cookies { get; set; }
    
    private double _multiplikator;

    public double Multiplikator
    {
        get => _multiplikator;
        set => _multiplikator = Math.Round(value, 2); 
    }
}