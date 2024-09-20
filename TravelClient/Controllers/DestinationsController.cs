using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;

namespace TravelClient.Controllers;
public class DestinationsController : Controller
{
    // The Index() action 
    public IActionResult Index()
    {
        List<Destination> destinations = Destination.GetDestinations();
        return View(destinations);
    }

    public IActionResult Details(int id)
    {
        Destination dest = Destination.GetDetails(id);
        return View(dest);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Destination dest)
    {
        Destination.Post(dest);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Destination dest = Destination.GetDetails(id);
        return View(dest);
    }

    [HttpPost]
    public IActionResult Edit(Destination dest)
    {
        Destination.Put(dest);
        return RedirectToAction("Details", new { id = dest.DestinationId});
    }

    public IActionResult Delete(int id)
    {
        Destination dest = Destination.GetDetails(id);
        return View(dest);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        Destination.Delete(id);
        return RedirectToAction("Index");
    }
}