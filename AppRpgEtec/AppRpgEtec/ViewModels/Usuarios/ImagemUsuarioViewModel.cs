using AppRpgEtec.Services.Usuarios;
using Plugin.Media;

namespace AppRpgEtec.ViewModels.Usuarios;

public class ImagemUsuarioViewModel : BaseViewModels
{
    private UsuariosServices uService;

	public ImagemUsuarioViewModel()
	{
		string token = Preferences.Get("UsuarioToken", string.Empty);
		uService = new UsuariosServices(token);
	}

	private ImageSource fonteImagem;
	public ImageSource FonteImagem
	{
		get { return fonteImagem;}
		set 
		{ 
			fonteImagem = value;
			OnPropertyCharged();
		}
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

	public async void Fotografar()
	{
		try
		{
			await CrossMedia.Current.Initialize();


		}
		catch (Exception ex)
		{

			await Application.Current.MainPage
				.DisplayAlert("Ops", ex.Message + " Detalhes " + ex.InnerException, "Ok");
		}
	}
}
