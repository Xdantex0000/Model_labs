using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Model model = new Model(15, 10, 2);
            model.simulate(1000);
        }
    }
}