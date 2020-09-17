using System;

namespace Lab3
{
    public class Element
    {
        private static int nextId;
        private double delayMean, delayDev;
        private string distribution;
        private int id;
        private string name;
        private Element nextElement;
        private int quantity;
        private int state;
        private double tcurr;
        private double tnext;


        public Element()
        {
            tnext = 0.0;
            delayMean = 1.0;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public Element(double delay)
        {
            name = "anonymus";
            tnext = 0.0;
            delayMean = delay;
            distribution = "";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public Element(string nameOfElement, double delay)
        {
            name = nameOfElement;
            tnext = 0.0;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public double getDelay()
        {
            var delay = getDelayMean();
            if ("exp".equalsIgnoreCase(getDistribution()))
            {
                delay = FunRand.Exp(getDelayMean());
            }
            else
            {
                if ("norm".equalsIgnoreCase(getDistribution()))
                {
                    delay = FunRand.Norm(getDelayMean(),
                        getDelayDev());
                }
                else
                {
                    if ("unif".equalsIgnoreCase(getDistribution()))
                    {
                        delay = FunRand.Unif(getDelayMean(),
                            getDelayDev());
                    }
                    else
                    {
                        if ("".equalsIgnoreCase(getDistribution()))
                            delay = getDelayMean();
                    }
                }
            }

            return delay;
        }


        public double getDelayDev()
        {
            return delayDev;
        }

        public void setDelayDev(double delayDev)
        {
            this.delayDev = delayDev;
        }

        public string getDistribution()
        {
            return distribution;
        }

        public void setDistribution(string distribution)
        {
            this.distribution = distribution;
        }


        public int getQuantity()
        {
            return quantity;
        }

        public double getTcurr()
        {
            return tcurr;
        }

        public void setTcurr(double tcurr)
        {
            this.tcurr = tcurr;
        }

        public int getState()
        {
            return state;
        }

        public void setState(int state)
        {
            this.state = state;
        }

        public Element getNextElement()
        {
            return nextElement;
        }

        public void setNextElement(Element nextElement)
        {
            this.nextElement = nextElement;
        }

        public void inAct()
        {
        }

        public void outAct()
        {
            quantity++;
        }

        public double getTnext()
        {
            return tnext;
        }


        public void setTnext(double tnext)
        {
            this.tnext = tnext;
        }

        public double getDelayMean()
        {
            return delayMean;
        }

        public void setDelayMean(double delayMean)
        {
            this.delayMean = delayMean;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void printResult()
        {
            Console.WriteLine(getName() + "  quantity = " + quantity);
        }

        public void printInfo()
        {
            Console.WriteLine(getName() + " state= " + state + " quantity = " + quantity + " tnext= " + tnext);
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void doStatistics(double delta)
        {
        }
    }
}