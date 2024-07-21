using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_BOCHA_STORE.Models;

public class Producto
{
    public int idProducto { get; set; }
    
    public int idProovedor { get; set; }
    public int idMarca { get; set; }
    
    public string nombreProducto { get; set; }
    
    public string descripcionProducto { get; set; }
    
    public double precio { get; set; }
    public int stock { get; set; }
    
    public DateTime fechaInscripcion { get; set; }

}
