using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class Lab : Element
    {
        public static List<double> ResultLabTime = new List<double>();
        public static List<double> inInterval = new List<double>();

        private Queue<double> pastTime = new Queue<double>();
        public static int disposedFromLab = 0;
        private int queue, maxqueue, failure;
        private double meanQueue;
        private int erlangK;

        public List<Element> Devices { get; set; } = new List<Element>();

        public Lab(double delay, int deviceNumber, int erlang) : base(delay)
        {
            queue = 0;
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
            erlangK = erlang;
            for (int i = 0; i < deviceNumber; i++)
            {
                Devices.Add(new Element("Laborant"));
            }
        }

        public void inActTime(double time)
        {
            pastTime.Enqueue(time);
            inInterval.Add(getTcurr());
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
            double pastTnext = 0.0;
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
            pastTnext = outDevice.getTnext();
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

            Random rand = new Random();
            if (rand.NextDouble() <= 0.5)
            {
                if (pastTnext != 0.0)
                {
                    disposed++;
                    ResultLabTime.Add(pastTnext - pastTime.Dequeue());
                    disposedFromLab++;
                }
            }
            else
            {
                var cabinet = (Cabinet)getNextElements()[0];
                cabinet.inActFromLab(pastTime.Dequeue());
            }
        }

        public static List<double> avgInInterval()
        {
            var result = new List<double>();

            for (int i = 0; i < inInterval.Count - 1; i++)
                result.Add(inInterval[i + 1] - inInterval[i]);

            return result;
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