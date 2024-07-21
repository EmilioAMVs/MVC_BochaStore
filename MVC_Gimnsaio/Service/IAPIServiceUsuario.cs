using MVC_BOCHA_STORE.Models;

namespace MVC_BOCHA_STORE.Service
{
    public interface IAPIServiceUsuario
    {

        Task<List<Usuario>> GetUsuarios();

        Task<bool> ValidarUsuario(Usuario UserToValidate);

        Task<Usuario> CreateUsuario(Usuario newUsuario);
    }
}
