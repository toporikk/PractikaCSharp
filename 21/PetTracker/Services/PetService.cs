using PetTracker.Models;
using PetTracker.ViewModels;
using PetTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetTracker.Services
{
    public class PetService : IPetService
    {
        private readonly AppDbContext _context;

        public PetService(AppDbContext context)
        {
            _context = context;
        }

        public List<Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }

        public void AddPet(PetViewModel viewModel)
        {
            var pet = new Pet
            {
                Name = viewModel.Name,
                Type = viewModel.Type,
                Birthday = viewModel.Birthday,
                Notes = viewModel.Notes
            };
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        public Pet GetPetById(int id)
        {
            return _context.Pets.Find(id);
        }

        public List<Pet> GetPetsByType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return _context.Pets.ToList();

            return _context.Pets.Where(p => p.Type.ToLower() == type.ToLower()).ToList();
        }

        public void UpdatePet(Pet pet)
        {
            _context.Pets.Update(pet);
            _context.SaveChanges();
        }

        public void DeletePet(int id)
        {
            var pet = _context.Pets.Find(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}