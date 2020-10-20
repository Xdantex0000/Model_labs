using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class Device
    {
        public int Quantity { get; set; }
        public int State { get; set; }

        // поточний момент часу
        public double Tcurr { get; set; }

        // момент часу наступної події
        public double Tnext { get; set; }

        public string Name { get; }

        public Device(string deviceName)
        {
            Tnext = 0.0;
            Tcurr = Tnext;
            State = 0;
            Name = deviceName;
        }

        public void addQuantity()
        {
            Quantity++;
        }
    }
}