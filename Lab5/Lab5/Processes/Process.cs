using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    public class Process : Element
    {
        public bool disposable = false;
        protected int queue, maxqueue, failure;
        private double meanQueue;
        private double lastMove = 0;
        private double lastQMove = 0;

        public double MeanLoad { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();

        public List<int>[] RList { get; set; }
        public List<double>[] RInterval { get; set; }
        public List<int> QList { get; set; } = new List<int>();

        public List<double> QInterval { get; set; } = new List<double>();

        public Process(double delay, int deviceNumber) : base(delay)
        {
            queue = 0;
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
            MeanLoad = 0.0;
            for (int i = 0; i < deviceNumber; i++)
            {
                Devices.Add(new Device("Device"));
            }
            RList = new List<int>[deviceNumber];
            for (int i = 0; i < deviceNumber; i++)
                RList[i] = new List<int>();
            RInterval = new List<double>[deviceNumber];
            for (int i = 0; i < deviceNumber; i++)
                RInterval[i] = new List<double>();
        }

        public override void inAct()
        {
            double minT = double.MaxValue;

            for (int i = 0; i < Devices.Count(); i++)
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
            for (int i = 0; i < Devices.Count(); i++)
            {
                if (Devices[i].State == 1 && Devices[i].Tnext == base.getTcurr())
                {
                    iteration = i;
                    outDevice = Devices[i];
                }
                else if (outDevice == null && i == Devices.Count() - 1)
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
            if (disposable == false)
                getNextElements()[0].inAct();
            else
                disposed++;
        }

        protected void addQuantity()
        {
            base.outAct();
        }

        public int getFailure()
        {
            return failure;
        }

        protected void addFailure()
        {
            failure++;
        }

        public int getQueue()
        {
            return queue;
        }

        public void setQueue(int queue)
        {
            this.queue = queue;
        }

        public int getMaxqueue()
        {
            return maxqueue;
        }

        public void setMaxqueue(int maxqueue)
        {
            this.maxqueue = maxqueue;
        }

        public void printInfo()
        {
        }

        public override void doStatistics(double delta)
        {
            meanQueue = getMeanQueue() + queue * delta;
            foreach (var x in Devices)
                MeanLoad += x.State * delta;
        }

        public double getMeanQueue()
        {
            return meanQueue;
        }

        public void addPressureVariable(int item, double tcurr, int iteration)
        {
            RList[iteration].Add(item);
            RInterval[iteration].Add(tcurr - lastMove);
            lastMove = tcurr;
        }

        public void addQueueVariable(int queue, double tcurr)
        {
            QList.Add(queue);
            QInterval.Add(tcurr - lastQMove);
            lastQMove = tcurr;
        }
    }
}