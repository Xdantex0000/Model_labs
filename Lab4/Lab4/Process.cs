using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    public class Process : Element
    {
        public bool disposable = false;
        private int queue, maxqueue, failure;
        private double meanQueue;
        private double lastMove = 0;
        private double lastQMove = 0;

        public List<Element> Devices { get; set; } = new List<Element>();

        public List<int>[] RList { get; set; }
        public List<double>[] RInterval { get; set; }
        public List<int> QList { get; set; } = new List<int>();

        public List<double> QInterval { get; set; } = new List<double>();

        public Process(double delay, int deviceNumber) : base(delay)
        {
            queue = 0;
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
            for (int i = 0; i < deviceNumber; i++)
            {
                Devices.Add(new Element("Device"));
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
                if (Devices[i].getState() == 0)
                {
                    Devices[i].setState(1);
                    Devices[i].setTnext(base.getTcurr() + base.getDelay());

                    foreach (var x in Devices)
                        if (minT > x.getTnext() && x.getState() == 1)
                            minT = x.getTnext();

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
            Element outDevice = null;
            double minT = double.MaxValue;
            for (int i = 0; i < Devices.Count(); i++)
            {
                if (Devices[i].getState() == 1 && Devices[i].getTnext() == base.getTcurr())
                {
                    iteration = i;
                    outDevice = Devices[i];
                }
                else if (outDevice == null && i == Devices.Count() - 1)
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

            Random rand = new Random();
            double num = rand.NextDouble();
            int size = getNextElements().Count();
            if (disposable == true)
                size++;
            for (int i = 0; i < size; i++)
            {
                if (num > (double)i / (double)size && num <= (double)(i + 1) / (double)size)
                    if (disposable == true && i == size - 1)
                        disposed++;
                    else
                        getNextElements()[i].inAct();
            }
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
            printInfo();
            Console.WriteLine("failure = " + this.getFailure());
        }

        public override void doStatistics(double delta)
        {
            meanQueue = getMeanQueue() + queue * delta;
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