using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class Program
    {
        private static List<TableData> tableData = new List<TableData>();

        private static void Main(string[] args)
        {
            //#region First

            //Create c = new Create(0.5);
            //Cashbox p1 = new Cashbox(0.3, 1);
            //Cashbox p2 = new Cashbox(0.3, 1);

            //p1.disposable = true;
            //p2.disposable = true;

            //c.setNextElement(p1);
            //p2.setNextElement(p1);
            //p1.setNextElement(p2);

            //p1.setMaxqueue(3);
            //p2.setMaxqueue(3);

            //c.setName("CREATOR");
            //p1.setName("CASE1");
            //p2.setName("CASE2");

            //c.setDistribution("exp");
            //p1.setDistribution("exp");
            //p2.setDistribution("exp");

            //List<Element> list = new List<Element>();
            //list.Add(c);
            //list.Add(p1);
            //list.Add(p2);

            //Model model = new Model(list);
            //model.simulateFirst(1000.0);

            //#endregion First

            #region Second

            Create c2 = new Create(15.0);
            Cabinet cabinet = new Cabinet(1, 2);
            Follower follower = new Follower(3); // Constructor has delay (3-8)
            RoadToLab rtl = new RoadToLab();
            Registration registration = new Registration(4.5, 2, 3);
            Lab lab = new Lab(4, 2, 2);

            c2.setNextElement(cabinet);
            cabinet.setNextElement(follower);
            cabinet.setNextElement(rtl);
            rtl.setNextElement(registration);
            registration.setNextElement(lab);
            lab.setNextElement(cabinet);

            c2.setName("CREATOR");
            cabinet.setName("CABINET");
            follower.setName("FOLLOWER");
            rtl.setName("ROAD_TO_LAB");
            registration.setName("REGISTRATION");
            lab.setName("LAB");

            c2.setDistribution("exp");
            cabinet.setDistribution("exp");
            follower.setDistribution("unif");
            rtl.setDistribution("unif");
            registration.setDistribution("erlang");
            lab.setDistribution("erlang");

            List<Element> list2 = new List<Element>();
            list2.Add(c2);
            list2.Add(cabinet);
            list2.Add(follower);
            list2.Add(rtl);
            list2.Add(registration);
            list2.Add(lab);

            Model model2 = new Model(list2);
            model2.simulateSecond(1000.0);

            #endregion Second
        }

        private static void addData(List<Element> data, Model model)
        {
            var tdata = new TableData();
            tdata.createdQuantity = data[0].getQuantity();
            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];
                tdata.failedQuantity.Add(proc.getFailure());
            }
            tdata.failProbility = model.getRefusalProb();
            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];
                tdata.maxQueue.Add(model.maxQueueValue(proc));
            }
            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];
                tdata.avgQueue.Add(model.avgQueueValue(proc));
            }
            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];
                var dt = model.maxQueuePressure(proc);
                var list = new List<double>();

                for (int j = 0; j < dt.Length; j++)
                {
                    list.Add(dt[j]);
                }
                tdata.maxPressure.Add(list);
            }
            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];
                var dt = model.avgQueuePressure(proc);
                var list = new List<double>();

                for (int j = 0; j < dt.Count; j++)
                {
                    list.Add(dt[j]);
                }
                tdata.avgPressure.Add(list);
            }
            tdata.createDelay = data[0].StartDelay;

            for (int i = 1; i < data.Count; i++)
            {
                var proc = (Process)data[i];

                var list = new List<double>();

                for (int j = 0; j < proc.Devices.Count; j++)
                {
                    list.Add(proc.Devices.Count);
                }
                tdata.devicesCount.Add(list);
            }
            tableData.Add(tdata);
        }

        private static void printTable()
        {
            for (int i = 0; i < tableData.Count; i++)
            {
                if (i >= 0 && i < 3)
                    Console.WriteLine("Num\tCRTD\tNS1\tNS2\tNS3\tNS4\tRP\tMXQ1\tMXQ2\tMXQ3\tMXQ4\tAVQ1\tAVQ2\tAVQ3\tAVQ4\tMXP1\tAVP1\tDEL1\tDEV");
                if (i >= 3 && i < 6)
                    Console.WriteLine("Num\tCRTD\tNS1\tNS2\tNS3\tNS4\tRP\tMXQ1\tMXQ2\tMXQ3\tMXQ4\tAVQ1\tAVQ2\tAVQ3\tAVQ4\tMXP1\tMXP2\tAVP1\tAVP2\tDEL1\tDEV");
                if (i >= 6 && i < 9)
                    Console.WriteLine("Num\tCRTD\tNS1\tNS2\tNS3\tNS4\tRP\tMXQ1\tMXQ2\tMXQ3\tMXQ4\tAVQ1\tAVQ2\tAVQ3\tAVQ4\tMXP1\tMXP2\tMXP3\tAVP1\tAVP2\tAVP3\tDEL1\tDEV");
                var data = tableData[i];
                Console.Write($"{i + 1}\t{tableData[i].createdQuantity}\t");

                for (int j = 0; j < data.failedQuantity.Count; j++)
                    Console.Write($"{data.failedQuantity[j]}\t");

                Console.Write($"{Math.Round(data.failProbility, 2)}\t");

                for (int j = 0; j < data.maxQueue.Count; j++)
                    Console.Write($"{Math.Round(data.maxQueue[j], 2)}\t");

                for (int j = 0; j < data.avgQueue.Count; j++)
                    Console.Write($"{Math.Round(data.avgQueue[j], 2)}\t");

                for (int z = 0; z < data.maxPressure[0].Count; z++)
                    Console.Write($"{Math.Round(data.maxPressure[0][z], 2)}\t");

                for (int z = 0; z < data.avgPressure[0].Count; z++)
                    Console.Write($"{Math.Round(data.avgPressure[0][z], 2)}\t");

                Console.Write($"{data.createDelay}\t");

                Console.Write($"{Math.Round(data.devicesCount[0][0], 2)}\t");

                Console.WriteLine("\n");
            }
        }
    }
}