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

        public double RequestsArrivedPerTime(List<double> createdTime, int createdCount)
        {
            double result;
            double chisl = 0.0;

            foreach (var x in createdTime)
            {
                chisl += x;
            }

            result = chisl / createdCount;

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