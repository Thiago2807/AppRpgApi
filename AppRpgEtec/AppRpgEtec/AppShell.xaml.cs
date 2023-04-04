﻿using AppRpgEtec.ViewModels;

namespace AppRpgEtec;

public partial class AppShell : Shell
{
	AppShellViewModel viewModel;
	public AppShell()
	{
		InitializeComponent();

		viewModel = new AppShellViewModel();
		BindingContext = viewModel;

		string login = Preferences.Get("UsuarioUsername", string.Empty);
		lblLogin.Text = $"Login: {login}";
	}
}
