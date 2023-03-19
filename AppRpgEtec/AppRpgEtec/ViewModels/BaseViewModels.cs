using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppRpgEtec.ViewModels;

public class BaseViewModels : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyCharged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
