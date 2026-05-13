using PetTracker.Models;
using PetTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetTracker.Services
{
    public class PetService : IPetService
    {
        private static List<Pet> _pets = new List<Pet>();
        private static int _nextId = 1;

        public PetService()
        {
            if (_pets.Count == 0)
            {
                _pets.Add(new Pet { Id = _nextId++, Name = "Барсик", Type = "Кот", Birthday = new DateTime(2020, 5, 15) });
                _pets.Add(new Pet { Id = _nextId++, Name = "Шарик", Type = "Собака", Birthday = new DateTime(2019, 8, 20) });
            }
        }

        public List<Pet> GetAllPets()
        {
            return _pets;
        }

        public void AddPet(PetViewModel viewModel)
        {
            _pets.Add(new Pet
            {
                Id = _nextId++,
                Name = viewModel.Name,
                Type = viewModel.Type,
                Birthday = viewModel.Birthday
            });
        }

        public Pet GetPetById(int id)
        {
            return _pets.FirstOrDefault(p => p.Id == id);
        }

        public List<Pet> GetPetsByType(string type)
        {
            if (string.IsNullOrEmpty(type)) return _pets;
            return _pets.Where(p => p.Type.ToLower() == type.ToLower()).ToList();
        }
    }
}