using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class FirstProcess : Process
    {
        public FirstProcess(double delay, int deviceNumber) : base(delay, deviceNumber)
        {
        }

        public override void inAct()
        {
            double minT = double.MaxValue;

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].State == 0)
                {
                    Devices[i].State = 1;
                    Devices[i].Tnext = base.getTcurr() + base.getDelay();

                    foreach (var x in Devices)
                        if (minT > x.Tnext && x.State == 1)
                            minT = x.Tnext;

                    base.setTnext(minT);
                    base.setZeroToMax();

                    addPressureVariable(0, getTcurr(), i);

                    return;
                }
            }
            if (getQueue() < getMaxqueue())
            {
                addQueueVariable(getQueue(), getTcurr());

                setQueue(getQueue() + 1);
            }
            else
            {
                failure++;
            }
        }

        public override void outAct()
        {
            bool inQueue = false;
            int iteration = int.MinValue;
            Device outDevice = null;
            double minT = double.MaxValue;
            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].State == 1 && Devices[i].Tnext == base.getTcurr())
                {
                    iteration = i;
                    outDevice = Devices[i];
                }
                else if (outDevice == null && i == Devices.Count - 1)
                    throw new Exception("Error");
            }
            outDevice.addQuantity();
            outDevice.Tnext = double.MaxValue;
            outDevice.State = 0;

            addQuantity();
            base.setTnext(double.MaxValue);

            if (getQueue() > 0)
            {
                inQueue = true;

                addQueueVariable(getQueue(), getTcurr());

                setQueue(getQueue() - 1);
                outDevice.State = 1;
                outDevice.Tnext = base.getTcurr() + base.getDelay();
            }
            foreach (var x in Devices)
                if (minT > x.Tnext && x.State == 1)
                    minT = x.Tnext;
            base.setTnext(minT);
            if (inQueue == false && iteration != int.MinValue)
            {
                addPressureVariable(1, getTcurr(), iteration);
            }

            Random rand = new Random();
            double num = rand.NextDouble();
            if (num > 0 && num <= 0.42)
                disposed++;
            else if (num > 0.42 && num <= 0.57)
                getNextElements()[0].inAct();
            else if (num > 0.57 && num <= 0.7)
                getNextElements()[1].inAct();
            else if (num > 0.7 && num <= 1)
                getNextElements()[2].inAct();
        }
    }
}