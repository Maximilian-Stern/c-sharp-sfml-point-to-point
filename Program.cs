using System;
using System.Collections;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using GameData;

namespace drivepath
{
    class Program
    {
        private static void Main(string[] args)
        {
            var game = new Game();
            game.Run();
        }
    }
}
