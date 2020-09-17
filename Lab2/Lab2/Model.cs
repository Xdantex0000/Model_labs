using System;
using System.Collections.Generic;

namespace Lab2
{
    public class Model
    {
        private readonly double _delayCreate;
        private readonly double _delayProcess;
        private readonly int maxqueue;
        private int queue;
        private int nextEvent;

        private int numCreate, numProcess, failure;

        private State state;
        private double tcurr;
        private double timeCreate, timeProcess;

        private List<double> timeCreateList = new List<double>();
        private List<int> queueValues = new List<int>();
        private List<double> queueTime = new List<double>();

        public Model(double delay0, double delay1, int maxQ = 3)
        {
            _delayCreate = delay0;
            _delayProcess = delay1;
            tnext = 0.0;
            tcurr = tnext;
            timeCreate = tcurr;
            timeProcess = double.MaxValue;
            maxqueue = maxQ;
        }

        private double tnext { get; set; }

        public void simulate(double timeModeling)
        {
            while (tcurr < timeModeling)
            {
                tnext = timeCreate;
                nextEvent = 0;

                if (timeProcess < tnext)
                {
                    tnext = timeProcess;
                    nextEvent = 1;
                }

                tcurr = tnext;
                switch (nextEvent)
                {
                    case 0:
                        {
                            eventimeCreate();
                            break;
                        }
                    case 1:
                        {
                            eventimeProcess();
                            break;
                        }
                }

                printInfo();
            }

            printStatistic(timeModeling);
        }

        public void printStatistic(double allTime)
        {
            var modifications = new Modifications();
            var notCompleted = numCreate - numProcess;
            double notCompletedProbability = (double)failure / (double)numCreate;
            var intensivityOfRequests = numCreate / allTime;
            var intensivityOfService = numProcess / allTime;

            var avgEnterCreate = modifications.RequestsArrivedPerTime(intensivityOfRequests, intensivityOfService);

            var avgPres = modifications.AvgPressureCalculate(queueValues, queueTime, allTime);
            var avgWaitInQueue = modifications.AvgWaitInQueue(queueValues, queueTime, numProcess);

            Console.WriteLine(
                $"\n numCreate = {numCreate} numProcess = {numProcess} failure = {failure}\n " +
                $"Average pressure: {avgPres}\n Average wait in queue: {avgWaitInQueue}\n Average enter create: {avgEnterCreate}\n " +
                $"Fail probability: {notCompletedProbability}\n Not completed: {notCompleted}\n\n" +
                $"Intensivity of Request: {intensivityOfRequests}\nIntensivity of Service: {intensivityOfService}");
        }

        public void printInfo()
        {
            Console.WriteLine($" t= {tcurr}\t state = {state}\t queue = {queue}");
        }

        public void eventimeCreate()
        {
            timeCreate = tcurr + getDelayOfCreate();
            numCreate++;
            if (state == State.Active)
            {
                state = State.Busy;
                timeProcess = tcurr + getDelayOfProcess();
            }
            else
            {
                if (queue < maxqueue)
                {
                    queue++;
                    queueValues.Add(queue);
                    queueTime.Add(timeProcess - tcurr);
                }
                else
                    failure++;
            }
        }

        public void eventimeProcess()
        {
            timeProcess = double.MaxValue;
            state = State.Active;
            if (queue > 0)
            {
                queue--;
                state = State.Busy;
                timeProcess = tcurr + getDelayOfProcess();
            }

            numProcess++;
        }

        private double getDelayOfCreate()
        {
            return FunRand.Exp(_delayCreate);
        }

        private double getDelayOfProcess()
        {
            return FunRand.Exp(_delayProcess);
        }
    }
}