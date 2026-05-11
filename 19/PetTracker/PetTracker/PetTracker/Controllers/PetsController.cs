using Microsoft.AspNetCore.Mvc;
using PetTracker.Models;

namespace PetTracker.Controllers
{
    public class PetsController : Controller
    {
        private static List<Pet> _pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Барсик", Type = "Кот", Birthday = new DateTime(2020, 5, 15) },
            new Pet { Id = 2, Name = "Шарик", Type = "Собака", Birthday = new DateTime(2019, 8, 20) }
        };
        private static int _nextId = 3;

        public IActionResult Index()
        {
            return View(_pets);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Pet pet)
        {
            // Добавь эту строку для отладки
            System.Diagnostics.Debug.WriteLine($"Получен питомец: {pet?.Name}, {pet?.Type}, {pet?.Birthday}");

            if (ModelState.IsValid)
            {
                pet.Id = _nextId++;
                _pets.Add(pet);
                return RedirectToAction("Index");
            }
            return View(pet);
        }

        public IActionResult Age(int id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet == null)
                return NotFound();
            return View(pet);
        }
    }
}