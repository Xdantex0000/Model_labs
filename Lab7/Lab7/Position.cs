using System.Collections.Generic;

namespace Lab7
{
    public class Position : Element
    {
        public int MarkersCount { get; set; }
        public List<int> MarkerHistory { get; set; } = new List<int>();

        public Position(int markersCount, string name = "Position") : base(name)
        {
            this.MarkersCount = markersCount;
            MarkerHistory.Add(markersCount);
        }
    }
}