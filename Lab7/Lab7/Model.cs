using System;
using System.Collections.Generic;

namespace Lab7
{
    internal class Model
    {
        public List<Element> list { get; set; }

        public Model(List<Element> list)
        {
            this.list = list;
        }

        public void simulate(int coutIterations, bool printState)
        {
            InArcCalculate();
            SortProbability();

            for (int i = 0; i < coutIterations; i++)
            {
                for (int j = list.Count - 1; j >= 0; j--) // Loop by all elements in list
                {
                    List<bool> trList = new List<bool>();
                    bool transited = false;
                    Position element = null;
                    if (list[j].GetType() == typeof(Position))
                        element = (Position)list[j];

                    bool transit = true;
                    if (element != null)
                    {
                        Transition nextElement = null;
                        bool probabilitized = false;
                        for (int z = 0; z < element.OutArcs.Count; z++) // Loop by all outArc list
                        {
                            probabilitized = element.OutArcs[z].Probability == 1.0 ? false : true;
                            trList.Add(true);
                            nextElement = (Transition)element.OutArcs[z].NextElement;
                            foreach (var el in nextElement.InArcs)
                            {
                                Position inPos = (Position)el.NextElement;
                                if (el.Multiplicity > inPos.MarkersCount)
                                {
                                    if (probabilitized)
                                    {
                                        transit = false;
                                        break;
                                    }
                                    else
                                    {
                                        trList[z] = false;
                                        transit = false;
                                        continue;
                                    }
                                }
                            }
                            if (transit && probabilitized) break;
                        }
                        if (nextElement != null)
                        {
                            if (transit && !probabilitized) // Default transmit
                            {
                                transited = true;
                                Transit(nextElement, printState);
                                break;
                            }
                            else if (transit && probabilitized) // Transmit on probability
                            {
                                transited = true;
                                double rand = new Random().NextDouble();
                                double start = 0.0;
                                foreach (var x in element.OutArcs)
                                {
                                    if (rand >= start && rand <= start + x.Probability)
                                    {
                                        Transit((Transition)x.NextElement, printState);
                                        break;
                                    }
                                    start += x.Probability;
                                }
                                break;
                            }
                        }
                        if (transited == false && j == 0)
                        {
                            Console.WriteLine("There are no transitions!");
                            if (printState)
                                printResult();
                            return;
                        }
                    }
                }
            }
            if (printState)
                printResult();
        }

        public void printResult()
        {
            Console.WriteLine("-----------------Result-----------------");
            foreach (var x in list)
            {
                Position position;
                if (x.GetType() == typeof(Position))
                {
                    position = (Position)x;
                    Console.WriteLine($"{position.Name} has {position.MarkersCount} markers");
                }
            }
            Console.WriteLine();
            foreach (var x in list)
            {
                Transition position;
                if (x.GetType() == typeof(Transition))
                {
                    position = (Transition)x;
                    Console.WriteLine($"{position.Name} has quantity: {position.Quantity}");
                }
            }
            Console.WriteLine();
            foreach (var x in list)
            {
                Position position;
                if (x.GetType() == typeof(Position))
                {
                    position = (Position)x;
                    var min = int.MaxValue;
                    var max = int.MinValue;
                    var avg = 0.0;

                    foreach (var y in position.MarkerHistory)
                    {
                        if (y < min)
                            min = y;
                        if (y > max)
                            max = y;
                        avg += y;
                    }
                    Console.WriteLine($"{position.Name}\n  -min: {min}\n  -max: {max}\n  -avg: {avg / position.MarkerHistory.Count}");
                }
            }
        }

        public void Transit(Transition transition, bool printState)
        {
            Position position;
            if (printState)
                Console.WriteLine("In Arg:");
            foreach (var x in transition.InArcs)
            {
                position = (Position)x.NextElement;
                position.MarkersCount--;
                if (printState)
                    Console.WriteLine($"  {position.Name} has {position.MarkersCount} markers");
                position.MarkerHistory.Add(position.MarkersCount);
            }
            if (printState)
                Console.WriteLine("Out Arg:");
            foreach (var x in transition.OutArcs)
            {
                position = (Position)x.NextElement;
                position.MarkersCount++;
                if (printState)
                    Console.WriteLine($"  {position.Name} has {position.MarkersCount} markers");
                position.MarkerHistory.Add(position.MarkersCount);
            }
            transition.Quantity++;
            if (printState)
                Console.WriteLine();
        }

        public void InArcCalculate()
        {
            foreach (var x in list)
            {
                foreach (var y in x.OutArcs)
                {
                    y.NextElement.InArcs.Add(new Arc(y.Multiplicity, x, y.Priority));
                }
            }
        }

        public bool DoesProbEqual(Position position)
        {
            double probability = double.MinValue;

            foreach (var x in position.OutArcs)
                if (x != null)
                    probability = x.Probability;
            if (probability != double.MinValue)
            {
                for (int i = 0; i < position.OutArcs.Count; i++)
                    if (position.OutArcs[i] != null)
                        if (position.OutArcs[i].Probability != probability)
                            return false;
                return true;
            }
            return false;
        }

        public void SortProbability()
        {
            foreach (var x in list)
            {
                x.InArcs.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                x.OutArcs.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            }
        }
    }
}