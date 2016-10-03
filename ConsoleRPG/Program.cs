using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    class Program {
        static void Main(string[] args)
        {
            // Init message manager singleton
            MessageManager.Init();

            // Set up necessary messages
            MessageManager.instance.AddMsg("test", "this is a test message.");
            MessageManager.instance.PrintRandomMsg("test");

            Game oGame = new Game();
            oGame.Run();
        }
    }
}
