namespace Lab7
{
    public class Transition : Element
    {
        public double DelayTime { get; set; }
        public int Quantity { get; set; }

        public Transition(double delay, string name = "Transition") : base(name)
        {
            this.DelayTime = delay;
        }
    }
}