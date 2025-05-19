using System;
using System.Text.Json.Serialization;
using PetSimulator.Enums;

namespace PetSimulator
{
    public class Pet
    {public event Action<string>? PetDied;

        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetType Type { get; set; }

        public int Hunger { get; set; } = 50;
        public int MaxHunger { get; set; } = 100;
        public int Sleep { get; set; } = 50;
        public int MaxSleep { get; set; } = 100;
        public int Fun { get; set; } = 50;
        public int MaxFun { get; set; } = 100;

        [JsonIgnore]
        public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

        // yapıcı
        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
        }

       public void UpdateStats()
{
    Hunger = Math.Max(Hunger - 1, 0);
    Sleep = Math.Max(Sleep - 1, 0);
    Fun = Math.Max(Fun - 1, 0);

    if (!IsAlive)
    {
        PetDied?.Invoke(Name);
    }
}


        public void Feed() => Hunger = Math.Min(Hunger + 10, MaxHunger);
        public void SleepRest() => Sleep = Math.Min(Sleep + 10, MaxSleep);
        public void Play() => Fun = Math.Min(Fun + 10, MaxFun);

        public void PrintStats()
        {
            Console.WriteLine($"{Name} ({Type}) - Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}");
        }
    }
}
