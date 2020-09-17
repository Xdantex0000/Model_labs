using System.Collections.Generic;

namespace Lab2
{
    public class Modifications
    {
        public double AvgPressureCalculate(List<int> queueValue, List<double> queueTime, double allTime)
        {
            double result;
            double chisl = 0.0;

            int listLength = queueValue.Count;

            for (int i = 0; i < listLength; i++)
            {
                chisl += queueTime[i] * queueValue[i];
            }

            result = chisl / allTime;

            return result;
        }

        public double RequestsArrivedPerTime(double intensivityOfRequest, double intensivityOfService)
        {
            double result;

            result = intensivityOfService / (intensivityOfRequest + intensivityOfService);

            return result;
        }

        public double AvgWaitInQueue(List<int> queueValue, List<double> queueTime, int processedCount)
        {
            double result;
            double chisl = 0.0;

            int listLength = queueValue.Count;

            for (int i = 0; i < listLength; i++)
            {
                chisl += queueTime[i] * queueValue[i];
            }

            result = chisl / processedCount;

            return result;
        }
    }
}