namespace RPG;

public class MainWindowViewModel : ViewModelBase
{
    private string content;

    public string Content
    {
        get => content;
        set
        {
            if (content != value)
            {
                content = value;
                OnPropertyChanged();
            }
        }
    }
}