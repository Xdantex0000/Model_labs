using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    internal class TableData
    {
        public int createdQuantity;
        public List<int> failedQuantity = new List<int>();
        public double failProbility;
        public List<double> maxQueue = new List<double>();
        public List<double> avgQueue = new List<double>();
        public List<List<double>> maxPressure = new List<List<double>>();
        public List<List<double>> avgPressure = new List<List<double>>();
        public double createDelay;
        public List<List<double>> devicesCount = new List<List<double>>();
    }
}