using System;

[Serializable]
public class GameData
{
    public double Cookies { get; set; }
    
    private double _multiplikator;

    public double Multiplikator
    {
        get => _multiplikator;
        set => _multiplikator = Math.Round(value, 2); 
    }
}