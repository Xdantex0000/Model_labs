using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Lab5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //#region First

            //Create c = new Create(2.0);
            //FirstProcess p1 = new FirstProcess(0.6, 1);
            //Process p2 = new Process(0.3, 1);
            //Process p3 = new Process(0.4, 1);
            //Process p4 = new Process(0.1, 2);

            //c.setNextElement(p1);
            //p1.setNextElement(p2);
            //p1.setNextElement(p3);
            //p1.setNextElement(p4);
            //p2.setNextElement(p1);
            //p3.setNextElement(p1);
            //p4.setNextElement(p1);

            //c.setName("CREATOR");
            //p1.setName("PROCESSOR");
            //p2.setName("PROCESSOR");
            //p3.setName("PROCESSOR");
            //p4.setName("PROCESSOR");

            //c.setDistribution("exp");
            //p1.setDistribution("exp");
            //p2.setDistribution("exp");
            //p3.setDistribution("exp");
            //p4.setDistribution("exp");

            //List<Element> list = new List<Element>();
            //list.Add(c);
            //list.Add(p1);
            //list.Add(p2);
            //list.Add(p3);
            //list.Add(p4);

            //Model model = new Model(list);
            //model.simulate(200000.0);

            //#endregion First

            #region Second

            // AutoModel(delayCreate, delayProcess, deviceCount, processesCount)
            // AutoModel.simulate(timeModeling, intervalOfDifficulty)

            double createDelay = 1.0, processDelay = 2.0;
            int size = 100;
            List<double> results = new List<double>();
            string result = "";
            List<long> time = new List<long>();

            for (int i = 0; i < size; i++)
            {
                var model = new AutoModel(createDelay, processDelay, 1, size * (i + 1));
                DateTime start = DateTime.Now;
                results.Add(model.simulate(20000.0));
                var dt = DateTime.Now - start;
                time.Add(Convert.ToInt32(dt.TotalMilliseconds));
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Time {i + 1}: {time[i]}");
                result += $"{time[i]} ";
            }
            result += "\n";
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Theoretical difficulty {i + 1}: {results[i]}");
                result += $"{results[i]} ";
            }
            result += "\n";

            File.WriteAllText("result.txt", result);

            // Next Model

            //for (int i = 0; i < size; i++)
            //{
            //    var model2 = new AutoModel(createDelay, processDelay, 5, size * (i + 1));
            //    DateTime start2 = DateTime.Now;
            //    results.Add(model2.simulate(20000.0));
            //    var dt2 = DateTime.Now - start2;
            //    time.Add(Convert.ToInt32(dt2.TotalMilliseconds));
            //}

            //for (int i = 0; i < size; i++)
            //{
            //    Console.WriteLine($"Time {i + 1}: {time[i]}");
            //    result += $"{time[i]} ";
            //}
            //result += "\n";
            //Console.WriteLine();

            //for (int i = 0; i < size; i++)
            //{
            //    Console.WriteLine($"Theoretical difficulty {i + 1}: {results[i]}");
            //    result += $"{results[i]} ";
            //}
            //result += "\n";

            //File.WriteAllText("result.txt", result);

            #endregion Second
        }
    }
}