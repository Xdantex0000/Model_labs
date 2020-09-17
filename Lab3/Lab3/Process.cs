using System;

namespace Lab3
{
    public class Process : Element {

    private int queue, maxqueue, failure;
    private double meanQueue;

    public Process(double delay) : base(delay) {
        queue = 0;
        maxqueue = int.MaxValue;
        meanQueue = 0.0;
    }

    public override void inAct() {
        if (getState() == 0) {
            setState(1);
            setTnext(getTcurr() + getDelay());
        } else {
            if (getQueue() < getMaxqueue()) {
                setQueue(getQueue() + 1);
            } else {
                failure++;
            }
        }
    }

    public override void outAct() {
        outAct();
        setTnext(double.MaxValue);
        setState(0);

        if (getQueue() > 0) {
            setQueue(getQueue() - 1);
            setState(1);
            setTnext(getTcurr() + getDelay());
        }
    }

    
    public int getFailure() {
        return failure;
    }

    public int getQueue() {
        return queue;
    }

    
    public void setQueue(int queue) {
        this.queue = queue;
    }


    public int getMaxqueue() {
        return maxqueue;
    }


    public void setMaxqueue(int maxqueue) {
        this.maxqueue = maxqueue;
    }

    public void printInfo() {
        printInfo();
        Console.WriteLine("failure = " + this.getFailure());
    }

    public override void doStatistics(double delta) {
        meanQueue = getMeanQueue() + queue * delta;
    }

    public double getMeanQueue() {
        return meanQueue;
    }
    }
}