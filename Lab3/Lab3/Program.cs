using System;
using System.Collections.Generic;

namespace Lab3
{
    internal class Program
    {
        private static List<TableData> tableData = new List<TableData>();

        private static void Main(string[] args)
        {
            #region First

            Create c = new Create(2.0);
            Process p1 = new Process(1.0, 1);
            Process p2 = new Process(1.0, 1);
            Process p3 = new Process(1.0, 1);
            Process p4 = new Process(1.0, 1);

            p2.disposable = true;
            p4.disposable = true;

            c.setNextElement(p1);

            p1.setNextElement(p2);
            p1.setNextElement(p3);

            p2.setNextElement(p1);
            p2.setNextElement(p3);

            p3.setNextElement(p1);
            p3.setNextElement(p2);
            p3.setNextElement(p4);

            p4.setNextElement(p1);
            p4.setNextElement(p3);

            p1.setMaxqueue(5);
            p2.setMaxqueue(5);
            p3.setMaxqueue(5);
            p4.setMaxqueue(5);

            c.setName("CREATOR");
            p1.setName("PROCESSOR1");
            p2.setName("PROCESSOR2");
            p3.setName("PROCESSOR3");
            p4.setName("PROCESSOR4");

            c.setDistribution("exp");
            p1.setDistribution("exp");
            p2.setDistribution("exp");
            p3.setDistribution("exp");
            p4.setDistribution("exp");

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            Model model = new Model(list);
            model.simulate(100.0);

            addData(model.getList(), model);

            #endregion First

            //#region Second

            //Create c2 = new Create(10.0);
            //Process p12 = new Process(1.0, 1);
            //Process p22 = new Process(1.0, 1);
            //Process p32 = new Process(1.0, 1);
            //Process p42 = new Process(1.0, 1);

            //p22.disposable = true;
            //p42.disposable = true;

            //c2.setNextElement(p12);

            //p12.setNextElement(p22);
            //p12.setNextElement(p32);

            //p22.setNextElement(p12);
            //p22.setNextElement(p32);

            //p32.setNextElement(p12);
            //p32.setNextElement(p22);
            //p32.setNextElement(p42);

            //p42.setNextElement(p12);
            //p42.setNextElement(p32);

            //p12.setMaxqueue(5);
            //p22.setMaxqueue(5);
            //p32.setMaxqueue(5);
            //p42.setMaxqueue(5);

            //c2.setName("CREATOR");
            //p12.setName("PROCESSOR1");
            //p22.setName("PROCESSOR2");
            //p32.setName("PROCESSOR3");
            //p42.setName("PROCESSOR4");

            //c2.setDistribution("exp");
            //p12.setDistribution("exp");
            //p22.setDistribution("exp");
            //p32.setDistribution("exp");
            //p42.setDistribution("exp");

            //List<Element> list2 = new List<Element>();
            //list2.Add(c2);
            //list2.Add(p12);
            //list2.Add(p22);
            //list2.Add(p32);
            //list2.Add(p42);
            //Model model2 = new Model(list2);
            //model2.simulate(1000.0);

            //addData(model2.getList(), model2);

            //#endregion Second

            //#region Third

            //Create c3 = new Create(2.0);
            //Process p13 = new Process(10.0, 1);
            //Process p23 = new Process(10.0, 1);
            //Process p33 = new Process(10.0, 1);
            //Process p43 = new Process(10.0, 1);

            //p23.disposable = true;
            //p43.disposable = true;

            //c3.setNextElement(p13);

            //p13.setNextElement(p23);
            //p13.setNextElement(p33);

            //p23.setNextElement(p13);
            //p23.setNextElement(p33);

            //p33.setNextElement(p13);
            //p33.setNextElement(p23);
            //p33.setNextElement(p43);

            //p43.setNextElement(p13);
            //p43.setNextElement(p33);

            //p13.setMaxqueue(5);
            //p23.setMaxqueue(5);
            //p33.setMaxqueue(5);
            //p43.setMaxqueue(5);

            //c3.setName("CREATOR");
            //p13.setName("PROCESSOR1");
            //p23.setName("PROCESSOR2");
            //p33.setName("PROCESSOR3");
            //p43.setName("PROCESSOR4");

            //c3.setDistribution("exp");
            //p13.setDistribution("exp");
            //p23.setDistribution("exp");
            //p33.setDistribution("exp");
            //p43.setDistribution("exp");

            //List<Element> list3 = new List<Element>();
            //list3.Add(c3);
            //list3.Add(p13);
            //list3.Add(p23);
            //list3.Add(p33);
            //list3.Add(p43);
            //Model model3 = new Model(list3);
            //model3.simulate(1000.0);

            //addData(model3.getList(), model3);

            //#endregion Third

            //#region Fourth

            //Create c4 = new Create(2.0);
            //Process p14 = new Process(1.0, 2);
            //Process p24 = new Process(1.0, 2);
            //Process p34 = new Process(1.0, 2);
            //Process p44 = new Process(1.0, 2);

            //p24.disposable = true;
            //p44.disposable = true;

            //c4.setNextElement(p14);

            //p14.setNextElement(p24);
            //p14.setNextElement(p34);

            //p24.setNextElement(p14);
            //p24.setNextElement(p34);

            //p34.setNextElement(p14);
            //p34.setNextElement(p24);
            //p34.setNextElement(p44);

            //p44.setNextElement(p14);
            //p44.setNextElement(p34);

            //p14.setMaxqueue(5);
            //p24.setMaxqueue(5);
            //p34.setMaxqueue(5);
            //p44.setMaxqueue(5);

            //c4.setName("CREATOR");
            //p14.setName("PROCESSOR1");
            //p24.setName("PROCESSOR2");
            //p34.setName("PROCESSOR3");
            //p44.setName("PROCESSOR4");

            //c4.setDistribution("exp");
            //p14.setDistribution("exp");
            //p24.setDistribution("exp");
            //p34.setDistribution("exp");
            //p44.setDistribution("exp");

            //List<Element> list4 = new List<Element>();
            //list4.Add(c4);
            //list4.Add(p14);
            //list4.Add(p24);
            //list4.Add(p34);
            //list4.Add(p44);
            //Model model4 = new Model(list4);
            //model4.simulate(1000.0);

            //addData(model4.getList(), model4);

            //#endregion Fourth

            //#region Fifth

            //Create c5 = new Create(10.0);
            //Process p15 = new Process(1.0, 2);
            //Process p25 = new Process(1.0, 2);
            //Process p35 = new Process(1.0, 2);
            //Process p45 = new Process(1.0, 2);

            //p25.disposable = true;
            //p45.disposable = true;

            //c5.setNextElement(p15);

            //p15.setNextElement(p25);
            //p15.setNextElement(p35);

            //p25.setNextElement(p15);
            //p25.setNextElement(p35);

            //p35.setNextElement(p15);
            //p35.setNextElement(p25);
            //p35.setNextElement(p45);

            //p45.setNextElement(p15);
            //p45.setNextElement(p35);

            //p15.setMaxqueue(5);
            //p25.setMaxqueue(5);
            //p35.setMaxqueue(5);
            //p45.setMaxqueue(5);

            //c5.setName("CREATOR");
            //p15.setName("PROCESSOR1");
            //p25.setName("PROCESSOR2");
            //p35.setName("PROCESSOR3");
            //p45.setName("PROCESSOR4");

            //c5.setDistribution("exp");
            //p15.setDistribution("exp");
            //p25.setDistribution("exp");
            //p35.setDistribution("exp");
            //p45.setDistribution("exp");

            //List<Element> list5 = new List<Element>();
            //list5.Add(c5);
            //list5.Add(p15);
            //list5.Add(p25);
            //list5.Add(p35);
            //list5.Add(p45);
            //Model model5 = new Model(list5);
            //model5.simulate(1000.0);

            //addData(model5.getList(), model5);

            //#endregion Fifth

            //#region Sixth

            //Create c6 = new Create(2.0);
            //Process p16 = new Process(10.0, 2);
            //Process p26 = new Process(10.0, 2);
            //Process p36 = new Process(10.0, 2);
            //Process p46 = new Process(10.0, 2);

            //p26.disposable = true;
            //p46.disposable = true;

            //c6.setNextElement(p16);

            //p16.setNextElement(p26);
            //p16.setNextElement(p36);

            //p26.setNextElement(p16);
            //p26.setNextElement(p36);

            //p36.setNextElement(p16);
            //p36.setNextElement(p26);
            //p36.setNextElement(p46);

            //p46.setNextElement(p16);
            //p46.setNextElement(p36);

            //p16.setMaxqueue(5);
            //p26.setMaxqueue(5);
            //p36.setMaxqueue(5);
            //p46.setMaxqueue(5);

            //c6.setName("CREATOR");
            //p16.setName("PROCESSOR1");
            //p26.setName("PROCESSOR2");
            //p36.setName("PROCESSOR3");
            //p46.setName("PROCESSOR4");

            //c6.setDistribution("exp");
            //p16.setDistribution("exp");
            //p26.setDistribution("exp");
            //p36.setDistribution("exp");
            //p46.setDistribution("exp");

            //List<Element> list6 = new List<Element>();
            //list6.Add(c6);
            //list6.Add(p16);
            //list6.Add(p26);
            //list6.Add(p36);
            //list6.Add(p46);
            //Model model6 = new Model(list6);
            //model6.simulate(1000.0);

            //addData(model6.getList(), model6);

            //#endregion Sixth

            //#region Seventh

            //Create c7 = new Create(2.0);
            //Process p17 = new Process(1.0, 3);
            //Process p27 = new Process(1.0, 3);
            //Process p37 = new Process(1.0, 3);
            //Process p47 = new Process(1.0, 3);

            //p27.disposable = true;
            //p47.disposable = true;

            //c7.setNextElement(p17);

            //p17.setNextElement(p27);
            //p17.setNextElement(p37);

            //p27.setNextElement(p17);
            //p27.setNextElement(p37);

            //p37.setNextElement(p17);
            //p37.setNextElement(p27);
            //p37.setNextElement(p47);

            //p47.setNextElement(p17);
            //p47.setNextElement(p37);

            //p17.setMaxqueue(5);
            //p27.setMaxqueue(5);
            //p37.setMaxqueue(5);
            //p47.setMaxqueue(5);

            //c7.setName("CREATOR");
            //p17.setName("PROCESSOR1");
            //p27.setName("PROCESSOR2");
            //p37.setName("PROCESSOR3");
            //p47.setName("PROCESSOR4");

            //c7.setDistribution("exp");
            //p17.setDistribution("exp");
            //p27.setDistribution("exp");
            //p37.setDistribution("exp");
            //p47.setDistribution("exp");

            //List<Element> list7 = new List<Element>();
            //list7.Add(c7);
            //list7.Add(p17);
            //list7.Add(p27);
            //list7.Add(p37);
            //list7.Add(p47);
            //Model model7 = new Model(list7);
            //model7.simulate(1000.0);

            //addData(model7.getList(), model7);

            //#endregion Seventh

            //#region Eigth

            //Create c8 = new Create(2.0);
            //Process p18 = new Process(1.0, 3);
            //Process p28 = new Process(1.0, 3);
            //Process p38 = new Process(1.0, 3);
            //Process p48 = new Process(1.0, 3);

            //p28.disposable = true;
            //p48.disposable = true;

            //c8.setNextElement(p18);

            //p18.setNextElement(p28);
            //p18.setNextElement(p38);

            //p28.setNextElement(p18);
            //p28.setNextElement(p38);

            //p38.setNextElement(p18);
            //p38.setNextElement(p28);
            //p38.setNextElement(p48);

            //p48.setNextElement(p18);
            //p48.setNextElement(p38);

            //p18.setMaxqueue(5);
            //p28.setMaxqueue(5);
            //p38.setMaxqueue(5);
            //p48.setMaxqueue(5);

            //c8.setName("CREATOR");
            //p18.setName("PROCESSOR1");
            //p28.setName("PROCESSOR2");
            //p38.setName("PROCESSOR3");
            //p48.setName("PROCESSOR4");

            //c8.setDistribution("exp");
            //p18.setDistribution("exp");
            //p28.setDistribution("exp");
            //p38.setDistribution("exp");
            //p48.setDistribution("exp");

            //List<Element> list8 = new List<Element>();
            //list8.Add(c8);
            //list8.Add(p18);
            //list8.Add(p28);
            //list8.Add(p38);
            //list8.Add(p48);
            //Model model8 = new Model(list8);
            //model8.simulate(1000.0);

            //addData(model8.getList(), model8);

            //#endregion Eigth

            //#region Ninth

            //Create c9 = new Create(2.0);
            //Process p19 = new Process(1.0, 3);
            //Process p29 = new Process(1.0, 3);
            //Process p39 = new Process(1.0, 3);
            //Process p49 = new Process(1.0, 3);

            //p29.disposable = true;
            //p49.disposable = true;

            //c9.setNextElement(p19);

            //p19.setNextElement(p29);
            //p19.setNextElement(p39);

            //p29.setNextElement(p19);
            //p29.setNextElement(p39);

            //p39.setNextElement(p19);
            //p39.setNextElement(p29);
            //p39.setNextElement(p49);

            //p49.setNextElement(p19);
            //p49.setNextElement(p39);

            //p19.setMaxqueue(5);
            //p29.setMaxqueue(5);
            //p39.setMaxqueue(5);
            //p49.setMaxqueue(5);

            //c9.setName("CREATOR");
            //p19.setName("PROCESSOR1");
            //p29.setName("PROCESSOR2");
            //p39.setName("PROCESSOR3");
            //p49.setName("PROCESSOR4");

            //c9.setDistribution("exp");
            //p19.setDistribution("exp");
            //p29.setDistribution("exp");
            //p39.setDistribution("exp");
            //p49.setDistribution("exp");

            //List<Element> list9 = new List<Element>();
            //list9.Add(c9);
            //list9.Add(p19);
            //list9.Add(p29);
            //list9.Add(p39);
            //list9.Add(p49);
            //Model model9 = new Model(list9);
            //model9.simulate(1000.0);

            //addData(model9.getList(), model9);

            //#endregion Ninth

            //printTable();
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