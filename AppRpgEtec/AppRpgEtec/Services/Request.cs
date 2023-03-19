using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AppRpgEtec.Services;

public class Request
{
    public async Task<int> PostReturnIntAsync<TResult>(string uri, TResult data)
    {
        HttpClient httpClient = new();

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string serialized = await response.Content.ReadAsStringAsync();

            return int.Parse(serialized);
        }else
        {
            return 0;
        }
    }

    public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token)
    {
        HttpClient httpClient = new();

        /* Enviar uma autorização do tipo token */
        httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        string serialized = await response.Content.ReadAsStringAsync();
        TResult result = data;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));

        return result;
    }
}
