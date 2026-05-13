using Microsoft.AspNetCore.Mvc;
using PetTracker.Models;
using PetTracker.Services;
using PetTracker.ViewModels;
using System.Linq;

namespace PetTracker.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        public IActionResult Index()
        {
            var pets = _petService.GetAllPets();
            return View(pets);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _petService.AddPet(viewModel);
                ViewBag.Message = "Питомец добавлен!";
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Age(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
                return NotFound();
            return View(pet);
        }

        public IActionResult ByType(string type)
        {
            var all = _petService.GetAllPets();

            List<Pet> filtered = new List<Pet>();

            if (type == "Кот")
            {
                filtered = all.Where(p => p.Type == "Кот" || p.Type == "кот").ToList();
            }
            else if (type == "Собака")
            {
                filtered = all.Where(p => p.Type == "Собака" || p.Type == "собака").ToList();
            }
            else
            {
                filtered = all;
            }

            return View("Index", filtered);
        }
    }
}