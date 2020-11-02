using System;

namespace Lab7
{
    internal class FunRand
    {
        /**
     * Generates a random value according to an exponential distribution
     *
     * @param timeMean mean value
     * @return a random value according to an exponential distribution
     */

        public static double Exp(double timeMean)
        {
            Random rand = new Random();
            double a = 0;
            while (a == 0)
            {
                a = rand.NextDouble();
            }
            a = -timeMean * Math.Log(a);

            return a;
        }

        /**
     * Generates a random value according to a uniform distribution
     *
     * @param timeMin
     * @param timeMax
     * @return a random value according to a uniform distribution
     */

        public static double Unif(double timeMin, double timeMax)
        {
            Random rand = new Random();
            double a = 0;
            while (a == 0)
            {
                a = rand.NextDouble();
            }
            a = timeMin + a * (timeMax - timeMin);

            return a;
        }

        /**
     * Generates a random value according to a normal (Gauss) distribution
     *
     * @param timeMean
     * @param timeDeviation
     * @return a random value according to a normal (Gauss) distribution
     */

        public static double Norm(double timeMean, double timeDeviation)
        {
            Random rand = new Random();
            double a;
            a = timeMean + timeDeviation * rand.NextDouble();

            return a;
        }

        public static double Erlang(double timeMean, int k)
        {
            Random rand = new Random();
            double sum = 0.0;

            for (int i = 0; i < k; i++)
                sum += Math.Log(rand.NextDouble());

            return sum * (-timeMean) / (double)k;
        }
    }
}