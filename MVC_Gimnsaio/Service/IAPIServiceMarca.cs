using MVC_BOCHA_STORE.Models;

namespace MVC_BOCHA_STORE.Service;

public interface IAPIServiceMarca
{
    public Task<List<Marca>> GetMarca();
    public Task<Marca> CreateMarca(Marca newMarca);
    public Task<Marca> GetMarcaByID(int idMarca);
    public Task<Marca> UpdateMarca(Marca newMarca, int idMarca);
    public Task<string> DeleteMarca(int idMarca);
    public Task<List<Producto>> GetProductosDeUnaMarca(int idMarca);

}