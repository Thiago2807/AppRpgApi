using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class CadastroView : ContentPage
{
	UsuariosViewModels usuarioViewModels;

	public CadastroView()
	{
		InitializeComponent();

		usuarioViewModels = new UsuariosViewModels();
		BindingContext = usuarioViewModels;

    }
}