using System;

namespace Lab2
{
    public class FunRand
    {
        public static double Exp(double time)
        {
            var rand = new Random();
            return -time * Math.Log(rand.NextDouble());
        }
    }
}