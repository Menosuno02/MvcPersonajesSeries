using MvcPersonajesSeries.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcPersonajesSeries.Services
{
    public class ServiceAPIPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceAPIPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiAzure");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                    return default(T);
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            return await this.CallApiAsync<List<Personaje>>(request);
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(string serie)
        {
            string request = "api/Personajes/PersonajesSeries/" + serie;
            return await this.CallApiAsync<List<Personaje>>(request);
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/Series";
            return await this.CallApiAsync<List<string>>(request);
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "api/Personajes/" + idpersonaje;
            return await this.CallApiAsync<Personaje>(request);
        }

        public async Task CreatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/DeletePersonaje/" + idpersonaje;
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
