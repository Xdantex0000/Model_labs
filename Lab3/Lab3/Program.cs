using System;
using System.Collections.Generic;

namespace Lab3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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

            Console.WriteLine($"Id{c.getId()} = {c.getName()}\tId{p1.getId()} = {p1.getName()}\tId{p2.getId()} = {p2.getName()}\tId{p3.getId()} = {p3.getName()}\tId{p4.getId()} = {p4.getName()}");

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
            model.simulate(1000.0);
        }
    }
}