using System.Threading.Tasks;

namespace PetSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game();
            await game.Start();
        }
    }
}
