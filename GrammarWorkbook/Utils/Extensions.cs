using System;

namespace GrammarWorkbook.Utils
{
    public static class Extensions
    {
        public static T[] Shuffle<T>(this T[] array)
        {
            int rndNo;
            var shuffledArray = new T[array.Length];
            Random rnd = new Random();
            for (int i = array.Length; i >= 1; i--)
            {
                rndNo = rnd.Next(1, i+1) - 1;
                shuffledArray[i - 1] = array[rndNo];
                array[rndNo] = array[i - 1];
            }

            return shuffledArray;
        }
    }
}