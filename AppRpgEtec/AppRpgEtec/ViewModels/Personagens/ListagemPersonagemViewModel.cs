using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using System.Collections.ObjectModel;

namespace AppRpgEtec.ViewModels.Personagens;

public class ListagemPersonagemViewModel : BaseViewModels
{
    private PersonagemServices pServices;
    public ObservableCollection<Personagem> Personagens { get; set; }

    public ListagemPersonagemViewModel()
    {
        string token = Preferences.Get("UsuarioToken", string.Empty);
        pServices = new PersonagemServices(token);
        Personagens = new ObservableCollection<Personagem>();
        _ = ObterPersonagens();
    }

    public async Task ObterPersonagens()
    {
        try
        {
            Personagens = await pServices.GetPersonagensAsync();
            OnPropertyCharged(nameof(Personagens));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
        }
    }
}


