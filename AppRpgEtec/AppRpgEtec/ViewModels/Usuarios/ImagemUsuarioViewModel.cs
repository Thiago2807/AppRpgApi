using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using Plugin.Media;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios;

public class ImagemUsuarioViewModel : BaseViewModels
{
	private UsuariosServices uService;

	public ImagemUsuarioViewModel()
	{
		string token = Preferences.Get("UsuarioToken", string.Empty);
		uService = new UsuariosServices(token);

		SalvarImagemCommand = new Command(SalvarImagem);
		FotografarCommand = new Command(Fotografar);
        AbrirGaleriaCommand = new Command(AbrirGaleria);

		CarregarUsuario();
    }

	public ICommand SalvarImagemCommand { get; }
    public ICommand FotografarCommand	{ get; }
    public ICommand AbrirGaleriaCommand { get; }

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

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await Application.Current.MainPage.DisplayAlert("Sem Câmera", "A camêa não disponivel", "Ok");
				await Task.FromResult(false);
			}

			string fileName = String.Format($"{DateTime.Now.ToString("ddMMyyyy_HHmmss")}.jpg");

			var file = await CrossMedia.Current.TakePhotoAsync
			(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Fotos",
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
					Name = fileName,
				});

			if (file is null)
				await Task.FromResult(false);

			MemoryStream ms = null;

			using (ms = new MemoryStream())
			{
				var stream = file.GetStream();
				stream.CopyTo(ms);
			}

			FonteImagem = ImageSource.FromStream(() => file.GetStream());
			Foto = ms.ToArray();
		}
		catch (Exception ex)
		{

			await Application.Current.MainPage
				.DisplayAlert("Ops", ex.Message + " Detalhes " + ex.InnerException, "Ok");
		}
	}

	public async void SalvarImagem()
	{
		try
		{
			Usuario u = new();
			u.Foto = foto;
			u.Id = Preferences.Get("UsuarioId", 0);

			if (await uService.PutFotoUsuarioAsync(u) != 0)
			{
				await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
				await Application.Current.MainPage.Navigation.PopAsync();

            }
			else
				throw new Exception("Erro ao tentar atualizar imagem.");
		}
		catch (Exception ex)
		{
			await Application.Current.MainPage
				.DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
		}
	}

	public async void AbrirGaleria()
	{
		try
		{
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Galeria não suportada", "Você não tem permissão para acessar a galeria", "Ok");
                await Task.FromResult(false);
            }

			var file = await CrossMedia.Current.PickPhotoAsync();

			if (file is null)
				return;

			MemoryStream ms = null;
			using (ms = new MemoryStream())
			{
				var stream = file.GetStream();
				stream.CopyTo(ms);
			}

			FonteImagem = ImageSource.FromStream(() => file.GetStream());
			Foto = ms.ToArray();
			return;
        }
		catch (Exception ex)
		{
            await Application.Current.MainPage
                .DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
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
                .DisplayAlert("Ops", $"{ex.Message} Detalhes: {ex.InnerException}", "Ok");
        }
	}
}
