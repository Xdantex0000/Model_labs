using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    internal class AutoModel
    {
        private int Event;
        private int countOfProcess;
        private double createDelay;
        private double processDelay;
        private List<double> difficulty = new List<double>();
        private readonly List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private List<double> eventIntensivity;

        public AutoModel(double createDelay, double processDelay, int deviceNumber, int numberOfProcess)
        {
            this.createDelay = createDelay;
            this.processDelay = processDelay;
            countOfProcess = numberOfProcess;
            Element.disposed = 0;
            tnext = 0.0;
            Event = 0;
            tcurr = tnext;
            eventIntensivity = new List<double>();

            list.Add(new Create(createDelay));
            for (int i = 0; i < numberOfProcess; i++)
            {
                var proc = new Process(processDelay, deviceNumber);
                var lastProc = list[i];

                proc.disposable = true;
                proc.setDistribution("exp");
                lastProc.setNextElement(proc);
                if (typeof(Process) == lastProc.GetType())
                {
                    var lp = (Process)lastProc;
                    lp.disposable = false;
                }
                list.Add(proc);
            }
        }

        public double simulate(double time)
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
                eventIntensivity.Add(tnext - tcurr);
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

            //printResult(time);

            return ((1 / createDelay) + (countOfProcess / processDelay)) * tcurr * 4;
        }

        public void printInfo()
        {
            foreach (var e in list) e.printInfo();
        }

        public void printResult(double time)
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

            Console.WriteLine($"\nIntensivity: {avgEventIntensivity()}");
            Console.WriteLine($"\nDisposed: {Element.disposed}");
        }

        public double avgEventIntensivity()
        {
            var result = 0.0;

            foreach (var x in eventIntensivity)
                result += x;

            return result / eventIntensivity.Count;
        }
    }
}