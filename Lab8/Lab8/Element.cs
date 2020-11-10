using System.Collections.Generic;

namespace Lab7

{
    public class Element
    {
        public List<Arc> InArcs { get; set; } = new List<Arc>();
        public List<Arc> OutArcs { get; set; } = new List<Arc>();

        public string Name { get; set; }

        public Element(string name)
        {
            this.Name = name;
        }
    }
}