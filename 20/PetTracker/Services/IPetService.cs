using PetTracker.ViewModels;
using PetTracker.Models;
using System.Collections.Generic;

namespace PetTracker.Services
{
    public interface IPetService
    {
        // Получить всех питомцев
        List<Pet> GetAllPets();

        // Добавить питомца (из ViewModel)
        void AddPet(PetViewModel viewModel);

        // Получить питомца по ID
        Pet GetPetById(int id);

        // Получить питомцев по типу (например, только котов)
        List<Pet> GetPetsByType(string type);
    }
}