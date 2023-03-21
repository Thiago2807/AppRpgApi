using AppRpgEtec.Models;
using Microsoft.Maui.Controls.Shapes;

namespace AppRpgEtec.Services.Usuarios;

public class UsuariosServices : Request
{
    private readonly Request _request;
    private const string apiUriBase = "http://WebRpg.somee.com/RpgApi/Usuarios";
    //private const string apiUriBase = "https://bsite.net/luizfernando987/Usuarios";

    public UsuariosServices ()
    {
        _request = new Request();
    }

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
}
