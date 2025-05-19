using System;
using System.Threading.Tasks;
using PetSimulator.Enums;
using PetSimulator; // ← Item sınıfı burada

namespace PetSimulator
{
    public class Game
    {
        private PetManager petManager = new PetManager();
        private bool isRunning = true;

        public async Task Start()
        {
            string savePath = "pets.json";
            petManager.LoadPetsFromFile(savePath);

            _ = Task.Run(UpdateStatsPeriodically);

            Console.WriteLine("🐾 Sanal Evcil Hayvan Simülatörüne Hoş Geldin! 🐾");

            while (isRunning)
            {
                ShowMenu();

                Console.Write("Seçiminizi girin: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreatePet();
                        break;
                    case "2":
                        petManager.PrintAllStats();
                        break;
                    case "3":
                        InteractWithPet();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    case "5":
                        await UseItemOnPet(); // 👈 Yeni özellik buraya eklendi!
                        break;
                    case "6":
    UpgradePetStats();
    break;
    
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                        
                }

                petManager.UpdateAllPets();
                await Task.Delay(1000); // 1 saniye bekletme (async)
            }

            Console.WriteLine("Oyun bitti. Güle güle!");
            petManager.SavePetsToFile(savePath);
    

        }

        private void ShowMenu()
        {
            Console.WriteLine("\n----- MENÜ -----");
            Console.WriteLine("Hazırlayan: Kerem Yaman - 225040070");
            Console.WriteLine("1. Yeni Pet Oluştur");
            Console.WriteLine("2. Pet Durumlarını Göster");
            Console.WriteLine("3. Pet ile Etkileşime Gir (Yemek/Uyu/Oyna)");
            Console.WriteLine("4. Çıkış");
            Console.WriteLine("5. Item Kullan"); // 👈 Yeni menü seçeneği
            Console.WriteLine("6. Pet Stat Sınırını Geliştir");

        }

        private void CreatePet()
        {
            Console.Write("Pet ismini gir: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("İsim boş olamaz.");
                return;
            }

            Console.WriteLine("Pet türünü seç: 0 = Cat, 1 = Dog, 2 = Bird, 3 = Rabbit");
            if (int.TryParse(Console.ReadLine(), out int typeValue) && Enum.IsDefined(typeof(PetType), typeValue))
            {
                PetType type = (PetType)typeValue;
                Pet pet = new Pet(name, type);
                petManager.AddPet(pet);
            }
            else
            {
                Console.WriteLine("Geçersiz tür.");
            }
        }

        private void InteractWithPet()
        {
            Console.Write("Pet ismini gir: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("İsim boş olamaz.");
                return;
            }

            var pet = petManager.GetPetByName(name);
            if (pet == null)
            {
                Console.WriteLine("Bu isimde bir pet yok.");
                return;
            }

            Console.WriteLine("Ne yapmak istiyorsun? 1 = Yemek, 2 = Uyut, 3 = Oyna");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    pet.Feed();
                    Console.WriteLine($"{pet.Name} beslendi.");
                    break;
                case "2":
                    pet.SleepRest();
                    Console.WriteLine($"{pet.Name} uyutuldu.");
                    break;
                case "3":
                    pet.Play();
                    Console.WriteLine($"{pet.Name} ile oynandı.");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }

       // Game.cs sonuna eklemen gereken bölüm:

        private async Task UseItemOnPet()
        {
            Console.Write("Item verilecek pet ismini girin: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) return;

            var pet = petManager.GetPetByName(name);
            if (pet == null)
            {
                Console.WriteLine("Pet bulunamadı.");
                return;
            }

            Console.WriteLine("Item türünü seç: 0 = Food, 1 = Toy, 2 = Pillow");
            if (int.TryParse(Console.ReadLine(), out int itemTypeValue) &&
                Enum.IsDefined(typeof(ItemType), itemTypeValue))
            {
                ItemType type = (ItemType)itemTypeValue;
                string itemName = type.ToString();
                Item item = new Item(itemName, type, 10); // etki miktarı 10

                await item.UseAsync(pet); // ✅ await kullanıldı
            }
            else
            {
                Console.WriteLine("Geçersiz item türü.");
            }
        } // 👈 BU parantez Game class içindeki UseItemOnPet() metodunun sonu
private async Task UpdateStatsPeriodically()
{
    while (isRunning)
    {
        petManager.UpdateAllPets();
        await Task.Delay(5000); // her 5 saniyede bir statlar azalır
    }
}
private void UpgradePetStats()
{
    Console.Write("Stat sınırı geliştirilecek pet'in adını gir: ");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name)) return;

    var pet = petManager.GetPetByName(name);
    if (pet == null)
    {
        Console.WriteLine("Pet bulunamadı.");
        return;
    }

    Console.WriteLine("Hangi stat sınırını artırmak istiyorsun?");
    Console.WriteLine("1. Hunger");
    Console.WriteLine("2. Sleep");
    Console.WriteLine("3. Fun");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            pet.MaxHunger += 10;
            Console.WriteLine($"{pet.Name} için maksimum açlık seviyesi {pet.MaxHunger} oldu.");
            break;
        case "2":
            pet.MaxSleep += 10;
            Console.WriteLine($"{pet.Name} için maksimum uyku seviyesi {pet.MaxSleep} oldu.");
            break;
        case "3":
            pet.MaxFun += 10;
            Console.WriteLine($"{pet.Name} için maksimum eğlence seviyesi {pet.MaxFun} oldu.");
            break;
        default:
            Console.WriteLine("Geçersiz seçim.");
            break;
    }
}

    } // 👈 BU parantez Game class'ın SONU
} // 👈 BU da namespace PetSimulator'ın SONU

