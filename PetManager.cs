using System;
using System.Collections.Generic;
using System.Linq;
using PetSimulator; // ← Pet sınıfını tanır
using System.IO;
using System.Text.Json;

namespace PetSimulator
{
    public class PetManager
    {
        public void SavePetsToFile(string filePath)
{
    var json = JsonSerializer.Serialize(pets);
    File.WriteAllText(filePath, json);
}

public void LoadPetsFromFile(string filePath)
{
    if (!File.Exists(filePath))
        return;

    var json = File.ReadAllText(filePath);
    var loadedPets = JsonSerializer.Deserialize<List<Pet>>(json);
    if (loadedPets != null)
        pets = loadedPets;
}

        private List<Pet> pets = new List<Pet>();


        public void AddPet(Pet pet)
        {
            pets.Add(pet);
            Console.WriteLine($"{pet.Name} eklendi.");
            pet.PetDied += (name) => Console.WriteLine($"⚠️ {name} öldü! Event tetiklendi.");

        }

        public void RemoveDeadPets()
        {
            var deadPets = pets.Where(p => !p.IsAlive).ToList();

            foreach (var pet in deadPets)
            {
                Console.WriteLine($"{pet.Name} öldü ve silindi.");
                pets.Remove(pet);
            }
        }

        public void UpdateAllPets()
        {
            foreach (var pet in pets)
            {
                pet.UpdateStats();
            }

            RemoveDeadPets();
        }

        public void PrintAllStats()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("Hiç evcil hayvanın yok.");
                return;
            }

            foreach (var pet in pets)
            {
                pet.PrintStats();
            }
        }

        public Pet? GetPetByName(string name)
        {
            return pets.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Pet> GetAllPets()
        {
            return pets;
        }
    }
}
