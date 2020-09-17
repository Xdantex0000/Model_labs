using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
           Model model = new Model(2,1, 4);
                   model.simulate(1000);

        }
    }
}