using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using AuthenticationServices;
using System.Windows.Input;
using AppRpgEtec.Views.Usuarios;

namespace AppRpgEtec.ViewModels.Usuarios;

public class UsuariosViewModels : BaseViewModels
{
    private UsuariosServices uService;
    public ICommand RegistrarCommand { get; set; }
    public ICommand AutenticarCommand { get; set; }
    public ICommand DirecionarCadastroCommand { get; set; }

    public UsuariosViewModels()
    {
        uService = new UsuariosServices();
        InicializarCommands();
    }

    public void InicializarCommands()
    {
        RegistrarCommand = new Command(async () => await RegistrarUsuario());
        AutenticarCommand = new Command(async () => await AutenticarUsuario());
        DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
    }

    #region  AtributosPropriedades

    private string login = string.Empty;
    public string Login
    {
        get { return login; }
        set
        {
            login = value;
            OnPropertyCharged();
        }
    }

    private string senha = string.Empty;
    public string Senha
    {
        get { return senha;  }
        set
        {
            senha = value;
            OnPropertyCharged();
        }
    }
    #endregion

    #region Métodos
    public async Task RegistrarUsuario()
    {
        try
        {
            Usuario u = new();

            u.UserName = Login;
            u.PasswordString = Senha;

            Usuario uRegistro = await uService.PostRegistrarUsuarioAsync(u);

            if (uRegistro.Id != 0)
            {
                string mensagem = $"Usuário Id {uRegistro.Id} registro com sucesso.";
                await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        catch(Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Informação", ex.Message + " Detalhes" + ex.InnerException, "Ok");
        }
    }

    public async Task AutenticarUsuario()
    {
        try
        {

            Usuario u = new();
            u.UserName = Login;
            u.PasswordString = Senha;

            Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

            if (!string.IsNullOrEmpty(uAutenticado.Token))
            {
                string message = $"Bem-Vindo(a) {uAutenticado.UserName}";

                Preferences.Set("UsuarioId",        uAutenticado.Id);
                Preferences.Set("UsuarioUserName",  uAutenticado.UserName);
                Preferences.Set("UsuarioPerfil",    uAutenticado.Perfil);
                Preferences.Set("UsuarioToken",     uAutenticado.Token);

                await Application.Current.MainPage
                    .DisplayAlert("Informação", message, "Ok");

                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await Application.Current.MainPage
                .DisplayAlert("Informação", "Dados incorretos :(", "Ok");
            }

        }
        catch (Exception ex) {
            await Application.Current.MainPage
                .DisplayAlert("Informação", ex.Message + " Detalhes", "Ok");
        }
    }

    public async Task DirecionarParaCadastro()
    {
        try
        {
            await Application.Current.MainPage.
                Navigation.PushAsync(new CadastroView());
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Informação", ex.Message + " Detalhes", "Ok");
        }
    }
    #endregion
}
