using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class RoadToLab : Element
    {
        private Queue<double> pastTime = new Queue<double>();
        public static int disposedFromRoadToLab = 0;

        public List<Element> Devices { get; set; } = new List<Element>();
        private int failure;

        public RoadToLab() : base(new Random().NextDouble() * (5.0 - 2.0) + 2.0)
        {
            failure = 0;
            for (int i = 0; i < 50; i++)
            {
                Devices.Add(new Element("Follower"));
            }
        }

        public void inActTime(double time)
        {
            pastTime.Enqueue(time);
            double minT = double.MaxValue;

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].getState() == 0)
                {
                    Devices[i].setState(1);
                    Devices[i].setTnext(base.getTcurr() + base.getDelay());

                    foreach (var x in Devices)
                        if (minT > x.getTnext() && x.getState() == 1)
                            minT = x.getTnext();

                    base.setTnext(minT);
                    base.setZeroToMax();

                    return;
                }
            }
            failure++;
        }

        public override void outAct()
        {
            Element outDevice = null;
            double minT = double.MaxValue;
            // ------------- Find device with correct Tnext ---------------
            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].getState() == 1 && Devices[i].getTnext() == base.getTcurr())
                {
                    outDevice = Devices[i];
                }
                else if (outDevice == null && i == Devices.Count - 1)
                    throw new Exception("Error");
            }
            // -------------- Reset Device ---------------
            outDevice.outAct();
            outDevice.setTnext(double.MaxValue);
            outDevice.setState(0);
            outDevice.setType(0);
            // -------------------------------------------

            addQuantity();
            base.setTnext(double.MaxValue);

            foreach (var x in Devices)
                if (minT > x.getTnext() && x.getState() == 1)
                    minT = x.getTnext();
            base.setTnext(minT);

            var registation = (Registration)getNextElements()[0];
            registation.inActTime(pastTime.Dequeue());
            disposedFromRoadToLab++;
        }

        protected void addQuantity()
        {
            base.outAct();
        }

        public int getFailure()
        {
            return failure;
        }
    }
}