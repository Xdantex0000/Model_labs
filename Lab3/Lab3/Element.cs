using System;
using System.Collections.Generic;

namespace Lab3
{
    public class Element
    {
        private static int nextId;
        public static int disposed = 0;

        // середеє значення часової затримки / середнє квадратичне відхилення часової затримки
        private double delayMean, delayDev;

        private string distribution;
        private int id;
        private string name;
        private List<Element> nextElement = new List<Element>();
        private int quantity;
        private int state;

        // поточний момент часу
        private double tcurr;

        // момент часу наступної події
        private double tnext;

        public Element()
        {
            tnext = 0.0;
            delayMean = 1.0;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public Element(double delay)
        {
            tnext = 0.0;
            delayMean = delay;
            distribution = "";
            tcurr = tnext;
            state = 0;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public Element(string deviceName)
        {
            tnext = 0.0;
            delayMean = 1.0;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            id = nextId - 1;
            name = deviceName;
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

        // розрахунок часової затримками
        public double getDelay()
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
                        if ("".ToLower() == getDistribution().ToLower())
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

        public List<Element> getNextElements()
        {
            return nextElement;
        }

        public void setNextElement(Element _nextElement)
        {
            this.nextElement.Add(_nextElement);
        }

        public void setZeroToMax()
        {
            foreach (var x in getNextElements())
            {
                if (x.getTnext() == 0)
                {
                    x.setTnext(double.MaxValue);
                    x.setZeroToMax();
                }
            }
        }

        // вхід в елемент
        public virtual void inAct()
        {
        }

        // вихід з елементу
        public virtual void outAct()
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

        public virtual void doStatistics(double delta)
        {
        }
    }
}