using AppRpgEtec.Models;
using Microsoft.Maui.Controls.Shapes;

namespace AppRpgEtec.Services.Usuarios;

public class UsuariosServices : Request
{
    private readonly Request _request;
    private const string apiUriBase = "http://WebRpg.somee.com/RpgApi/Usuarios";
    //private const string apiUriBase = "https://bsite.net/luizfernando987/Usuarios";

    private string _token;

    public UsuariosServices(string token)
    {
        _request = new();
        _token = token;
    }

    public UsuariosServices ()
    {
        _request = new Request();
    }

    #region Métodos

    public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
    {
        const string urlComplementar = "/Registrar";

        u.Id = await _request.PostReturnIntAsync(apiUriBase + urlComplementar, u);
        return u;
    }

    public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
    {
        const string urlComplementar = "/Autenticar";

        u = await _request.PostAsync(apiUriBase + urlComplementar, u, string.Empty);

        return u;
    }

    public async Task<int> PutFotoUsuarioAsync(Usuario u)
    {
        string urlComplementar = "/AtualizarFoto";
        var result = await _request.PutAsync(apiUriBase + urlComplementar, u, _token);
        return result;
    }

    public async Task<Usuario> GetUsuarioAsync(int usuarioId)
    {
        string urlComplementar = string.Format("/{0}", usuarioId);
        var usuario = await
        _request.GetAsync<Models.Usuario>(apiUriBase + urlComplementar, _token);
        return usuario;
    }

    #endregion
}
