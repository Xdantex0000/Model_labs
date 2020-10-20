using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace Lab4
{
    public class Model
    {
        private int Event;
        private readonly List<Element> list = new List<Element>();
        private double tnext, tcurr;
        private double time;

        public Model(List<Element> elements)
        {
            Element.disposed = 0;
            list = elements;
            tnext = 0.0;
            Event = 0;
            tcurr = tnext;
        }

        // здійснення імітації на інтервалі часу time
        public void simulateFirst(double time)
        {
            this.time = time;
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

                // Console.WriteLine($"\nIt's time for Event in {list[Event].getName()}, time = {tnext}");
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
                // printInfo();
            }

            printResultFirst();
        }

        public void simulateSecond(double time)
        {
            this.time = time;
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

            printResultSecond();
        }

        public void printInfo()
        {
            foreach (var e in list) e.printInfo();
        }

        public void printResultFirst()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            foreach (var e in list)
            {
                e.printResult();
                if (typeof(Cashbox) == e.GetType())
                {
                    var proc = (Cashbox)e;
                    Console.WriteLine("mean length of queue = " +
                                      proc.getMeanQueue() / tcurr
                                      + "\nfailure probability  = " +
                                      proc.getFailure() / (double)proc.getQuantity());
                    Console.WriteLine($"Failure: {proc.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < proc.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + proc.Devices[i].getQuantity());
                    Console.WriteLine($"Maximal value of queue = {maxQueueValue((Process)e)}");
                    for (int i = 0; i < maxQueuePressure((Process)e).Count(); i++)
                    {
                        Console.WriteLine($"On device: {i + 1} maximal pressure = {maxQueuePressure((Process)e)[i]}");
                    }
                    for (int i = 0; i < avgQueuePressure((Process)e).Count(); i++)
                    {
                        Console.WriteLine($"On device: {i + 1} average pressure = {avgQueuePressure((Process)e)[i]}");
                    }
                }
                else if (typeof(Create) == e.GetType())
                {
                    e.printInfo();
                }

                Console.WriteLine();
            }
            Console.WriteLine($"Disposed tasks: {Element.disposed}");
            Console.WriteLine($"Average out from window time: {getAvgWindow()}");
            Console.WriteLine($"Average time in bank: {getAvgInBank()}");
            Console.WriteLine($"Fail percentage: {failProbability() * 100}");
            Console.WriteLine($"Line changed: {Cashbox.LineChange}");
        }

        public void printResultSecond()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            foreach (var e in list)
            {
                e.printResult();
                if (typeof(Process) == e.GetType())
                {
                    var p = (Process)e;
                    Console.WriteLine("mean length of queue = " +
                                      p.getMeanQueue() / tcurr
                                      + "\nfailure probability  = " +
                                      p.getFailure() / (double)p.getQuantity());
                }

                if (e.getName() == "CABINET")
                {
                    var cab = (Cabinet)e;
                    Console.WriteLine($"Failure: {cab.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < cab.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + cab.Devices[i].getQuantity());
                }
                if (e.getName() == "FOLLOWER")
                {
                    var cab = (Follower)e;
                    Console.WriteLine($"Failure: {cab.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < cab.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + cab.Devices[i].getQuantity());
                }
                if (e.getName() == "ROAD_TO_LAB")
                {
                    var rtl = (RoadToLab)e;
                    Console.WriteLine($"Failure: {rtl.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                }
                if (e.getName() == "REGISTRATION")
                {
                    var reg = (Registration)e;
                    Console.WriteLine($"Failure: {reg.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < reg.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + reg.Devices[i].getQuantity());
                }
                if (e.getName() == "LAB")
                {
                    var lab = (Lab)e;
                    Console.WriteLine($"Failure: {lab.getFailure()}");

                    //Console.WriteLine($"Refusal probability = {getRefusalProb()}");
                    for (int i = 0; i < lab.Devices.Count(); i++)
                        Console.WriteLine(i + 1 + ". Device has quantity: " + lab.Devices[i].getQuantity());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"Time in system, first type of pacient: {avg1()}");
            Console.WriteLine();
            Console.WriteLine($"Time in system, second and third type of pacient: {avg2()}");
            Console.WriteLine();
            Console.WriteLine($"Pacients arrive Lab with interval: {avg3()}");
            Console.WriteLine();
            Console.WriteLine($"Disposed from Cabinet: {Cabinet.disposedFromCabinet}");
            Console.WriteLine($"Disposed from Followers: {Cabinet.disposedFromCabinet}");
            Console.WriteLine($"Disposed from Road to lab: {RoadToLab.disposedFromRoadToLab}");
            Console.WriteLine($"Disposed from Registration: {Registration.disposedFromRegistration}");
            Console.WriteLine($"Disposed from Lab: {Lab.disposedFromLab}");
            Console.WriteLine($"\nDisposed: {Element.disposed}");
        }

        public double avg3()
        {
            double result = 0.0;

            foreach (var x in Lab.avgInInterval())
                result += x;

            return result / Lab.avgInInterval().Count;
        }

        public double avg2()
        {
            double result = 0.0;

            foreach (var x in Lab.ResultLabTime)
                result += x;

            return result / Lab.ResultLabTime.Count;
        }

        public double avg1()
        {
            double result = 0.0;

            foreach (var x in Follower.ResultFollowerTime)
                result += x;

            return result / Follower.ResultFollowerTime.Count;
        }

        public List<Element> getList()
        {
            return list;
        }

        public double failProbability()
        {
            double result = 0.0;

            foreach (var e in list)
            {
                if (e.getName() != "CREATOR")
                {
                    var proc = (Process)e;
                    result += proc.getFailure();
                }
            }
            return result / list[0].getQuantity();
        }

        public double getAvgInBank()
        {
            double result = 0.0;

            foreach (var x in Cashbox.InBank)
            {
                result += x;
            }
            return result / Cashbox.InBank.Count;
        }

        public double getAvgWindow()
        {
            double result = 0.0;

            foreach (var x in Cashbox.OutWindow)
            {
                result += x;
            }
            return result / Cashbox.OutWindow.Count;
        }

        // 2.1 ймовірність відмови
        public double getRefusalProb()
        {
            var creatorCount = 0;

            foreach (var e in list)
            {
                if (e.getName() == "CREATOR")
                {
                    creatorCount = e.getQuantity();
                    break;
                }
            }

            return Convert.ToDouble(creatorCount - Element.disposed) / Convert.ToDouble(creatorCount);
        }

        // 2.2 Максимальне значення черги
        public double maxQueueValue(Process process)
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

        // 2.4 Максимальне значення завантаження пристрою
        public double[] maxQueuePressure(Process process)
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
        public List<double> avgQueuePressure(Process process)
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