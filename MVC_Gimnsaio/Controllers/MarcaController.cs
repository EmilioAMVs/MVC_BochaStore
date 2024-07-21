using Microsoft.AspNetCore.Mvc;
using MVC_BOCHA_STORE.Models;
using MVC_BOCHA_STORE.Service;

namespace MVC_BOCHA_STORE.Controllers;
public class MarcaController : Controller
{
    private readonly IAPIServiceMarca _apiService;
    private readonly IServiceBusService _busService;

    public MarcaController(IAPIServiceMarca IAPIService, IServiceBusService busService)
    {
        _apiService = IAPIService;
        _busService = busService;

    }

    // // GET: ProductoController
    public async Task<IActionResult> Index()
    {
        try
        {
            List<Marca> marcas = await _apiService.GetMarca();
            return View(marcas);
        }
        catch (Exception error)
        {
            return View();
        }
    }

    // GET: ProductoController/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Marca marca) //Aquí recibo el objeto de tipo marca
    {
        try
        {
            if (marca != null)
            {
                // Invoca a la API y envio la nueva marca
                await _apiService.CreateMarca(marca);

                // Enviar un mensaje a la cola de Service Bus
                await _busService.SendMessageAsync($"Brand added: \n Id: {marca.idMarca}" +
                    $"\n Name:{marca.nombreMarca}", QueueType.Brands);

                // Redirije a la vista principal
                return RedirectToAction("Index");
            }

        }
        catch (Exception error)
        {
            return View();
        }
        return View();
    }


    // GET: MarcaController/Edit/5

    public async Task<IActionResult> Edit(int idMarca)
    {
        try
        {
            // Trae la marca en base al ID desde la API
            Marca marcaEncontrada = await _apiService.GetMarcaByID(idMarca);
            if (marcaEncontrada != null)
            {
                // Retorna la marca a la vista
                return View(marcaEncontrada);
            }
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Marca newMarca)
    {
        try
        {
            if (newMarca != null)
            {
                // Envía a la API la nueva marca con su ID
                await _apiService.UpdateMarca(newMarca, newMarca.idMarca);

                return RedirectToAction("Index");
            }
            // Retorna la marca a la vista
            return RedirectToAction("Index");
        }
        catch (Exception error)
        {
            return RedirectToAction("Index");
        }
    }

    // GET: MarcaController/Delete/5
    public async Task<IActionResult> Delete(int idMarca)
    {
        try
        {
            if (idMarca != 0)
            {
                await _apiService.DeleteMarca(idMarca);
                await _busService.SendMessageAsync($"Bran with id: {idMarca}", QueueType.Brands);
                return RedirectToAction("Index");
            }
        }
        catch (Exception error)
        {
            return RedirectToAction();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int idMarca)
    {
        try
        {
            // Invoca a la API y trae la marca en base a su ID
            Marca marcaEncontrada = await _apiService.GetMarcaByID(idMarca);

            if (marcaEncontrada != null)
            {
                // Retorna la marca a la vista
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