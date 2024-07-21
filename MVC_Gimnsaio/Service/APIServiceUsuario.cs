using Microsoft.AspNetCore.Mvc;
using MVC_BOCHA_STORE.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_BOCHA_STORE.Service
{
    public class APIServiceUsuario : IAPIServiceUsuario
    {

        private static string _baseURL;

        HttpClient httpClient = new HttpClient();

        public APIServiceUsuario()
        {

            // Añadir el archivo JSON al contenedor
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseURL = builder.GetSection("ApiSettings:BaseUrl").Value;
            httpClient.BaseAddress = new Uri(_baseURL);

        }

        public async Task<List<Usuario>> GetUsuarios()
        {

            // Send a GET request to the API
            HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "api/Usuarios");

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {

                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a list of Producto objects
                List<Usuario> listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);

                return listaUsuarios;

            }
            else
            {

                throw new Exception($"Error: {response.StatusCode}");

            }

        }

        public async Task<bool> ValidarUsuario(Usuario UserToValidate)
        {
            
            List<Usuario> listaUsuarios = await GetUsuarios();

            Usuario ValidUser = listaUsuarios.SingleOrDefault(data => data.username.Equals(UserToValidate.username) && data.password.Equals(UserToValidate.password));

            if (ValidUser != null)
            {
                return true;
            }   
            return false;

        }

        public async Task<Usuario> CreateUsuario(Usuario newUsuario)
        {

            var json = JsonConvert.SerializeObject(newUsuario);

            var newUsuarioJSON = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a POST request to the API
            HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "api/Usuarios", newUsuarioJSON);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {

                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a list of Membresia objects
                Usuario usuarioNuevo = JsonConvert.DeserializeObject<Usuario>(content);

                return usuarioNuevo;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }

        }


    }
}
