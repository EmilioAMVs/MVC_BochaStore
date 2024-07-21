using Microsoft.AspNetCore.Mvc;
using MVC_BOCHA_STORE.Models;
using MVC_BOCHA_STORE.Service;

namespace MVC_BOCHA_STORE.Controllers;

public class ProovedorController : Controller
{
    private readonly IAPIServiceProovedor _apiService;
    private readonly IServiceBusService _busService;
    public ProovedorController( IAPIServiceProovedor IAPIService, IServiceBusService busService)
    {
        _apiService = IAPIService;
        _busService = busService;
    }
    
    // // GET: ProovedorController
        public async Task<IActionResult> Index()
        {
            try 
            {
                List<Proovedor> proovedores = await _apiService.GetProovedor();
                return View(proovedores);
            }
            catch (Exception error)
            {
                return View();
            }
        }

        // GET: ProovedorController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proovedor proovedor) //Recibe un objeto del tipo proovedor
        {
            try
            {
                if (proovedor != null)
                {
                    // Invoca a la API y envia el nuevo proovedor
                    await _apiService.CreateProovedor(proovedor);
                // Enviar un mensaje a la cola de Service Bus
                await _busService.SendMessageAsync($"Supllier added: \n Id: {proovedor.idProovedor}" +
                    $"\n Name:{proovedor.nombreProovedor}\n Contract Duration: {proovedor.duracionContrato}" +
                    $"\n Importation Price: {proovedor.precioImportacion}", QueueType.Suppliers);
                // Redirije a la vista principal
                return RedirectToAction("Index"); 
                }

            } catch (Exception error)
            {
                return View();
            }
            return View();
        }

        // GET: ProovedorController/Edit/5
        
        public async Task<IActionResult> Edit(int idProovedor)
        {
            try
            {
            // Trae el proovedor en base al ID desde la API
            Proovedor proovedorEncontrado = await _apiService.GetProovedorByID(idProovedor);
                if (proovedorEncontrado != null)
                {
                    // Retorno el proovedor a la vista
                    return View(proovedorEncontrado);
                }
            }
            catch (Exception error)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Proovedor newProovedor)
        {
            try
            {
                if (newProovedor != null)
                {
                    // Envío a la API el nuevo proovedor junto al ID
                    await _apiService.UpdateProovedor(newProovedor, newProovedor.idProovedor);

                    return RedirectToAction("Index");
                }
                // Retorno el proovedor a la vista
                return RedirectToAction("Index");
            }
            catch (Exception error) 
            {
                return RedirectToAction("Index");
            }
        }
        
        // GET: ProovedorController/Delete/5
        public async Task<IActionResult> Delete(int idProovedor)
        {
            try
            {
                if (idProovedor != 0)
                {
                    await _apiService.DeleteProovedor(idProovedor);
                await _busService.SendMessageAsync($"Supplier with id: {idProovedor} removed", QueueType.Suppliers);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception error)
            {
                return RedirectToAction();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int idProovedor)
        {
            try
            {
                // Invoco a la API para traer el proovedor en base al ID
                Proovedor proovedorEncontrado = await _apiService.GetProovedorByID(idProovedor);

                if (proovedorEncontrado != null)
                {
                    // Retorna el proovedor a la vista
                    return View(proovedorEncontrado);
                }
            }
            catch (Exception error)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
}