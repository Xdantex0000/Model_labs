using System.Collections.Generic;

namespace Lab7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Element> elements = new List<Element>();

            Element p1 = new Position(1, "p1");
            Element t1 = new Transition(1.0, "t1");
            Element p2 = new Position(0, "p2");
            Element t2 = new Transition(1.0, "t2");
            Element p3 = new Position(1, "p3");
            Element t3 = new Transition(1.0, "t3");
            Element p4 = new Position(0, "p4");
            Element t4 = new Transition(1.0, "t4");
            Element p5 = new Position(0, "p5");
            Element p6 = new Position(0, "p6");
            Element[] items = { p1, t1, p2, t2, p3, t3, p4, t4, p5, p6 };

            p1.OutArcs.Add(new Arc(1, t1, 1.0));
            t1.OutArcs.Add(new Arc(1, p1, 1.0));
            t1.OutArcs.Add(new Arc(1, p2, 1.0));
            p2.OutArcs.Add(new Arc(1, t2, 1.0));
            t2.OutArcs.Add(new Arc(1, p4, 1.0));
            p4.OutArcs.Add(new Arc(1, t3, 1.0));
            t3.OutArcs.Add(new Arc(1, p3, 1.0));
            p3.OutArcs.Add(new Arc(1, t2, 1.0));
            t3.OutArcs.Add(new Arc(1, p5, 1.0));
            p5.OutArcs.Add(new Arc(1, t4, 1.0));
            t4.OutArcs.Add(new Arc(1, p6, 1.0));

            Model model = new Model(elements);
            model.list.AddRange(items);
            model.simulate(1000, true);

            //List<Element> elements = new List<Element>();

            //Element p1 = new Position(1, "p1");
            //Element t1 = new Transition(1.0, "t1");
            //Element p2 = new Position(0, "p2");
            //Element t2 = new Transition(1.0, "t2");
            //Element p3 = new Position(1, "p3");
            //Element p4 = new Position(1, "p4");
            //Element t3 = new Transition(1.0, "t3");
            //Element[] items = { p1, t1, p2, t2, p3, t3, p4 };

            //p1.OutArcs.Add(new Arc(1, t1, 1.0));
            //t1.OutArcs.Add(new Arc(1, p1, 1.0));
            //t1.OutArcs.Add(new Arc(1, p2, 1.0));
            //p2.OutArcs.Add(new Arc(1, t2, 1.0));
            //p2.OutArcs.Add(new Arc(1, t3, 1.0));
            //t2.OutArcs.Add(new Arc(1, p4, 1.0));
            //t3.OutArcs.Add(new Arc(1, p3, 1.0));

            //Model model = new Model(elements);
            //model.list.AddRange(items);
            //model.simulate(1000, true);
        }
    }
}