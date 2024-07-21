using Newtonsoft.Json;
using System.Text;
using MVC_BOCHA_STORE.Models;
using Newtonsoft.Json;
using System.Text;
using MVC_BOCHA_STORE.Service;

public class APIServiceMarca : IAPIServiceMarca
{
    private static string _baseURL;

    HttpClient httpClient = new HttpClient();

    public APIServiceMarca()
    {

        // Añadir el archivo JSON al contenedor
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseURL = builder.GetSection("ApiSettings:BaseUrl").Value;
        httpClient.BaseAddress = new Uri(_baseURL);

    }

    public async Task<List<Marca>> GetMarca()
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Marca");

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            List<Marca> marcas = JsonConvert.DeserializeObject<List<Marca>>(content);

            return marcas;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Marca> CreateMarca(Marca newMarca)
    {

        var json = JsonConvert.SerializeObject(newMarca);

        var newMarcaJSON = new StringContent(json, Encoding.UTF8, "application/json");

        // Send a POST request to the API
        HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "api/Marca", newMarcaJSON);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Membresia objects
            Marca marcaNueva= JsonConvert.DeserializeObject<Marca>(content);

            return marcaNueva;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Marca> GetMarcaByID(int idMarca)
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Marca/" + idMarca);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            Marca marcaEncontrada = JsonConvert.DeserializeObject<Marca>(content);

            return marcaEncontrada;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }

    public async Task<Marca> UpdateMarca(Marca newMarca, int idMarca)
    {

        var json = JsonConvert.SerializeObject(newMarca);
        var newMarcaJSON = new StringContent(json, Encoding.UTF8, "application/json");

        // Send a PUT request to the API
        HttpResponseMessage response = await httpClient.PutAsync(_baseURL + "api/Marca/" + idMarca, newMarcaJSON);

        // Ensure the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a list of Producto objects
            Marca marcaModificada = JsonConvert.DeserializeObject<Marca>(content);

            return marcaModificada;
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }

    }


    public async Task<string> DeleteMarca(int idMarca)
    {
        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.DeleteAsync(_baseURL + "api/Marca/" + idMarca);

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

    public async Task<List<Producto>> GetProductosDeUnaMarca(int idProovedor)
    {

        // Send a GET request to the API
        HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "/ProductosDeUnaMarca/" + idProovedor);

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
