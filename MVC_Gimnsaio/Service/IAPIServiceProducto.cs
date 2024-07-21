using MVC_BOCHA_STORE.Models;

namespace MVC_BOCHA_STORE.Service;

public interface IAPIServiceProducto
{
    Task<Producto> CreateProducto(Producto newProducto);
    Task<string> DeleteProducto(int idProducto);
    Task<List<Producto>> GetProductos();
    Task<Producto> GetProductoByID(int idProducto);
    Task<Producto> UpdateProducto(Producto newProducto, int idProducto);
}