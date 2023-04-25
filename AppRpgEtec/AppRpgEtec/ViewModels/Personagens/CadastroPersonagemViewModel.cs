using AppRpgEtec.Models;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Services.Personagens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens;

public class CadastroPersonagemViewModel : BaseViewModels
{
    private PersonagemServices pService;

    public ICommand SalvarCommand { get; }

	public CadastroPersonagemViewModel()
	{
		string token = Preferences.Get("UsuarioToken", string.Empty);
		pService = new PersonagemServices(token);
        _ = ObterClasses();

        SalvarCommand = new Command(async () => { await SalvarPersonagem(); });
	}

	private int id;
	private string nome;
	private int pontosVida;
	private int forca;
	private int defesa;
	private int inteligencia;
	private int disputas;
	private int vitorias;
	private int derrotas;

    public int Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyCharged();//Informa mundaça de estado para a View
        }
    }

    public string Nome
    {
        get => nome;
        set
        {
            nome = value;
            OnPropertyCharged();
        }
    }
    public int PontosVida
    {
        get => pontosVida;
        set
        {
            pontosVida = value;
            OnPropertyCharged();
        }
    }

    public int Forca
    {
        get => forca;
        set
        {
            forca = value;
            OnPropertyCharged();
        }
    }

    public int Defesa
    {
        get => defesa;
        set
        {
            defesa = value;
            OnPropertyCharged();
        }
    }

    public int Inteligencia
    {
        get => inteligencia;
        set
        {
            inteligencia = value;
            OnPropertyCharged();
        }
    }

    public int Disputas
    {
        get => disputas;
        set
        {
            disputas = value;
            OnPropertyCharged();
        }
    }

    public int Vitorias
    {
        get => vitorias;
        set
        {
            vitorias = value;
            OnPropertyCharged();
        }
    }

    public int Derrotas
    {
        get => derrotas;
        set
        {
            derrotas = value;
            OnPropertyCharged();
        }
    }

    private ObservableCollection<TipoClasse> listaTipoClasses;

    public ObservableCollection<TipoClasse> ListaTipoClasses
    {
        get { return listaTipoClasses; }
        set
        {
            if (value != null)
            {
                listaTipoClasses = value;
                OnPropertyCharged();
            }
        }
    }

    private TipoClasse tipoClasseSelecionada;

    public TipoClasse TipoClasseSelecionada 
    { 
        get => tipoClasseSelecionada; 
        set
        {
            if (value != null)
            {
                tipoClasseSelecionada = value;
                OnPropertyCharged();
            }
        }
    }

    public async Task ObterClasses()
    {
        try {
            ListaTipoClasses = new ObservableCollection<TipoClasse>
            {
                new TipoClasse() { Id = 1, Descricao = "Cavaleiro" },
                new TipoClasse() { Id = 2, Descricao = "Mago" },
                new TipoClasse() { Id = 3, Descricao = "Clerigo" }
            };

            OnPropertyCharged(nameof(ListaTipoClasses));
        }
        catch(Exception ex) 
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
        }
    }

    public async Task SalvarPersonagem()
    {
        try
        {
            Personagem model = new Personagem()
            {
                Nome = this.nome,
                PontosVida = this.PontosVida,
                Defesa = this.Defesa,
                Derrotas = this.Derrotas,
                Disputas = this.Disputas,
                Forca = this.Forca,
                Inteligencia = this.Inteligencia,
                Vitorias = this.Vitorias,
                Id = this.Id,
                Classe = (ClasseEnum)tipoClasseSelecionada.Id
            };

            if (model.Id == 0)
                await pService.PostPersonagemAsync(model);

            await Application.Current.MainPage
                .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
        }
    }
}
