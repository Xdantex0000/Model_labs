namespace Lab7
{
    public class Arc
    {
        public int Multiplicity { get; set; }
        public Element NextElement { get; set; }
        public double Priority { get; set; }
        public double Probability { get; set; }

        public Arc(int multiplicity, Element element, double Probability, double Priority = 1.0)
        {
            this.Multiplicity = multiplicity;
            this.NextElement = element;
            this.Priority = Priority;
            this.Probability = Probability;
        }
    }
}