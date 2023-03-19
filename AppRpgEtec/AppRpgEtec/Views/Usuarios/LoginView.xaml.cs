using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class LoginView : ContentPage
{
	UsuariosViewModels usuariosViewModels;

	public LoginView()
	{
		InitializeComponent();

		usuariosViewModels = new UsuariosViewModels();
		BindingContext = usuariosViewModels;
	}
}