using MVC_BOCHA_STORE.Models;

namespace MVC_BOCHA_STORE.Service;

public interface IAPIServiceProovedor
{
    public Task<List<Proovedor>> GetProovedor();
    public Task<Proovedor> CreateProovedor(Proovedor newProovedor);
    public Task<Proovedor> GetProovedorByID(int idProovedor);
    public Task<Proovedor> UpdateProovedor(Proovedor newProovedor, int idProovedor);
    public Task<string> DeleteProovedor(int idProovedor);
    public Task<List<Producto>> GetProductosDeUnProovedor(int idProovedor);

}