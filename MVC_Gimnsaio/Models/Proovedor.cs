using System.ComponentModel.DataAnnotations;

namespace MVC_BOCHA_STORE.Models;

public class Proovedor
{
    public int idProovedor { get; set; }
    public string nombreProovedor { get; set; }
    public int duracionContrato { get; set; }
    public double precioImportacion { get; set; }

}