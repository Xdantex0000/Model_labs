using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace Lab3
{
    public class Model
    {
        private int Event;
        private readonly List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private double time;

        public Model(List<Element> elements)
        {
            list = elements;
            tnext = 0.0;
            Event = 0;
            tcurr = tnext;
        }

        // здійснення імітації на інтервалі часу time
        public void simulate(double time)
        {
            this.time = time;
            while (tcurr < time)
            {
                // Set tnext to max value of double
                tnext = double.MaxValue;
                // Choose the action (create or process)
                foreach (var e in list)
                    if (e.getTnext() < tnext)
                    {
                        tnext = e.getTnext();
                        Event = e.getId();
                    }

                Console.WriteLine($"\nIt's time for Event in {list[Event].getName()}, time = {tnext}");
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
                printInfo();
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
                ;
                if (typeof(Process) == e.GetType())
                {
                    var p = (Process)e;
                    Console.WriteLine("mean length of queue = " +
                                      p.getMeanQueue() / tcurr
                                      + "\nfailure probability  = " +
                                      p.getFailure() / (double)p.getQuantity());
                }

                if (e.getName() != "CREATOR")
                {
                    var proc = (Process)e;
                    Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < proc.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + proc.Devices[i].getQuantity());
                    Console.WriteLine($"Maximal value of queue = {maxQueueValue((Process)e)}");
                    Console.WriteLine($"Average value of queue = {avgQueueValue((Process)e)}");
                    for (int i = 0; i < maxQueuePressure((Process)e).Count(); i++)
                    {
                        Console.WriteLine($"On device: {i + 1} maximal pressure = {maxQueuePressure((Process)e)[i]}");
                    }
                    for (int i = 0; i < avgQueuePressure((Process)e).Count(); i++)
                    {
                        Console.WriteLine($"On device: {i + 1} average pressure = {avgQueuePressure((Process)e)[i]}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Disposed tasks: {Element.disposed}");
        }

        // 2.1 ймовірність відмови
        private double getRefusalProb()
        {
            var creatorCount = 0;

            foreach (var e in list)
            {
                if (e.getName() == "CREATOR")
                    creatorCount += e.getQuantity();
            }

            return Convert.ToDouble(creatorCount - Element.disposed) / Convert.ToDouble(creatorCount);
        }

        // 2.2 Максимальне значення черги
        private double maxQueueValue(Process process)
        {
            if (process.QList.Count() == 0)
            {
                return 0.0;
            }
            var maxQList = double.MinValue;

            for (int i = 0; i < process.QList.Count; i++)
            {
                if (process.QList[i] > maxQList)
                    maxQList = process.QList[i];
            }

            return maxQList;
        }

        // 2.3 Середнє значення черги
        private double avgQueueValue(Process process)
        {
            if (process.QList.Count() == 0)
            {
                return 0.0;
            }

            var avgDict = new Dictionary<int, double>();
            double result = 0;

            for (int i = 0; i < process.QInterval.Count; i++)
            {
                if (!avgDict.ContainsKey(process.QList[i]))
                    avgDict.Add(process.QList[i], process.QInterval[i]);
                else
                    avgDict[process.QList[i]] += process.QInterval[i];
            }

            for (int i = 0; i <= avgDict.Keys.Max(); i++)
            {
                result += i * avgDict[i] / time;
            }

            return result;
        }

        // 2.4 Максимальне значення завантаження пристрою
        private double[] maxQueuePressure(Process process)
        {
            var maxRIntervals = new double[process.Devices.Count()];

            for (int i = 0; i < process.Devices.Count(); i++)
            {
                for (int j = 0; j < process.RList[i].Count(); j++)
                    if (maxRIntervals[i] < (process.RList[i][j] * process.RInterval[i][j]))
                        maxRIntervals[i] = process.RList[i][j] * process.RInterval[i][j];
            }

            return maxRIntervals;
        }

        // 2.5 Середнє значення завантаження пристрою
        private List<double> avgQueuePressure(Process process)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < process.RList.Count(); i++)
            {
                var chisl = 0.0;

                for (int j = 0; j < process.RList[i].Count(); j++)
                {
                    chisl += process.RList[i][j] * process.RInterval[i][j];
                }

                result.Add(chisl / time);
            }

            return result;
        }
    }
}