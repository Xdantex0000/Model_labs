using System.Collections.Generic;

namespace Lab7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Task 1
            List<Element> elements = new List<Element>();

            Element p1 = new Position(1, "Message from A to B\t\t");
            Element p2 = new Position(0, "Start sending from A to B\t");
            Element p3 = new Position(0, "Send from A to B\t\t");
            Element p4 = new Position(0, "Receive from A to B\t\t");
            Element p5 = new Position(1, "Freed B\t\t\t\t");
            Element p6 = new Position(1, "Message from B to A\t\t");
            Element p7 = new Position(0, "Start sending from B to A\t");
            Element p8 = new Position(0, "Send from B to A\t\t");
            Element p9 = new Position(0, "Receive from B to A\t\t");
            Element p10 = new Position(1, "Freed A\t\t\t\t");
            Element p11 = new Position(1, "State controller\t\t");
            Element t1 = new Transition(1.0, "t1");
            Element t2 = new Transition(1.0, "t2");
            Element t3 = new Transition(1.0, "t3");
            Element t4 = new Transition(1.0, "t4");
            Element t5 = new Transition(1.0, "t5");
            Element t6 = new Transition(1.0, "t6");
            Element t7 = new Transition(1.0, "t7");
            Element t8 = new Transition(1.0, "t8");

            Element[] items = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, t1, t2, t3, t4, t5, t6, t7, t8 };

            p1.OutArcs.Add(new Arc(1, t1, 1.0));
            t1.OutArcs.Add(new Arc(1, p2, 1.0));
            p2.OutArcs.Add(new Arc(1, t2, 1.0));
            t2.OutArcs.Add(new Arc(1, p3, 1.0));
            p3.OutArcs.Add(new Arc(1, t3, 1.0));
            t3.OutArcs.Add(new Arc(1, p4, 1.0));
            p4.OutArcs.Add(new Arc(1, t4, 1.0));
            t4.OutArcs.Add(new Arc(1, p5, 1.0));
            p5.OutArcs.Add(new Arc(1, t1, 1.0));
            t4.OutArcs.Add(new Arc(1, p1, 1.0));
            t4.OutArcs.Add(new Arc(1, p11, 1.0));
            p6.OutArcs.Add(new Arc(1, t5, 1.0));
            t5.OutArcs.Add(new Arc(1, p7, 1.0));
            p7.OutArcs.Add(new Arc(1, t6, 1.0));
            t6.OutArcs.Add(new Arc(1, p8, 1.0));
            p8.OutArcs.Add(new Arc(1, t7, 1.0));
            t7.OutArcs.Add(new Arc(1, p9, 1.0));
            p9.OutArcs.Add(new Arc(1, t8, 1.0));
            t8.OutArcs.Add(new Arc(1, p10, 1.0));
            t8.OutArcs.Add(new Arc(1, p6, 1.0));
            p10.OutArcs.Add(new Arc(1, t5, 1.0));
            t8.OutArcs.Add(new Arc(1, p11, 1.0));
            p11.OutArcs.Add(new Arc(1, t2, 1.0));
            p11.OutArcs.Add(new Arc(1, t6, 1.0));

            Model model = new Model(elements);
            model.list.AddRange(items);
            model.simulate(100000, false);
            //// Task 2
            //int n = 10;

            //List<Element> elements = new List<Element>();

            //Element p1 = new Position(10, "Create limitation");
            //Element p2 = new Position(0, "Buffer");
            //Element p3 = new Position(0, "Processed");
            //Element p4 = new Position(n, "Buffer limitation");
            //Element t1 = new Transition(1.0, "t1");
            //Element t2 = new Transition(1.0, "t2");

            //Element[] items = { p1, p2, p3, p4, t1, t2 };

            //p1.OutArcs.Add(new Arc(1, t1, 1.0));
            //p4.OutArcs.Add(new Arc(1, t1, 1.0));
            //t1.OutArcs.Add(new Arc(1, p2, 1.0));
            //p2.OutArcs.Add(new Arc(1, t2, 1.0));
            //t2.OutArcs.Add(new Arc(1, p4, 1.0));
            //t2.OutArcs.Add(new Arc(1, p3, 1.0));

            //Model model = new Model(elements);
            //model.list.AddRange(items);
            //model.simulate(11, true);

            //// Task 3
            //int n = 12;

            //List<Element> elements = new List<Element>();

            //Element p1 = new Position(1, "Create marker");
            //Element p2 = new Position(0, "Tasks queue");
            //Element p3 = new Position(0, "Task1 on processing");
            //Element p4 = new Position(0, "Task2 on processing");
            //Element p5 = new Position(0, "Task3 on processing");
            //Element p6 = new Position(0, "Task1 processed");
            //Element p7 = new Position(0, "Task2 processed");
            //Element p8 = new Position(0, "Task3 processed");
            //Element p9 = new Position(n, "System resources");
            //Element p10 = new Position(0, "Tasks 1 queue");
            //Element p11 = new Position(0, "Tasks 2 queue");
            //Element p12 = new Position(0, "Tasks 3 queue");
            //Element t1 = new Transition(1.0, "t1");
            //Element t2 = new Transition(1.0, "t2");
            //Element t3 = new Transition(1.0, "t3");
            //Element t4 = new Transition(1.0, "t4");
            //Element t5 = new Transition(1.0, "t5");
            //Element t6 = new Transition(1.0, "t6");
            //Element t7 = new Transition(1.0, "t7");
            //Element t8 = new Transition(1.0, "t8");
            //Element t9 = new Transition(1.0, "t9");
            //Element t10 = new Transition(1.0, "t10");

            //Element[] items = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10 };

            //p1.OutArcs.Add(new Arc(1, t1, 1.0));
            //t1.OutArcs.Add(new Arc(1, p1, 1.0));
            //t1.OutArcs.Add(new Arc(1, p2, 1.0));
            //p2.OutArcs.Add(new Arc(1, t8, 1.0));
            //p2.OutArcs.Add(new Arc(1, t9, 1.0));
            //p2.OutArcs.Add(new Arc(1, t10, 1.0));
            //t8.OutArcs.Add(new Arc(1, p10, 1.0));
            //t9.OutArcs.Add(new Arc(1, p11, 1.0));
            //t10.OutArcs.Add(new Arc(1, p12, 1.0));
            //p10.OutArcs.Add(new Arc(1, t2, 1.0));
            //p11.OutArcs.Add(new Arc(1, t3, 1.0));
            //p12.OutArcs.Add(new Arc(1, t4, 1.0));
            //t2.OutArcs.Add(new Arc(1, p3, 1.0));
            //t3.OutArcs.Add(new Arc(1, p4, 1.0));
            //t4.OutArcs.Add(new Arc(1, p5, 1.0));
            //p3.OutArcs.Add(new Arc(1, t5, 1.0));
            //p4.OutArcs.Add(new Arc(1, t6, 1.0));
            //p5.OutArcs.Add(new Arc(1, t7, 1.0));
            //t5.OutArcs.Add(new Arc(1, p6, 1.0));
            //t6.OutArcs.Add(new Arc(1, p7, 1.0));
            //t7.OutArcs.Add(new Arc(1, p8, 1.0));
            //t5.OutArcs.Add(new Arc((int)n, p9, 1.0));
            //t6.OutArcs.Add(new Arc((int)(n / 3), p9, 1.0));
            //t7.OutArcs.Add(new Arc((int)(n / 2), p9, 1.0));
            //p9.OutArcs.Add(new Arc((int)n, t2, 1.0));
            //p9.OutArcs.Add(new Arc((int)(n / 3), t3, 1.0));
            //p9.OutArcs.Add(new Arc((int)(n / 2), t4, 1.0));

            //Model model = new Model(elements);
            //model.list.AddRange(items);
            //model.simulate(100, false);
        }
    }
}