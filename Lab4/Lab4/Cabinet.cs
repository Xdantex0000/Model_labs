using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab4
{
    public class Cabinet : Element
    {
        private Queue<double> pastTime = new Queue<double>();

        public static int disposedFromCabinet = 0;
        private Queue<Pacient> queue = new Queue<Pacient>();

        private int maxqueue, failure;
        private double meanQueue;

        public List<Element> Devices { get; set; } = new List<Element>();

        public Cabinet(double delay, int deviceNumber) : base(delay)
        {
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
            for (int i = 0; i < deviceNumber; i++)
            {
                Devices.Add(new Element("Doctor"));
            }
        }

        public override void inAct()
        {
            pastTime.Enqueue(getTcurr());

            Pacient pacient = null;
            double minT = double.MaxValue;
            Random rand = new Random();
            double num = rand.NextDouble();

            // ----------- Select the type -----------------
            if (num > 0 && num <= 0.5)
                pacient = new Pacient(Type.First);
            if (num > 0.5 && num <= 0.6)
                pacient = new Pacient(Type.Second);
            if (num > 0.6 && num <= 1)
                pacient = new Pacient(Type.Third);
            // ----------------------------------------------

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].getState() == 0)
                {
                    Devices[i].setState(1);
                    Devices[i].setTnext(base.getTcurr() + base.getDelay());
                    Devices[i].setType((int)pacient.Type);

                    foreach (var x in Devices)
                        if (minT > x.getTnext() && x.getState() == 1)
                            minT = x.getTnext();

                    base.setTnext(minT);
                    base.setZeroToMax();

                    return;
                }
            }
            if (getQueue().Count < getMaxqueue())
            {
                addToQueue(pacient);
            }
            else
            {
                failure++;
            }
        }

        public void inActFromLab(double time)
        {
            pastTime.Enqueue(time);

            Pacient pacient = new Pacient(Type.First);
            double minT = double.MaxValue;

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].getState() == 0)
                {
                    Devices[i].setState(1);
                    Devices[i].setTnext(base.getTcurr() + base.getDelay());
                    Devices[i].setType((int)pacient.Type);

                    foreach (var x in Devices)
                        if (minT > x.getTnext() && x.getState() == 1)
                            minT = x.getTnext();

                    base.setTnext(minT);
                    base.setZeroToMax();

                    return;
                }
            }
            if (getQueue().Count < getMaxqueue())
            {
                addToQueue(pacient);
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

            if (outDevice.getType() == (int)Type.First)
            {
                var follower = (Follower)getNextElements()[0];
                follower.inActTime(pastTime.Dequeue());
            }
            else
            {
                var rtl = (RoadToLab)getNextElements()[1];
                rtl.inActTime(pastTime.Dequeue());
            }

            outDevice.setType(0);
            // -------------------------------------------

            addQuantity();
            base.setTnext(double.MaxValue);

            if (getQueue().Count > 0)
            {
                // -------------- Delete pacient from queue ------------
                Queue<Pacient> pacientQueue = new Queue<Pacient>();
                bool deleted = false;
                foreach (var x in queue)
                {
                    if (x.Type == Type.First && deleted == false)
                    {
                        deleted = true;
                        outDevice.setType((int)x.Type);
                    }
                    else
                        pacientQueue.Enqueue(x);
                }
                queue = pacientQueue;
                if (deleted == false)
                    outDevice.setType((int)queue.Dequeue().Type);
                // ---------------------------------------------------

                outDevice.setState(1);
                outDevice.setTnext(base.getTcurr() + base.getDelay());
            }
            foreach (var x in Devices)
                if (minT > x.getTnext() && x.getState() == 1)
                    minT = x.getTnext();
            base.setTnext(minT);

            disposedFromCabinet++;
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

        public Queue<Pacient> getQueue()
        {
            return queue;
        }

        public void addToQueue(Pacient item)
        {
            this.queue.Enqueue(item);
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
    }
}