using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class Program
    {
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
    }
}