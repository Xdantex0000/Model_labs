using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    internal class Registration : Element
    {
        private Queue<double> pastTime = new Queue<double>();
        public static int disposedFromRegistration = 0;
        private int queue, maxqueue, failure;
        private double meanQueue;
        private int erlangK;

        public List<Element> Devices { get; set; } = new List<Element>();

        public Registration(double delay, int deviceNumber, int erlang) : base(delay)
        {
            queue = 0;
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
            erlangK = erlang;
            for (int i = 0; i < deviceNumber; i++)
            {
                Devices.Add(new Element("Registrator"));
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
                    Devices[i].setTnext(base.getTcurr() + getDelay());

                    foreach (var x in Devices)
                        if (minT > x.getTnext() && x.getState() == 1)
                            minT = x.getTnext();

                    base.setTnext(minT);
                    base.setZeroToMax();

                    return;
                }
            }
            if (getQueue() < getMaxqueue())
            {
                this.setQueue(getQueue() + 1);
            }
            else
            {
                failure++;
            }
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

            if (getQueue() > 0)
            {
                setQueue(getQueue() - 1);
                outDevice.setState(1);
                outDevice.setTnext(base.getTcurr() + getDelay());
            }
            foreach (var x in Devices)
                if (minT > x.getTnext() && x.getState() == 1)
                    minT = x.getTnext();
            base.setTnext(minT);

            var lab = (Lab)getNextElements()[0];
            lab.inActTime(pastTime.Dequeue());

            disposedFromRegistration++;
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

        public double getMeanQueue()
        {
            return meanQueue;
        }

        public override double getDelay()
        {
            var delay = getDelayMean();
            if ("exp".ToLower() == getDistribution().ToLower())
            {
                delay = FunRand.Exp(getDelayMean());
            }
            else
            {
                if ("norm".ToLower() == getDistribution().ToLower())
                {
                    delay = FunRand.Norm(getDelayMean(),
                        getDelayDev());
                }
                else
                {
                    if ("unif".ToLower() == getDistribution().ToLower())
                    {
                        delay = FunRand.Unif(getDelayMean(),
                            getDelayDev());
                    }
                    else
                    {
                        if ("erlang".ToLower() == getDistribution().ToLower())
                        {
                            delay = FunRand.Erlang(getDelayMean(),
                                erlangK);
                        }
                        else
                        {
                            if ("".ToLower() == getDistribution().ToLower())
                                delay = getDelayMean();
                        }
                    }
                }
            }

            return delay;
        }
    }
}