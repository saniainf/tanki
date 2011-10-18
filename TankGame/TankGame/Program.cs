using System;

namespace TankGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// точка входа
        /// </summary>
        static void Main(string[] args)
        {
            using (BaseGame game = new BaseGame())
            {
                game.Run();
            }
        }
    }
#endif
}

