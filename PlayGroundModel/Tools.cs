using System;
using System.Collections.Generic;
using System.Threading;

namespace PlayGroundModel
{
    internal static class Tools
    {
        private static Random _rand;

        private static List<string> _names;

        static Tools()
        {
            _rand = new Random();
            _names = new List<string>() { "Михаил", "Ольга", "Сергей", "Екатерина", "Петр" };
        }
        
        /// <summary>
        /// Получаем случайное имя из списка имен.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomName() => _names[_rand.Next(0, 5)];

        /// <summary>
        /// Получаем случайное двузначное число от 10 до 99.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomValue()
        { 
            var num = _rand.Next(10, 100);
            //Thread.Sleep(100);
            return num;
        }
    }
}
