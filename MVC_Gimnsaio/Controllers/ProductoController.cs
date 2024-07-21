using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_BOCHA_STORE.Models;
using MVC_BOCHA_STORE.Service;

namespace MVC_BOCHA_STORE.Controllers;

public class ProductoController : Controller
{
    private readonly IAPIServiceProducto _apiService;
    private readonly IAPIServiceProovedor _apiServiceProovedor;
    private readonly IAPIServiceMarca _apiServiceMarca;
    private readonly IServiceBusService _busService;

    public ProductoController(IAPIServiceProducto IAPIService, IAPIServiceProovedor APIServiceProovedor, IAPIServiceMarca APIServiceMarca, IServiceBusService busService)
    {
        _apiService = IAPIService;
        _apiServiceProovedor = APIServiceProovedor;
        _apiServiceMarca = APIServiceMarca;
        _busService = busService;
    }

    // // GET: ProductoController
    public async Task<IActionResult> Index()
    {
        try
        {
            List<Producto> productos = await _apiService.GetProductos();
            return View(productos);
        }
        catch (Exception error)
        {
            return View();
        }
    }


    // GET: Producto/Controller/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Producto producto) //Recibe un objeto del tipo producto
    {
        try
        {
            if (producto != null)
            {
                // Invoco a la API y le envío el nuevo producto
                await _apiService.CreateProducto(producto);

                // Enviar un mensaje a la cola de Service Bus
                await _busService.SendMessageAsync($"Product added: \n Id: {producto.idProducto}" +
                    $"\n Name:{producto.nombreProducto}\n Description: {producto.descripcionProducto}" +
                    $"\n Price: {producto.precio}",QueueType.Products);

                // Redirige a la vista principal
                return RedirectToAction("Index");
            }
        }
        catch (Exception error)
        {
            return View();
        }
        return View();
    }

    // GET: ProductoController/Edit/5
    public async Task<IActionResult> Editar(int idProducto)
    {
        try
        {
            //Trae el producto en base al ID desde la API
            Producto productoEncontrado = await _apiService.GetProductoByID(idProducto);
            List<Marca> marcas = await _apiServiceMarca.GetMarca();
            List<Proovedor> proovedores = await _apiServiceProovedor.GetProovedor();

            if (productoEncontrado != null)
            {
                // Pásala a la vista
                ViewBag.Proovedor = new SelectList(proovedores, "idProovedor", "nombreProovedor");
                ViewBag.Marca = new SelectList(marcas, "idMarca", "nombreMarca");
                // Retorno el producto a la vista
                return View(productoEncontrado);
            }
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Producto nuevoProducto)
    {
        try
        {
            if (nuevoProducto != null)
            {
                // Envío a la API el nuevo producto con su ID
                await _apiService.UpdateProducto(nuevoProducto, nuevoProducto.idProducto);
                return RedirectToAction("Index");
            }
            // Retorna el producto a la vista
            return View(nuevoProducto);
        }
        catch (Exception error)
        {
            return View();
        }
    }


    // GET: ProductoController/Delete/5
    public async Task<IActionResult> Delete(int idProducto)
    {
        try
        {
            if (idProducto != 0)
            {
                await _apiService.DeleteProducto(idProducto);
                await _busService.SendMessageAsync($"Product with id: {idProducto} removed", QueueType.Products);
                return RedirectToAction("Index");
            }
        }
        catch (Exception error)
        {
            return RedirectToAction();
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> VerDetalleProducto(int idProducto)
    {
        try
        {
            //Trae el producto en base al ID desde la API
            Producto productoEncontrado = await _apiService.GetProductoByID(idProducto);

            if (productoEncontrado != null)
            {
                // Retorna el producto a la vista
                return View(productoEncontrado);
            }
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ProovedorAsociado(int idProducto)
    {
        try
        {
            //Trae el producto en base al ID desde la API
            Producto productoEncontrado = await _apiService.GetProductoByID(idProducto);

            Proovedor proovedorEncontrado = await _apiServiceProovedor.GetProovedorByID(productoEncontrado.idProovedor);
            if (proovedorEncontrado != null)
            {
                // Retorna el producto a la vista
                return View(proovedorEncontrado);
            }
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MarcaAsociada(int idProducto)
    {
        try
        {
            //Trae el producto en base al ID desde la API
            Producto productoEncontrado = await _apiService.GetProductoByID(idProducto);

            Marca marcaEncontrada = await _apiServiceMarca.GetMarcaByID(productoEncontrado.idMarca);

            if (marcaEncontrada != null)
            {
                // Retorna el producto a la vista
                return View(marcaEncontrada);
            }
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

}
       