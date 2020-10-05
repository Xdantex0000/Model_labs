using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class Cashbox : Process
    {
        public static List<double> OutWindow = new List<double>();
        public static List<double> InBank = new List<double>();
        private static double nextWindowMove = 0.0;
        public static int LineChange { get; set; }

        public Cashbox(double delay, int deviceNumber) : base(delay, deviceNumber)
        {
        }

        public override void inAct()
        {
            var proc = (Process)getNextElements()[0];
            var procQueueValue = proc.getQueue();

            if (this.getQueue() > procQueueValue)
                proc.inAct();
            else
            {
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

                        InBank.Add(this.getTnext() - this.getTcurr());

                        base.setZeroToMax();

                        addPressureVariable(0, getTcurr(), i);

                        return;
                    }
                }
                if (getQueue() < getMaxqueue())
                {
                    addQueueVariable(this.getQueue(), getTcurr());

                    this.setQueue(this.getQueue() + 1);
                }
                else
                {
                    addFailure();
                }
            }
        }

        public override void outAct()
        {
            bool inQueue = false;
            int iteration = int.MinValue;
            Element outDevice = null;
            double minT = double.MaxValue;

            OutWindow.Add(this.getTnext() - nextWindowMove);
            nextWindowMove = this.getTnext();

            balanceQueue();

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].getState() == 1 && Devices[i].getTnext() == base.getTcurr())
                {
                    iteration = i;
                    outDevice = Devices[i];
                }
                else if (outDevice == null && i == Devices.Count - 1)
                    throw new Exception("Error");
            }
            outDevice.outAct();
            outDevice.setTnext(double.MaxValue);
            outDevice.setState(0);

            addQuantity();

            base.setTnext(double.MaxValue);

            if (getQueue() > 0)
            {
                inQueue = true;

                addQueueVariable(getQueue(), getTcurr());

                setQueue(getQueue() - 1);
                outDevice.setState(1);
                outDevice.setTnext(base.getTcurr() + base.getDelay());
            }
            foreach (var x in Devices)
                if (minT > x.getTnext() && x.getState() == 1)
                    minT = x.getTnext();
            base.setTnext(minT);
            if (inQueue == false && iteration != int.MinValue)
            {
                addPressureVariable(1, getTcurr(), iteration);
            }

            disposed++;
        }

        private void balanceQueue()
        {
            var proc = (Process)getNextElements()[0];
            var procQueueValue = proc.getQueue();

            if (this.getQueue() == procQueueValue - 1 || (this.getQueue() > procQueueValue - 1 && this.getQueue() > 1))
            {
                LineChange++;
                proc.inAct();

                addQueueVariable(this.getQueue(), getTcurr());

                this.setQueue(this.getQueue() - 1);
            }
        }
    }
}