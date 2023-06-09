﻿using AppRpgEtec.Models;
using System.Collections.ObjectModel;

namespace AppRpgEtec.Services.Personagens;

class PersonagemServices : Request
{
    private readonly Request _request;
    private const string apiUrlBase = "http://WebRpg.somee.com/RpgApi/Personagens";

    private string _token;

    public PersonagemServices(string token)
    {
        _request = new Request();
        _token = token;
    }

    public async Task<int> PostPersonagemAsync(Personagem p)
    {
        return await _request.PostReturnIntTokenAsync(apiUrlBase, p, _token);
    }

    public async Task<ObservableCollection<Personagem>> GetPersonagensAsync()
    {
        string urlComplementar = "/GetAll";
        ObservableCollection<Models.Personagem> listaPersonagens = await
        _request.GetAsync<ObservableCollection<Models.Personagem>>(apiUrlBase + urlComplementar,
        _token);
        return listaPersonagens;
    }

    public async Task<Personagem> GetPersonagemAsync(int personagemId)
    {
        string urlComplementar = string.Format("/{0}", personagemId);
        var personagem = await _request.GetAsync<Models.Personagem>(apiUrlBase +
        urlComplementar, _token);
        return personagem;
    }
    public async Task<int> PutPersonagemAsync(Personagem p)
    {
        var result = await _request.PutAsync(apiUrlBase, p, _token);
        return result;
    }
    public async Task<int> DeletePersonagemAsync(int personagemId)
    {
        string urlComplementar = string.Format("/{0}", personagemId);
        var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
        return result;
    }

}
