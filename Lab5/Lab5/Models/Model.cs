using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace Lab5
{
    public class Model
    {
        private int Event;
        private readonly List<Element> list = new List<Element>();
        private double tnext, tcurr;

        public Model(List<Element> elements)
        {
            Element.disposed = 0;
            list = elements;
            tnext = 0.0;
            Event = 0;
            tcurr = tnext;
        }

        public void simulate(double time)
        {
            while (tcurr < time)
            {
                // Set tnext to max value of double
                tnext = double.MaxValue;
                // Choose the action (create or process)
                for (int i = 0; i < list.Count; i++)
                    if (list[i].getTnext() < tnext)
                    {
                        tnext = list[i].getTnext();
                        Event = i;
                    }

                //Console.WriteLine($"\nIt's time for Event in {list[Event].getName()}, time = {tnext}");
                // Count statistics
                foreach (var e in list) e.doStatistics(tnext - tcurr);
                tcurr = tnext;
                // Set all tcurr in element to new values
                foreach (var e in list) e.setTcurr(tcurr);
                // Call action
                list[Event].outAct();
                foreach (var e in list)
                    if (e.getTnext() == tcurr && e.getTnext() != 0)
                        e.outAct();
                //printInfo();
            }

            printResult();
        }

        public void printInfo()
        {
            foreach (var e in list) e.printInfo();
        }

        public void printResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            foreach (var e in list)
            {
                e.printResult();

                if (typeof(Process) == e.GetType())
                {
                    var p = (Process)e;
                    Console.WriteLine($"Mean length of queue = {p.getMeanQueue() / tcurr}\nfailure probability  = {p.getFailure() / (double)p.getQuantity()}\nMean load = {p.MeanLoad / tcurr}");
                    p.printInfo();
                }
                else if (typeof(FirstProcess) == e.GetType())
                {
                    var p = (FirstProcess)e;
                    Console.WriteLine($"Mean length of queue = {p.getMeanQueue() / tcurr}\nfailure probability  = {p.getFailure() / (double)p.getQuantity()}\nMean load = {p.MeanLoad / tcurr}");
                    p.printInfo();
                }
                else if (typeof(Create) == e.GetType())
                {
                    var c = (Create)e;
                    c.printInfo();
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nDisposed: {Element.disposed}");
        }
    }
}