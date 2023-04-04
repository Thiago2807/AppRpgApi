using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;

namespace AppRpgEtec.ViewModels;

public class AppShellViewModel : BaseViewModels
{
    private UsuariosServices uService;

    public AppShellViewModel()
    {
        string token = Preferences.Get("UsuarioToken", string.Empty);
        uService = new UsuariosServices(token);
        CarregarUsuario();
    }

    private byte[] foto;
    public byte[] Foto
    {
        get => foto;
        set
        {
            foto = value;
            OnPropertyCharged();
        }
    }
    public async void CarregarUsuario()
    {
        try
        {
            int usuarioId = Preferences.Get("UsuarioId", 0);
            Usuario u = await uService.GetUsuarioAsync(usuarioId);
            Foto = u.Foto;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
            .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
        }
    }
}
