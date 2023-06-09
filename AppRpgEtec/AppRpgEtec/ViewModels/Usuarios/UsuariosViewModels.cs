﻿using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using System.Windows.Input;
using AppRpgEtec.Views.Usuarios;
using AppRpgEtec.Helpers.Message;

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

                Models.Email email = new Models.Email();
                email.Remetente = "thiagoroque2807@gmail.com";
                email.RemetentePassword = "qgtbeaxlkfkqbijg";
                email.Destinatario = "thiagoroque2807@gmail.com";
                email.DominioPrimario = "smtp.gmail.com";
                email.PortaPrimaria = 587;
                email.Assunto = "Notificação de acesso";
                email.Mensagem = $"Usuário {u.UserName} acessou o aplicativo" +
                    $" em {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";

                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.EnviarEmail(email);

                await Application.Current.MainPage
                    .DisplayAlert("Informação", message, "Ok");

                Application.Current.MainPage = new AppShell();
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
