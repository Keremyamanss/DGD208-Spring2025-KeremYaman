using System;
using System.Threading.Tasks;
using PetSimulator.Enums;

namespace PetSimulator
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int EffectAmount { get; set; }

        public Item(string name, ItemType type, int effectAmount)
        {
            Name = name;
            Type = type;
            EffectAmount = effectAmount;
        }

        public async Task UseAsync(Pet pet)
        {
            Console.WriteLine($"{Name} uygulanıyor...");
            await Task.Delay(2000);

            switch (Type)
            {
                case ItemType.Food:
                    pet.Feed();
                    Console.WriteLine($"{pet.Name} beslendi.");
                    break;
                case ItemType.Toy:
                    pet.Play();
                    Console.WriteLine($"{pet.Name} eğlendi.");
                    break;
                case ItemType.Pillow:
                    pet.SleepRest();
                    Console.WriteLine($"{pet.Name} uyudu.");
                    break;
            }
        }
    }
}
