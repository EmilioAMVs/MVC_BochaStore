using Newtonsoft.Json;
using System.Text;
using MVC_BOCHA_STORE.Models;
using Newtonsoft.Json;
using System.Text;
using MVC_BOCHA_STORE.Service;

public class APIServiceProovedor : IAPIServiceProovedor
{
    private static string _baseURL;

    HttpClient httpClient = new HttpClient();

    public APIServiceProovedor()
    {

        // Añadir el archivo JSON al contenedor
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseURL = builder.GetSection("ApiSettings:BaseUrl").Value;
        httpClient.BaseAddress = new Uri(_baseURL);

    }

    public async Task<List<Proovedor>> GetProovedor()
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Proovedor");

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            List<Proovedor> proovedores= JsonConvert.DeserializeObject<List<Proovedor>>(content);

            return proovedores;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Proovedor> CreateProovedor(Proovedor newProovedor)
    {

        var json = JsonConvert.SerializeObject(newProovedor);

        var newProovedorJSON = new StringContent(json, Encoding.UTF8, "application/json");

        // Send a POST request to the API
        HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "api/Proovedor", newProovedorJSON);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Membresia objects
            Proovedor proovedorNuevo = JsonConvert.DeserializeObject<Proovedor>(content);

            return proovedorNuevo;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Proovedor> GetProovedorByID(int idProovedor)
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Proovedor/" + idProovedor);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            Proovedor proovedorEncontrado = JsonConvert.DeserializeObject<Proovedor>(content);

            return proovedorEncontrado;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Proovedor> UpdateProovedor(Proovedor newProovedor, int idProovedor)
    {

        var json = JsonConvert.SerializeObject(newProovedor);
        var newProovedorJSON = new StringContent(json, Encoding.UTF8, "application/json");

        // Send a PUT request to the API
        HttpResponseMessage response = await httpClient.PutAsync(_baseURL + "api/Proovedor/" + idProovedor, newProovedorJSON);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            Proovedor proovedorModificado = JsonConvert.DeserializeObject<Proovedor>(content);

            return proovedorModificado;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }


    public async Task<string> DeleteProovedor(int idProovedor)
    {
        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.DeleteAsync(_baseURL + "api/Proovedor/" + idProovedor);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string mensaje = await response.Content.ReadAsStringAsync();

            return mensaje;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    // 3 Métodos del final adicionales de la API Membresía

    public async Task<List<Producto>> GetProductosDeUnProovedor(int idProovedor)
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "/ProductosDeUnProovedor/" + idProovedor);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            List<Producto> productosDeUnProovedor = JsonConvert.DeserializeObject<List<Producto>>(content);

            return productosDeUnProovedor;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }


}

