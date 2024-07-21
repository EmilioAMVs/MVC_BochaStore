using System.Text;
using Newtonsoft.Json;
using System.Text;
using MVC_BOCHA_STORE.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_BOCHA_STORE.Service
{
    public class APIServiceProducto : IAPIServiceProducto
    {
        private static string _baseURL;

        HttpClient httpClient = new HttpClient();

        public APIServiceProducto()
        {
            // AÃ±adir el archivo JSON al contenedor
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseURL = builder.GetSection("ApiSettings:BaseUrl").Value;

            httpClient.BaseAddress = new Uri(_baseURL);
        }

        public async Task<Producto> CreateProducto(Producto newProducto)
        {
            var json = JsonConvert.SerializeObject(newProducto);
            var newProductoJSON = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a POST request to the API
            HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "api/Producto", newProductoJSON);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a list of Producto objects
                Producto productoNuevo = JsonConvert.DeserializeObject<Producto>(content);
                return productoNuevo;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task<string> DeleteProducto(int idProducto)
        {
            // Send a GET request to the API
            HttpResponseMessage response = await httpClient.DeleteAsync(_baseURL + "api/Producto/" + idProducto);
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

        public async Task<List<Producto>> GetProductos()
        {
            // Send a GET request to the API
            HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Producto");
            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON string to a list of Producto objects
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(content);
                return productos;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task<Producto> GetProductoByID(int idProducto)
        {
            // Send a GET request to the API
            HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Producto/" + idProducto);
            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON string to a list of Producto objects
                Producto productoEncontrado = JsonConvert.DeserializeObject<Producto>(content);

                return productoEncontrado;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task<Producto> UpdateProducto(Producto newProducto, int idProducto)
        {
            var json = JsonConvert.SerializeObject(newProducto);

            var newProductoJSON = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a PUT request to the API
            HttpResponseMessage response = await httpClient.PutAsync(_baseURL + "api/Producto/" + idProducto, newProductoJSON);
            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON string to a list of Producto objects
                Producto productoModificado = JsonConvert.DeserializeObject<Producto>(content);
                return productoModificado;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}